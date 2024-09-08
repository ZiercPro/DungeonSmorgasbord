using RMC.Core.Architectures.Mini.Service;
using ZiercCode.Core.Data;
using ZiercCode.Test.Data;

namespace ZiercCode.Test.MVC
{
    public class SettingsService : BaseService
    {
        private Settings _settings;
        private readonly IDataService _jsonService;
        private readonly SettingsView _settingsView;

        public SettingsService(SettingsView view)
        {
            _jsonService = new JsonDataService();
            _settingsView = view;
        }

        public void Load()
        {
            SettingsModel settingsModel = Context.ModelLocator.GetItem<SettingsModel>();

            _settings = _jsonService.LoadData<Settings>(Settings.SETTING_DATA_PATH, false);

            settingsModel.FpsToggle.Value = _settings.FPSOn;
            settingsModel.LanguageEnum.Value = _settings.Language;
            settingsModel.MasterVolume.Value = _settings.MasterVolume;
            settingsModel.MusicVolume.Value = _settings.MusicVolume;
            settingsModel.SfxVolume.Value = _settings.SfxVolume;
            settingsModel.EnvironmentVolume.Value = _settings.EnvironmentVolume;
        }

        public void Save()
        {
            SettingsModel settingsModel = Context.ModelLocator.GetItem<SettingsModel>();
            _settings.FPSOn = settingsModel.FpsToggle.Value;
            _settings.Language = settingsModel.LanguageEnum.Value;
            _settings.MasterVolume = settingsModel.MasterVolume.Value;
            _settings.MusicVolume = settingsModel.MusicVolume.Value;
            _settings.SfxVolume = settingsModel.SfxVolume.Value;
            _settings.EnvironmentVolume = settingsModel.EnvironmentVolume.Value;

            _jsonService.SaveData(Settings.SETTING_DATA_PATH, _settings, false);

            _settingsView.settingsChanged.Value = false;
        }
    }
}