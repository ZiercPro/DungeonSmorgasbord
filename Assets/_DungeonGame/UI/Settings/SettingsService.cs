using RMC.Mini.Service;
using ZiercCode._DungeonGame.Config;

namespace ZiercCode._DungeonGame.UI.Settings
{
    public class SettingsService : BaseService
    {
        private GameSettings _gameSettings;
        private readonly SettingsView _settingsView;

        public SettingsService(SettingsView view)
        {
            _settingsView = view;
        }

        //从文件中加载设置数据 同步到视图？ 应该是同步到model
        public void Load()
        {
            _gameSettings = ConfigComponent.Instance.GameSettings;

            _settingsView.Fps.isOn = _gameSettings.FPSOn;
            _settingsView.LanguageDropdown.value = (int)_gameSettings.Language;
            _settingsView.MasterVolume.value = _gameSettings.MasterVolume;
            _settingsView.MusicVolume.value = _gameSettings.MusicVolume;
            _settingsView.SfxVolume.value = _gameSettings.SfxVolume;
            _settingsView.EnvironmentVolume.value = _gameSettings.EnvironmentVolume;
        }

        //将model数据写入文件
        public void Save()
        {
            SettingsModel settingsModel = Context.ModelLocator.GetItem<SettingsModel>();

            _gameSettings.FPSOn = settingsModel.FpsToggle.Value;
            _gameSettings.Language = settingsModel.LanguageEnum.Value;
            _gameSettings.MasterVolume = settingsModel.MasterVolume.Value;
            _gameSettings.MusicVolume = settingsModel.MusicVolume.Value;
            _gameSettings.SfxVolume = settingsModel.SfxVolume.Value;
            _gameSettings.EnvironmentVolume = settingsModel.EnvironmentVolume.Value;

            ConfigComponent.Instance.SaveGameSettings(_gameSettings);
        }
    }
}