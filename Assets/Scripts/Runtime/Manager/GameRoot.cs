using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using ZiercCode.Runtime.Audio;
using ZiercCode.Runtime.Basic;
using ZiercCode.Runtime.Data;
using ZiercCode.Runtime.Scene;

namespace ZiercCode.Runtime.Manager
{
    /// <summary>
    /// 游戏程序全局管理器
    /// </summary>
    public class GameRoot : USingletonComponentDontDestroy<GameRoot>
    {
        protected override void Awake()
        {
            base.Awake();
            InitGame();
        }

        private void Start()
        {
            EnterGame();
        }

        private void OnDestroy()
        {
            ExitGame();
        }


        /// <summary>
        /// 游戏初始化
        /// </summary>
        private void InitGame()
        {
            DataManager.LoadSettings();
            AudioPlayer.Instance.SetEnvironmentVolume(DataManager.SettingsData.EnvironmentVolume);
            AudioPlayer.Instance.SetMasterVolume(DataManager.SettingsData.MasterVolume);
            AudioPlayer.Instance.SetMusicVolume(DataManager.SettingsData.MusicVolume);
            AudioPlayer.Instance.SetSFXVolume(DataManager.SettingsData.SFXVolume);
        }

        private void EnterGame()
        {
            SceneSystem.SetScene(new StartScene());
            AudioPlayer.Instance.PlayAudioAsync(AudioName.MenuBgm);
        }

        private void ExitGame()
        {
            DataManager.SaveSettings();
        }
    }
}