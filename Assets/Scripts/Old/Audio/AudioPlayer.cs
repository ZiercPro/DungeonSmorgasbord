using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;
using ZiercCode.Core.Utilities;
using ZiercCode.Old.ScriptObject;
using ZiercCode.Test.ObjectPool;

namespace ZiercCode.Old.Audio
{
    /// <summary>
    /// 音效播放管理器 控制音频的播放、暂停和音量
    /// </summary>
    public class AudioPlayer : USingleton<AudioPlayer>
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioMixerGroup music;
        [SerializeField] private AudioMixerGroup sfx;
        [SerializeField] private AudioMixerGroup environment;
        [Space] [SerializeField] private AudioSource musicPlayer;
        [Space] [SerializeField] private AudioSource sfxPlayer;

        private AudioBase _currentMusic;
        private bool _isMusicPlaying;

        protected override void Awake()
        {
            base.Awake();
            ZiercPool.Register("sfxPlayer", sfxPlayer);
        }

        public void PlayMusic(string audioName)
        {
            AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(audioName);
            handle.Completed += h =>
            {
                PlayMusic(h.Result);
            };
        }

        public void PlayMusic(AudioClip musicClip)
        {
            if (musicPlayer.isPlaying)
            {
                musicPlayer.Stop();
            }

            musicPlayer.clip = musicClip;
            musicPlayer.Play();
        }

        public void PlaySfx(string clip)
        {
            AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(clip);
            handle.Completed += h =>
            {
                PlaySfx(h.Result);
            };
        }

        public void PlaySfx(AudioClip clip)
        {
            sfxPlayer.PlayOneShot(clip);
        }

        public void PlaySfx(string clip, Vector3 position)
        {
            AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(clip);
            handle.Completed += h =>
            {
                PlaySfx(h.Result, position);
            };
        }

        public void PlaySfx(AudioClip clip, Vector3 position)
        {
            AudioSource newSfxPlayer = ZiercPool.Get("sfxPlayer") as AudioSource;
            if (newSfxPlayer)
            {
                newSfxPlayer.transform.position = position;
                newSfxPlayer.PlayOneShot(clip);
                ZiercPool.Release("sfxPlayer", newSfxPlayer);
            }
            else
            {
                Debug.LogWarning($"{clip.name}获取sfx播放器失败");
            }
        }

        /// <summary>
        /// 异步播放音频
        /// </summary>
        /// <param name="audioName">音频名称</param>
        // public void PlayAudio(AudioName audioName)
        // {
        //     AudioBase audioBase = _audioDic[audioName];
        //
        //     AudioSource player = _audioManager.GetAudioSource(audioBase);
        //
        //     if (player)
        //     {
        //         if (audioBase.outputGroup == sfx)
        //             PlaySfx(player, player.clip);
        //         else
        //         {
        //             PlayMusic(player);
        //             if (audioBase.outputGroup == music)
        //             {
        //                 _currentMusic = audioBase;
        //                 _isMusicPlaying = true;
        //             }
        //         }
        //     }
        //     else
        //     {
        //         Debug.LogWarning($"音频{audioBase.AudioType.Name}组件获取失败!");
        //     }
        // }
        //
        // /// <summary>
        // /// 异步随机播放音频
        // /// </summary>
        // /// <param name="audioNames">音频名称链表</param>
        // public void PlayAudiosRandomAsync(List<AudioName> audioNames)
        // {
        //     int length = audioNames.Count;
        //     int temp = MyMath.GetRandom(0, length);
        //     PlayAudio(audioNames[temp]);
        // }
        //
        // /// <summary>
        // /// 异步停止播发音频
        // /// </summary>
        // /// <param name="audioName">音频名称</param>
        // public void StopAudio(AudioName audioName)
        // {
        //     AudioBase audioBase = _audioDic[audioName];
        //     AudioSource player = _audioManager.GetAudioSource(audioBase);
        //     if (player)
        //     {
        //         if (audioBase == _currentMusic)
        //             _isMusicPlaying = false;
        //         player.Stop();
        //     }
        //     else
        //     {
        //         Debug.LogWarning($"音频{audioBase.AudioType.Name}组件缺失或被销毁!");
        //     }
        // }
        //
        // /// <summary>
        // /// 清除已经加载的音频缓存
        // /// </summary>
        // /// <returns></returns>
        // public void ClearAudioCache()
        // {
        //     _audioManager.RemoveAllAudios();
        // }
        // private void StopAudio(AudioBase audioBase)
        // {
        //     AudioSource player = _audioManager.GetAudioSource(audioBase);
        //     if (player)
        //     {
        //         if (audioBase == _currentMusic)
        //             _isMusicPlaying = false;
        //         player.Stop();
        //     }
        //     else
        //     {
        //         Debug.LogWarning($"音频{audioBase.AudioType.Name}组件缺失或被销毁!");
        //     }
        // }
        //
        //
        // private void PlayMusic(AudioSource player)
        // {
        //     if (_isMusicPlaying)
        //         StopAudio(_currentMusic);
        //
        //     if (!player.isPlaying)
        //         player.Play();
        //     else
        //         Debug.LogWarning($"{player.name}is already playing");
        // }
        //
        // private void PlaySfx(AudioSource player, AudioClip clip)
        // {
        //     player.PlayOneShot(clip);
        // }
        public void SetMusicVolume(float amount)
        {
            audioMixer.SetFloat("musicVolume", amount);
        }

        public void SetSfxVolume(float amount)
        {
            audioMixer.SetFloat("sfxVolume", amount);
        }

        public void SetMasterVolume(float amount)
        {
            audioMixer.SetFloat("masterVolume", amount);
        }

        public void SetEnvironmentVolume(float amount)
        {
            audioMixer.SetFloat("environmentVolume", amount);
        }
    }
}