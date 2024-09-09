using RMC.Core.Architectures.Mini.Service;
using ZiercCode.Core.Data;
using ZiercCode.Test.Base;
using ZiercCode.Test.Config;
using ZiercCode.Test.Data;
using ZiercCode.Test.Locale;

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

        //从文件中加载设置数据
        public void Load()
        {
            _settings = _jsonService.LoadData<Settings>(Settings.SETTING_DATA_PATH, false);

            _settingsView.Fps.isOn = _settings.FPSOn;
            _settingsView.LanguageDropdown.value = (int)_settings.Language;
            _settingsView.MasterVolume.value = _settings.MasterVolume;
            _settingsView.MusicVolume.value = _settings.MusicVolume;
            _settingsView.SfxVolume.value = _settings.SfxVolume;
            _settingsView.EnvironmentVolume.value = _settings.EnvironmentVolume;
        }

        //将model数据写入文件
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
        }
    }
}