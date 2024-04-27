using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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
        /// 储存已经创建好的audiosource
        /// </summary>
        private Dictionary<AudioBase, AudioSource> _audioSourceDictionary;

        public AudioManager()
        {
            _audioParent = FindAudiosParent();
            _audioClipDictionary = new Dictionary<AudioBase, AudioClip>();
            _audioSourceDictionary = new Dictionary<AudioBase, AudioSource>();
        }

        /// <summary>
        /// 通过audiobase异步创建新的音频
        /// </summary>
        /// <param name="audioBase"></param>
        /// <returns></returns>
        public async Task<AudioSource> GetAudioSourceAsync(AudioBase audioBase)
        {
            if (!_audioParent) _audioParent = FindAudiosParent();
            AudioClip newClip = null;
            AudioSource result = null;

            if (CheckAudioSourceDictionary(audioBase, out result))
                return result;

            if (CheckAudioClipDictionary(audioBase, out newClip))
            {
                result = CreateNewAudioSource(audioBase, newClip, _audioParent);
                return result;
            }

            AsyncOperationHandle<AudioClip> asyncOperationHandle =
                Addressables.LoadAssetAsync<AudioClip>(audioBase.AudioType.Path);
            asyncOperationHandle.Completed += handle =>
            {
                if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    newClip = Object.Instantiate(handle.Result);
                    _audioClipDictionary.TryAdd(audioBase, newClip);
                }
                else
                    Debug.LogError("加载失败!");
            };

            await asyncOperationHandle.Task;

            return CreateNewAudioSource(audioBase, newClip, _audioParent);
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

        // /// <summary>
        // /// 通过audiobase创建新的音频
        // /// </summary>
        // /// <param name="audioBase"></param>
        // /// <returns></returns>
        // [Obsolete]
        // public AudioSource GetAudioSource(AudioBase audioBase)
        // {
        //     AudioClip newClip = null;
        //     AsyncOperationHandle<AudioClip> asyncOperationHandle;
        //     if (audioClipsDictionary.ContainsKey(audioBase.AudioType))
        //         newClip = audioClipsDictionary[audioBase.AudioType];
        //     else
        //     {
        //         asyncOperationHandle = Addressables.LoadAssetAsync<AudioClip>(audioBase.AudioType.Path);
        //         asyncOperationHandle.Completed += handle =>
        //         {
        //             AudioClip prefab = handle.Result;
        //             newClip = Object.Instantiate(prefab);
        //             Addressables.Release(asyncOperationHandle);
        //         };
        //
        //         asyncOperationHandle.WaitForCompletion();
        //
        //         audioClipsDictionary.Add(audioBase.AudioType, newClip);
        //     }
        //
        //     if (!newClip)
        //     {
        //         Debug.LogError($"资源 {audioBase.AudioType.name} 加载失败");
        //         return null;
        //     }
        //
        //     if (audioSourceDictionary.ContainsKey(audioBase.AudioType))
        //     {
        //         if (!audioSourceDictionary[audioBase.AudioType])
        //         {
        //             audioSourceDictionary[audioBase.AudioType] =
        //                 CreateNewAudioSource(audioBase, newClip, _audioParent);
        //         }
        //
        //         return audioSourceDictionary[audioBase.AudioType];
        //     }
        //
        //     AudioSource newComponent = CreateNewAudioSource(audioBase, newClip, _audioParent);
        //
        //     audioSourceDictionary.Add(audioBase.AudioType, newComponent);
        //
        //     return newComponent;
        // }

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

            Debug.LogWarning("audioBase already exist");
            
            Object.Destroy(newAudioS);
            return _audioSourceDictionary[audioBase];
        }
    }
}