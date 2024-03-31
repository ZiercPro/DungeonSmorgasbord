using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

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
        private Dictionary<AudioType, AudioSource> _audioSourceDictionary;

        public AudioManager()
        {
            _audioSourceDictionary = new Dictionary<AudioType, AudioSource>();
        }

        /// <summary>
        /// 通过audiobase异步创建新的音频
        /// </summary>
        /// <param name="audioBase"></param>
        /// <returns></returns>
        public async Task<AudioSource> GetAudioSourceAsync(AudioBase audioBase)
        {
            AudioClip newClip;
            var tcs = new TaskCompletionSource<AudioSource>();
            if (_audioSourceDictionary.TryGetValue(audioBase.AudioType, out AudioSource value))
            {
                tcs.SetResult(value);
                return await tcs.Task;
            }

            AsyncOperationHandle<AudioClip> asyncOperationHandle =
                Addressables.LoadAssetAsync<AudioClip>($"{audioBase.AudioType.Path}");
            asyncOperationHandle.Completed += handle =>
            {
                if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    newClip = Object.Instantiate(handle.Result);
                    tcs.SetResult(CreateNewAudioSource(audioBase, newClip, FindAudiosParent()));
                }

                if (asyncOperationHandle.Status == AsyncOperationStatus.Failed)
                {
                    Debug.LogError("加载失败!");
                }
            };

            return await tcs.Task;
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
        //         Debug.LogError($"资源 {audioBase.AudioType.Name} 加载失败");
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

            _audioSourceDictionary.Add(audioBase.AudioType, newComponent);
            return newComponent;
        }
    }
}