using RMC.Core.Architectures.Mini.Service;
using ZiercCode.DungeonSmorgasbord.Manager;

namespace ZiercCode.Test.MVC
{
    public class SettingsService : BaseService
    {
        public void Load()
        {
            SettingsModel settingsModel = Context.ModelLocator.GetItem<SettingsModel>();

            settingsModel.FpsToggle.Value = DataManager.SettingsData.FPSOn;
            settingsModel.LanguageEnum.Value = DataManager.SettingsData.Language;
            settingsModel.MasterVolume.Value = DataManager.SettingsData.MasterVolume;
            settingsModel.MusicVolume.Value = DataManager.SettingsData.MusicVolume;
            settingsModel.SfxVolume.Value = DataManager.SettingsData.SfxVolume;
            settingsModel.EnvironmentVolume.Value = DataManager.SettingsData.EnvironmentVolume;
        }
    }
}