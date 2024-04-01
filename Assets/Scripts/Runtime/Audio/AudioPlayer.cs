using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using ZiercCode.Runtime.Basic;
using ZiercCode.Runtime.Helper;
using ZiercCode.Runtime.ScriptObject;

namespace ZiercCode.Runtime.Audio
{
    /// <summary>
    /// 音效播放管理器 控制音频的播放、暂停和音量
    /// </summary>
    public class AudioPlayer : UnitySingleton<AudioPlayer>
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
            _audioDic = audioList.AudioEditableDictionary.ToDictionary();
            _audioManager = new AudioManager();
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
                Debug.LogError($"音频{audioBase.AudioType.Name}组件缺失!");
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

        #region 弃用

////淡入淡出
//public void PlayMusicWithFade(AudioClip music, float duration = 2f)
//{
//    StopAllCoroutines();
//    float halfduration = duration / 2;
//    StartCoroutine(FadeTransiton(music, halfduration));
//}
//IEnumerator FadeTransiton(AudioClip music, float duration)
//{
//    AudioSource player = GetCurrentPlayer();
//    if (!player.isPlaying) player.Play();
//    for (float i = 0; i < duration; i += Time.deltaTime)
//    {
//        player.volume = 1 - (i / duration);
//        yield return null;
//    }
//    player.volume = 0f;
//    player.clip = music;
//    for (float i = 0; i < duration; i += Time.deltaTime)
//    {
//        player.volume = i / duration;
//        yield return null;
//    }
//    player.volume = 1f;
//}
////交叉
//public void PlayMusicWithCross(AudioClip music, float duration = 2f)
//{
//    StopAllCoroutines();
//    float halfduration = duration / 2;
//    StartCoroutine(CrossTransion(music, halfduration));
//}
//IEnumerator CrossTransion(AudioClip music, float duration)
//{
//    AudioSource currentPlayer = GetCurrentPlayer();
//    AudioSource nextPlayer = GetAnotherPlayer();
//    if (currentPlayer == null || nextPlayer == null) Debug.LogErrorFormat("player reference is null!");
//    nextPlayer.clip = music;
//    if (!currentPlayer.isPlaying) currentPlayer.Play();
//    nextPlayer.volume = 0f;
//    nextPlayer.Play();
//    for (float i = 0; i < duration; i += Time.deltaTime)
//    {
//        currentPlayer.volume = 1 - (i / duration);
//        nextPlayer.volume = i / duration;
//        yield return null;
//    }
//    nextPlayer.volume = 1f;
//    currentPlayer.volume = 0f;
//    currentPlayer.Stop();
//}

        #endregion

        #region SET VOLUME

        public void SetMusicVolume(float amount)
        {
            audioMixer.SetFloat("musicVolume", amount);
        }

        public void SetSFXVolume(float amount)
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

        public AudioMixerGroup Music => music;
        public AudioMixerGroup Sfx => sfx;
        public AudioMixerGroup Environment => environment;

        #endregion
    }
}