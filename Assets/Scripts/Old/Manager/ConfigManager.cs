using System;
using UnityEngine;
using ZiercCode.Core.Data;
using ZiercCode.DungeonSmorgasbord.Data;

namespace ZiercCode.Old.Manager
{
    /// <summary>
    ///  全局配置数据管理
    /// 提供加载不同数据的方法
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// 设置数据保存地址
        /// </summary>
        private const string SETTING_DATA_PATH = "/settings.json";

        /// <summary>
        /// 设置数据
        /// </summary>
        public static SettingsData SettingsData;

        /// <summary>
        /// json数据服务
        /// </summary>
        private static IDataService _jsonService = new JsonDataService();


        /// <summary>
        /// 加载设置信息
        /// </summary>
        public static void LoadSettings()
        {
            //默认值
            SettingsData loadedData;
            try
            {
                loadedData = _jsonService.LoadData<SettingsData>(SETTING_DATA_PATH, false);
                Debug.Log("Settings loading complete!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Can not load settings data due to:{e.Message};{e.StackTrace}");
                loadedData = default;
                //提示玩家 加载失败
            }

            SettingsData = loadedData;
        }

        /// <summary>
        /// 保存设置信息
        /// </summary>
        public static void SaveSettings()
        {
            if (_jsonService.SaveData(SETTING_DATA_PATH, SettingsData, false))
                Debug.Log("Settings saved!");
            else
                Debug.LogError("Settings can not save!");
        }
    }
}