using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.ResourceManagement.AsyncOperations;
using ZiercCode.ObjectPool;
using ZiercCode.Utilities;

namespace ZiercCode.Audio
{
    /// <summary>
    /// 控制音频的播放、暂停和音量
    /// 这个组件需要在编辑器中配置，并且放在不可销毁场景中，所以继承的USingleton
    /// </summary>
    public class AudioPlayer : USingleton<AudioPlayer>
    {
        //混音器 用于分别处理不同种类的音频
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioMixerGroup music;
        [SerializeField] private AudioMixerGroup sfx;
        [SerializeField] private AudioMixerGroup environment;

        [Space] [SerializeField] private AudioSource environmentPlayer;
        [Space] [SerializeField] private AudioSource musicPlayer;
        [Space] [SerializeField] private AudioSource sfxPlayer;

        private Dictionary<string, AudioClip> _loadedClips = new Dictionary<string, AudioClip>();

        private LinkedList<AudioSource> _sfxPlayerToRelease = new LinkedList<AudioSource>(); //需要释放的音效播放器

        // //通过Addressable加载并保存磁盘中的音频资源
        // private AudioClip LoadClip(string addressableClipName)
        // {
        //     AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(addressableClipName);
        //     handle.WaitForCompletion();
        //     _loadedClips.Add(addressableClipName, handle.Result);
        //     return handle.Result;
        // }

        private void AutoReleaseSfxPlayer()
        {
            LinkedListNode<AudioSource> first = _sfxPlayerToRelease.First;
            while (first != null)
            {
                if (!first.Value.isPlaying)
                {
                    PoolManager.Instance.Release("sfxPlayer", first.Value);
                    _sfxPlayerToRelease.Remove(first);
                }

                first = first.Next;
            }
        }

        //获取sfx播放器需要做的事情
        private AudioSource GetSfxPlayer()
        {
            AudioSource newSfx = (AudioSource)PoolManager.Instance.Get("sfxPlayer");
            _sfxPlayerToRelease.AddLast(newSfx);
            newSfx.transform.SetParent(null);
            newSfx.transform.SetParent(transform);
            return newSfx;
        }

        private void Update()
        {
            AutoReleaseSfxPlayer();
        }

        public void Init()
        {
            PoolManager.Instance.Register("sfxPlayer", sfxPlayer, 30, 200); //在对象池中注册音效播放器 因为游戏中需要大量创建
        }

        //这里的音量是单独调整音频音量的 因为有些音频自身音量不合适 需要调整
        public void PlayMusic(string audioName, float volume = 1f)
        {
            AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(audioName);
            handle.Completed += re =>
            {
                PlayMusic(re.Result, volume);
            };
        }

        public void PlayMusic(AudioClip musicClip, float volume = 1f)
        {
            musicPlayer.volume = volume;

            if (musicPlayer.isPlaying)
            {
                musicPlayer.Stop();
            }

            musicPlayer.clip = musicClip;
            musicPlayer.Play();
        }

        public void PlaySfx(string audioName, float volume = 1f)
        {
            AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(audioName);
            handle.Completed += re =>
            {
                PlaySfx(re.Result, volume);
            };
        }

        public void PlaySfx(AudioClip clip, float volume = 1f)
        {
            AudioSource newSfxPlayer = GetSfxPlayer();

            newSfxPlayer.volume = volume;
            newSfxPlayer.clip = clip;

            newSfxPlayer.PlayOneShot(clip);
        }

        public void PlaySfx(string audioName, Vector3 position, float volume = 1f)
        {
            AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(audioName);
            handle.Completed += h =>
            {
                PlaySfx(h.Result, position);
            };
        }

        public void PlaySfx(AudioClip clip, Vector3 position, float volume = 1f)
        {
            AudioSource newSfxPlayer = GetSfxPlayer();

            newSfxPlayer.volume = volume;
            newSfxPlayer.clip = clip;
            newSfxPlayer.transform.position = position;

            newSfxPlayer.PlayOneShot(clip);
        }

        public void PlayEnvironmentSfx(string audioName, float volume = 1f)
        {
            AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>(audioName);
            handle.Completed += re =>
            {
                PlayEnvironmentSfx(re.Result, volume);
            };
        }

        public void PlayEnvironmentSfx(AudioClip clip, float volume = 1f)
        {
            environmentPlayer.clip = clip;
            environmentPlayer.volume = volume;

            environmentPlayer.Play();
        }

        public void StopMusic()
        {
            if (!musicPlayer.isPlaying) return;

            musicPlayer.Stop();
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