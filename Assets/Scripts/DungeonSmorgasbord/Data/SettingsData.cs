using System;
using UnityEngine;
using ZiercCode.Core.Data;
using ZiercCode.DungeonSmorgasbord.Locale;

namespace ZiercCode.DungeonSmorgasbord.Data
{
    /// <summary>
    /// 储存设置信息
    /// </summary>
    public class SettingsData : IDataStore<SettingsData>
    {
        private const string SETTING_DATA_PATH = "/settings.json";

        /// <summary>
        /// 主音量
        /// </summary>
        [Range(-70, 5)] public float MasterVolume;

        /// <summary>
        /// 音乐音量
        /// </summary>
        [Range(-60, 10)] public float MusicVolume;

        /// <summary>
        /// 音效
        /// </summary>
        [Range(-60, 10)] public float SfxVolume;

        /// <summary>
        /// 环境音量
        /// </summary>
        [Range(-40, 20)] public float EnvironmentVolume;

        /// <summary>
        /// 是否显示fps
        /// </summary>
        public bool FPSOn;

        /// <summary>
        /// 默认语言
        /// </summary>
        public LanguageEnum Language;

        /// <summary>
        /// json数据服务
        /// </summary>
        private IDataService _jsonDataService;

        /// <summary>
        /// 无参构造
        /// </summary>
        public SettingsData() : this(0f, 0f, 0f, 0f, false, LanguageEnum.Chinese)
        {
        }

        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="masterVolume">主音量</param>
        /// <param name="musicVolume">音乐音量</param>
        /// <param name="sfxVolume">音效音量</param>
        /// <param name="environmentVolume">环境音量</param>
        /// <param name="fpsOn">是否开启帧率显示</param>
        /// <param name="language">语言</param>
        public SettingsData(float masterVolume, float musicVolume, float sfxVolume, float environmentVolume, bool fpsOn,
            LanguageEnum language)
        {
            MasterVolume = masterVolume;
            MusicVolume = musicVolume;
            SfxVolume = sfxVolume;
            EnvironmentVolume = environmentVolume;
            FPSOn = fpsOn;
            Language = language;
            _jsonDataService = new JsonDataService();
        }

        public void Save()
        {
            if (_jsonDataService.SaveData(SETTING_DATA_PATH, this, false))
                Debug.Log("Settings saved!");
            else
                Debug.LogError("Settings can not save!");
        }

        public bool Load(out SettingsData data)
        {
            try
            {
                data = _jsonDataService.LoadData<SettingsData>(SETTING_DATA_PATH, false);
                Debug.Log("Settings loading complete!");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Can not load settings data due to:{e.Message};{e.StackTrace}");
                data = null;
                return false;
            }
        }
    }
}