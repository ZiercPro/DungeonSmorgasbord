using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using ZiercCode.Core.Utilities;
using ZiercCode.Old.ScriptObject;

namespace ZiercCode.Old.Audio
{
    /// <summary>
    /// 音效播放管理器 控制音频的播放、暂停和音量
    /// </summary>
    public class AudioPlayer : USingleton<AudioPlayer>
    {
        [SerializeField] private AudioListSo audioList;
        [Space] [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioMixerGroup music;
        [SerializeField] private AudioMixerGroup sfx;
        [SerializeField] private AudioMixerGroup environment;

        private Dictionary<AudioName, AudioBase> _audioDic;

        private AudioManager _audioManager;
        private AudioBase _currentMusic;
        private bool _isMusicPlaying;

        protected override void Awake()
        {
            base.Awake();
            _audioManager = new AudioManager();
            _audioDic = audioList.AudioEditableDictionary.ToDictionary();
        }

        /// <summary>
        /// 异步播放音频
        /// </summary>
        /// <param name="audioName">音频名称</param>
        public async void PlayAudioAsync(AudioName audioName)
        {
            AudioBase audioBase = _audioDic[audioName];

            AudioSource player = await _audioManager.GetAudioSourceAsync(audioBase);
            if (player)
            {
                if (audioBase.outputGroup == sfx)
                    PlaySfx(player, player.clip);
                else
                {
                    PlayMusic(player);
                    if (audioBase.outputGroup == music)
                    {
                        _currentMusic = audioBase;
                        _isMusicPlaying = true;
                    }
                }
            }
            else
            {
                Debug.LogWarning($"音频{audioBase.AudioType.Name}组件获取失败!");
            }
        }

        /// <summary>
        /// 异步随机播放音频
        /// </summary>
        /// <param name="audioNames">音频名称链表</param>
        public void PlayAudiosRandomAsync(List<AudioName> audioNames)
        {
            int length = audioNames.Count;
            int temp = MyMath.GetRandom(0, length);
            PlayAudioAsync(audioNames[temp]);
        }

        /// <summary>
        /// 异步停止播发音频
        /// </summary>
        /// <param name="audioName">音频名称</param>
        public async void StopAudioAsync(AudioName audioName)
        {
            AudioBase audioBase = _audioDic[audioName];
            AudioSource player = await _audioManager.GetAudioSourceAsync(audioBase);
            if (player)
            {
                if (audioBase == _currentMusic)
                    _isMusicPlaying = false;
                player.Stop();
            }
            else
            {
                Debug.LogWarning($"音频{audioBase.AudioType.Name}组件缺失或被销毁!");
            }
        }

        /// <summary>
        /// 清除已经加载的音频缓存
        /// </summary>
        /// <returns></returns>
        public void ClearAudioCache()
        {
            _audioManager.RemoveAllAudios();
        }


        private async void StopAudioAsync(AudioBase audioBase)
        {
            AudioSource player = await _audioManager.GetAudioSourceAsync(audioBase);
            if (player)
            {
                if (audioBase == _currentMusic)
                    _isMusicPlaying = false;
                player.Stop();
            }
            else
            {
                Debug.LogWarning($"音频{audioBase.AudioType.Name}组件缺失或被销毁!");
            }
        }


        private void PlayMusic(AudioSource player)
        {
            if (_isMusicPlaying)
                StopAudioAsync(_currentMusic);

            if (!player.isPlaying)
                player.Play();
            else
                Debug.LogWarning($"{player.name}is already playing");
        }

        private void PlaySfx(AudioSource player, AudioClip clip)
        {
            player.PlayOneShot(clip);
        }


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