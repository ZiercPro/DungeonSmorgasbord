using Runtime.ScriptObject;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Runtime.Audio
{
    using Base;
    using Basic;
    using Helper;

    /// <summary>
    /// 音效播放管理器 控制音频的播放、暂停和音量
    /// </summary>
    public class AudioPlayerManager : UnitySingleton<AudioPlayerManager>
    {
        [SerializeField] private AudioListSo audioList;
        [Space] [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioMixerGroup music;
        [SerializeField] private AudioMixerGroup sfx;
        [SerializeField] private AudioMixerGroup environment;

        private AudioManager _audioManager;
        private AudioBase _currentMusic;
        private bool _isMusicPlaying;

        protected override void Awake()
        {
            base.Awake();
            _audioManager = new AudioManager();
        }

        private void PlayAudio(AudioBase audioBase)
        {
            AudioSource player = _audioManager.GetAudioSource(audioBase);
            if (player)
            {
                if (audioBase.outputGroup == sfx)
                    PlaySFX(player, player.clip);
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

        public void PlayAudio(AudioName audioName)
        {
            AudioBase audioBase = audioList.Audios[audioName];
            AudioSource player = _audioManager.GetAudioSource(audioBase);
            if (player)
            {
                if (audioBase.outputGroup == sfx)
                    PlaySFX(player, player.clip);
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

        private void PlayAudiosRandom(List<AudioBase> audioBases)
        {
            int length = audioBases.Count;
            int temp = MyMath.GetRandom(0, length);
            PlayAudio(audioBases[temp]);
        }

        public void PlayAudiosRandom(List<AudioName> audioNames)
        {
            int length = audioNames.Count;
            int temp = MyMath.GetRandom(0, length);
            PlayAudio(audioNames[temp]);
        }

        private void StopAudio(AudioBase audioBase)
        {
            AudioSource player = _audioManager.GetAudioSource(audioBase);
            if (player)
            {
                if (audioBase == _currentMusic)
                    _isMusicPlaying = false;
                player.Stop();
            }
            else
            {
                Debug.LogError($"音频{audioBase.AudioType.Name}组件缺失!");
            }
        }

        public void StopAudio(AudioName audioName)
        {
            AudioBase audioBase = audioList.Audios[audioName];
            AudioSource player = _audioManager.GetAudioSource(audioBase);
            if (player)
            {
                if (audioBase == _currentMusic)
                    _isMusicPlaying = false;
                player.Stop();
            }
            else
            {
                Debug.LogError($"音频{audioBase.AudioType.Name}组件缺失!");
            }
        }

        private void PlayMusic(AudioSource player)
        {
            if (!player.isPlaying)
            {
                if (_isMusicPlaying)
                    StopAudio(_currentMusic);
                player.Play();
            }
        }

        private void PlaySFX(AudioSource player, AudioClip clip)
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