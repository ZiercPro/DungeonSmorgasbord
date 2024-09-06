using RMC.Core.Architectures.Mini.Service;
using ZiercCode.Core.Data;
using ZiercCode.Test.Config;

namespace ZiercCode.Test.MVC
{
    public class SettingsService : BaseService
    {
        private Settings _settings;
        private readonly IDataService _jsonService;

        public SettingsService()
        {
            _jsonService = new JsonDataService();
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
            _jsonService.SaveData(Settings.SETTING_DATA_PATH, _settings, false);
        }
    }
}