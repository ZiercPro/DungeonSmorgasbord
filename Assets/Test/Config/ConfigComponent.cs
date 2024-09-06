using ZiercCode.Core.Data;
using ZiercCode.DungeonSmorgasbord.Locale;
using ZiercCode.Old.Audio;
using ZiercCode.Test.Base;

namespace ZiercCode.Test.Config
{
    public class ConfigComponent : ZiercComponent
    {
        private bool _initialized;

        public void Initialize()
        {
            if (_initialized) return;

            IDataService jsonDataService = new JsonDataService();

            Settings settings = jsonDataService.LoadData<Settings>(Settings.SETTING_DATA_PATH, false);

            LocaleManager.Instance.SetLanguage(settings.Language);

            AudioPlayer.Instance.SetMasterVolume(settings.MasterVolume);
            AudioPlayer.Instance.SetMusicVolume(settings.MusicVolume);
            AudioPlayer.Instance.SetSfxVolume(settings.SfxVolume);
            AudioPlayer.Instance.SetEnvironmentVolume(settings.EnvironmentVolume);

            _initialized = true;
        }
    }
}