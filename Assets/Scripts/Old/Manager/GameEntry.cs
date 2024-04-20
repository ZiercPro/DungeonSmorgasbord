using ZiercCode.Core.System;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Scene;

namespace ZiercCode.Old.Manager
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public class GameEntry : USingletonComponentDontDestroy<GameEntry>
    {
        public ConfigManager ConfigManager { get; private set; }

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
            ConfigManager = new ConfigManager();
            ConfigManager.Load();
            LocaleManager.Instance.SetLanguage(ConfigManager.SettingsData.Language);
        }

        private void EnterGame()
        {
            AudioPlayer.Instance.SetEnvironmentVolume(ConfigManager.SettingsData.EnvironmentVolume);
            AudioPlayer.Instance.SetMasterVolume(ConfigManager.SettingsData.MasterVolume);
            AudioPlayer.Instance.SetMusicVolume(ConfigManager.SettingsData.MusicVolume);
            AudioPlayer.Instance.SetSfxVolume(ConfigManager.SettingsData.SfxVolume);
            SceneSystem.SetScene(new StartScene());
        }

        private void ExitGame()
        {
            ConfigManager.Save();
        }
    }
}