using System;
using UnityEngine;
using ZiercCode.Runtime.Data;

namespace ZiercCode.Runtime.Manager
{
    /// <summary>
    ///  数据管理器
    /// 提供加载不同数据的方法
    /// </summary>
    public class DataManager
    {
        /// <summary>
        /// 设置数据保存地址
        /// </summary>
        private const string SETTING_DATA_PATH = "/settings.json";

        /// <summary>
        /// 设置数据
        /// </summary>
        public static SettingsData SettingsData { get; private set; }

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
            SettingsData loadedData = null;
            try
            {
                loadedData = _jsonService.LoadData<SettingsData>(SETTING_DATA_PATH, false);
                Debug.Log("Settings loading complete!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Can not load settings data due to:{e.Message};{e.StackTrace}");
                loadedData = null;
                //提示玩家 加载失败
            }

            if (loadedData == null) loadedData = new SettingsData();
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