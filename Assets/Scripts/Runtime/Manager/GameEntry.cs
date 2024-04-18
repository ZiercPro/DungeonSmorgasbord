using UnityEngine;
using ZiercCode.Runtime.Audio;
using ZiercCode.Runtime.Scene;

namespace ZiercCode.Runtime.Manager
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public class GameEntry : MonoBehaviour
    {
        private void Awake()
        {
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
            LocaleManager.Instance.SetLanguage(DataManager.SettingsData.Language);
        }

        private void EnterGame()
        {
            AudioPlayer.Instance.SetEnvironmentVolume(DataManager.SettingsData.EnvironmentVolume);
            AudioPlayer.Instance.SetMasterVolume(DataManager.SettingsData.MasterVolume);
            AudioPlayer.Instance.SetMusicVolume(DataManager.SettingsData.MusicVolume);
            AudioPlayer.Instance.SetSfxVolume(DataManager.SettingsData.SFXVolume);
            SceneSystem.SetScene(new StartScene());
        }

        private void ExitGame()
        {
            DataManager.SaveSettings();
        }
    }
}