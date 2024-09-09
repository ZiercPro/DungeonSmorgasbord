using System;
using UnityEngine;
using ZiercCode.Core.Data;
using ZiercCode.Old.Audio;
using ZiercCode.Test.Base;
using ZiercCode.Test.Locale;

namespace ZiercCode.Test.Config
{
    public class ConfigComponent : ZiercComponent
    {
        private bool _isInitialized;

        public void Initialize()
        {
            if (_isInitialized) return;

            IDataService jsonDataService = new JsonDataService();

            try
            {
                Settings settings =
                    jsonDataService.LoadData<Settings>(Settings.SETTING_DATA_PATH, false);

                AudioPlayer.Instance.SetEnvironmentVolume(settings.EnvironmentVolume);
                AudioPlayer.Instance.SetMasterVolume(settings.MasterVolume);
                AudioPlayer.Instance.SetMusicVolume(settings.MusicVolume);
                AudioPlayer.Instance.SetSfxVolume(settings.SfxVolume);

                GameEntry.GetComponent<LocalizationComponent>().SetLanguage(settings.Language);

                _isInitialized = true;
            }
            catch (Exception e)
            {
                jsonDataService.SaveData(Settings.SETTING_DATA_PATH, new Settings(), false);
                Debug.LogWarning($"无法获取设置数据，将重新生成。{e.Message},{e.StackTrace}");
            }
        }
    }
}