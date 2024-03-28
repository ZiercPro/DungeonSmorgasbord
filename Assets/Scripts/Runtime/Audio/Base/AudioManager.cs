using UnityEngine;
using System.Collections.Generic;

namespace Runtime.Audio.Base
{
    /// <summary>
    /// 音频管理器
    /// 帮助获取音频切片资源、加载到字典中并生成AudioBase
    /// </summary>
    public class AudioManager
    {
        /// <summary>
        ///储存已经读取过的音频资源
        /// </summary>
        public Dictionary<AudioType, AudioSource> audioDic { get; private set; }

        /// <summary>
        /// 储存所以读取过的音频切片
        /// </summary>
        private Dictionary<AudioType, AudioClip> clips;

        public AudioManager()
        {
            audioDic = new Dictionary<AudioType, AudioSource>();
            clips = new Dictionary<AudioType, AudioClip>();
        }

        /// <summary>
        /// 通过audiobase创建新的音频
        /// </summary>
        /// <param name="audiobase"></param>
        /// <returns></returns>
        public AudioSource GetAudioSource(AudioBase audiobase)
        {
            GameObject parent = GameObject.Find("Audios");
            if (parent == null)
                parent = new GameObject("Audios");

            GameObject newAudioS = null;
            AudioClip newClip = null;
            AudioSource newComponent = null;

            if (clips.ContainsKey(audiobase.AudioType))
                newClip = clips[audiobase.AudioType];
            else
            {
                // Addressables.LoadAssetAsync<AudioClip>(audiobase.AudioType.Path).Completed += handle =>
                // {
                //     AudioClip prefab = handle.Result;
                //     newClip = Object.Instantiate(prefab);
                // };
                newClip = GameObject.Instantiate(Resources.Load<AudioClip>(audiobase.AudioType.Path));
                clips.Add(audiobase.AudioType, newClip);
            }

            if (newClip == null)
            {
                Debug.LogError($"资源 {audiobase.AudioType.Name} 加载失败");
                return null;
            }

            if (audioDic.ContainsKey(audiobase.AudioType))
            {
                if (audioDic[audiobase.AudioType] == null)
                {
                    newAudioS = new GameObject(audiobase.AudioType.Name);
                    newAudioS.transform.SetParent(parent.transform);
                    newComponent = newAudioS.AddComponent<AudioSource>();

                    newComponent.clip = newClip;
                    newComponent.playOnAwake = audiobase.isPlayAwake;
                    newComponent.loop = audiobase.isLoop;
                    newComponent.volume = audiobase.volume;
                    newComponent.outputAudioMixerGroup = audiobase.outputGroup;

                    audioDic[audiobase.AudioType] = newComponent;
                }

                return audioDic[audiobase.AudioType];
            }

            newAudioS = new GameObject(audiobase.AudioType.Name);
            newAudioS.transform.SetParent(parent.transform);
            newComponent = newAudioS.AddComponent<AudioSource>();

            newComponent.clip = newClip;
            newComponent.playOnAwake = audiobase.isPlayAwake;
            newComponent.loop = audiobase.isLoop;
            newComponent.volume = audiobase.volume;
            newComponent.outputAudioMixerGroup = audiobase.outputGroup;

            audioDic.Add(audiobase.AudioType, newComponent);

            return newComponent;
        }
    }
}