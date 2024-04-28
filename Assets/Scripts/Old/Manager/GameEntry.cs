using ZiercCode.Core.System;
using ZiercCode.DungeonSmorgasbord.Locale;
using ZiercCode.DungeonSmorgasbord.Manager;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Scene;

namespace ZiercCode.Old.Manager
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public class GameEntry : USingletonComponentDontDestroy<GameEntry>
    {
        private DataManager _dataManager;

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
            _dataManager = new DataManager();
        }

        /// <summary>
        /// 游戏初始化
        /// </summary>
        private void InitGame()
        {
            _dataManager.Load();
            LocaleManager.Instance.SetLanguage(DataManager.SettingsData.Language);
        }

        private void EnterGame()
        {
            AudioPlayer.Instance.SetEnvironmentVolume(DataManager.SettingsData.EnvironmentVolume);
            AudioPlayer.Instance.SetMasterVolume(DataManager.SettingsData.MasterVolume);
            AudioPlayer.Instance.SetMusicVolume(DataManager.SettingsData.MusicVolume);
            AudioPlayer.Instance.SetSfxVolume(DataManager.SettingsData.SfxVolume);
            SceneSystem.SetScene(new MainMenuScene());
        }

        private void ExitGame()
        {
            _dataManager.Save();
        }
    }
}