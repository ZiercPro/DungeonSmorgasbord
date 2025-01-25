using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;
using ZiercCode.Core.Utilities;
using ZiercCode.Test.ObjectPool;

namespace ZiercCode.Old.Audio
{
    /// <summary>
    /// 控制音频的播放、暂停和音量
    /// 这个组件需要在编辑器中配置，并且放在不可销毁场景中，所以继承的USingleton
    /// </summary>
    public class AudioPlayer : USingleton<AudioPlayer>
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioMixerGroup music;
        [SerializeField] private AudioMixerGroup sfx;
        [SerializeField] private AudioMixerGroup environment;
        [Space][SerializeField] private AudioSource musicPlayer;
        [Space][SerializeField] private AudioSource sfxPlayer;

        private bool _isMusicPlaying;//用于切换音乐 一次只能有一首背景音乐播放

        public void Init()
        {
            ZiercPool.Register("sfxPlayer", sfxPlayer);//在对象池中注册音效播放器 因为游戏中需要大量创建
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

        #region 设置音量
        public void SetMusicVolume(float amount)
        {
            string musicName = "musicVolume";
            audioMixer.SetFloat(musicName, amount);
        }

        public void SetSfxVolume(float amount)
        {
            string sfxName = "sfxVolume";
            audioMixer.SetFloat(sfxName, amount);
        }

        public void SetMasterVolume(float amount)
        {
            string masterName = "masterVolume";
            audioMixer.SetFloat(masterName, amount);
        }

        public void SetEnvironmentVolume(float amount)
        {
            string envName = "environmentVolume";
            audioMixer.SetFloat(envName, amount);
        }
        #endregion
    }
}