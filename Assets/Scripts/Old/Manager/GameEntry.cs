using ZiercCode.Core.System;
using ZiercCode.DungeonSmorgasbord.Locale;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Scene;

namespace ZiercCode.Old.Manager
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public class GameEntry : USingletonComponentDontDestroy<GameEntry>
    {
        private ConfigManager _configManager;

        protected override void Awake()
        {
            base.Awake();
            Init();
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
        /// 游戏入口初始化
        /// </summary>
        private void Init()
        {
            _configManager = new ConfigManager();
        }

        /// <summary>
        /// 游戏初始化
        /// </summary>
        private void InitGame()
        {
            _configManager.Load();
            LocaleManager.Instance.SetLanguage(ConfigManager.SettingsData.Language);
        }

        private void EnterGame()
        {
            AudioPlayer.Instance.SetEnvironmentVolume(ConfigManager.SettingsData.EnvironmentVolume);
            AudioPlayer.Instance.SetMasterVolume(ConfigManager.SettingsData.MasterVolume);
            AudioPlayer.Instance.SetMusicVolume(ConfigManager.SettingsData.MusicVolume);
            AudioPlayer.Instance.SetSfxVolume(ConfigManager.SettingsData.SfxVolume);
            SceneSystem.SetScene(new MainMenuScene());
        }

        private void ExitGame()
        {
            _configManager.Save();
        }
    }
}