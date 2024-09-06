using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Test.ObjectPool;
using Object = UnityEngine.Object;

namespace ZiercCode.Old.Audio
{
    /// <summary>
    /// 音频管理器
    /// 帮助获取音频切片资源、加载到字典中并生成AudioBase
    /// </summary>
    public class AudioManager
    {
        private Transform _audioParent;

        /// <summary>
        ///储存已经读取过的音频资源
        /// </summary>
        private Dictionary<AudioBase, AudioClip> _audioClipDictionary;

        /// <summary>
        /// 储存已经创建好的audioSource
        /// </summary>
        private Dictionary<AudioBase, AudioSource> _audioSourceDictionary;

        public AudioManager()
        {
            _audioParent = FindAudiosParent();
            _audioClipDictionary = new Dictionary<AudioBase, AudioClip>();
            _audioSourceDictionary = new Dictionary<AudioBase, AudioSource>();
        }

        public AudioSource GetAudioSource(AudioBase audioBase)
        {
            if (!_audioParent) _audioParent = FindAudiosParent();

            if (CheckAudioSourceDictionary(audioBase, out AudioSource result))
                return result;

            if (CheckAudioClipDictionary(audioBase, out AudioClip newClip))
            {
                result = CreateNewAudioSource(audioBase, newClip, _audioParent);
                return result;
            }

            newClip = ZiercPool.Get(audioBase.AudioType.Name) as AudioClip;

            if (newClip != null)
            {
                _audioClipDictionary.TryAdd(audioBase, newClip);
                return CreateNewAudioSource(audioBase, newClip, _audioParent);
            }
            else
            {
                Debug.LogError($"无法获取音频资源{audioBase.AudioType.Name}");
                return null;
            }
        }

        public bool RemoveAllAudios()
        {
            if (_audioSourceDictionary != null)
            {
                if (_audioSourceDictionary.Count == 0)
                    return true;
                _audioSourceDictionary.Clear();
                return true;
            }

            return false;
        }

        //检查是否已经加载了clip
        private bool CheckAudioClipDictionary(AudioBase audioBase, out AudioClip audioClip)
        {
            if (_audioClipDictionary.TryGetValue(audioBase, out AudioClip value))
                if (value)
                {
                    audioClip = value;
                    return true;
                }

            _audioClipDictionary.Remove(audioBase);
            audioClip = null;
            return false;
        }

        //检查是否已经加载了audioSource
        private bool CheckAudioSourceDictionary(AudioBase audioBase, out AudioSource audioSource)
        {
            if (_audioSourceDictionary.TryGetValue(audioBase, out AudioSource value))
            {
                if (value && value.clip)
                {
                    audioSource = value;
                    return true;
                }

                Object.Destroy(value.gameObject);
                _audioSourceDictionary.Remove(audioBase);
            }

            audioSource = null;
            return false;
        }

        //获取音频父物体
        private Transform FindAudiosParent()
        {
            GameObject parent = GameObject.Find("Audios");
            if (!parent) parent = new GameObject("Audios");
            return parent.transform;
        }

        //创建新的AudioSource
        private AudioSource CreateNewAudioSource(AudioBase audioBase, AudioClip clip, Transform parent)
        {
            GameObject newAudioS = new(audioBase.AudioType.Name);
            AudioSource newComponent = newAudioS.AddComponent<AudioSource>();

            newAudioS.transform.SetParent(parent);

            newComponent.clip = clip;
            newComponent.playOnAwake = audioBase.isPlayAwake;
            newComponent.loop = audioBase.isLoop;
            newComponent.volume = audioBase.volume;
            newComponent.outputAudioMixerGroup = audioBase.outputGroup;

            if (_audioSourceDictionary.TryAdd(audioBase, newComponent))
                return newComponent;

            Object.Destroy(newAudioS);
            return _audioSourceDictionary[audioBase];
        }
    }
}