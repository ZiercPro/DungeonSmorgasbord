using RMC.Mini.Model;
using ZiercCode.DungeonSmorgasbord.Locale;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame.UI.Settings
{
    public class SettingsModel : BaseModel
    {
        public readonly ObserverValue<bool> FpsToggle;

        public readonly ObserverValue<bool> VolumePanelToggle;
        public readonly ObserverValue<bool> OtherPanelToggle;
        public readonly ObserverValue<bool> LanguagePanelToggle;

        public readonly ObserverValue<LanguageEnum> LanguageEnum;

        public readonly ObserverValue<float> MasterVolume;
        public readonly ObserverValue<float> MusicVolume;
        public readonly ObserverValue<float> SfxVolume;
        public readonly ObserverValue<float> EnvironmentVolume;


        public SettingsModel()
        {
            FpsToggle = new ObserverValue<bool>(false);
            VolumePanelToggle = new ObserverValue<bool>(false);
            OtherPanelToggle = new ObserverValue<bool>(false);
            LanguagePanelToggle = new ObserverValue<bool>(false);

            LanguageEnum = new ObserverValue<LanguageEnum>(DungeonSmorgasbord.Locale.LanguageEnum.Chinese);

            MasterVolume = new ObserverValue<float>(0f);
            MusicVolume = new ObserverValue<float>(0f);
            SfxVolume = new ObserverValue<float>(0f);
            EnvironmentVolume = new ObserverValue<float>(0f);
            
        }

      
        
    }
}