using System;
using UnityEngine;
using ZiercCode.Core.Data;
using ZiercCode.Test.Base;
using ZiercCode.Test.Config;

namespace ZiercCode.Test.Data
{
    /// <summary>
    ///  全局数据管理
    /// </summary>
    public class DataComponent : ZiercComponent
    {
        private IDataService _jsonDataService;

        public void Initialize()
        {
            _jsonDataService = new JsonDataService();

            try
            {
                _jsonDataService.LoadData<Settings>(Settings.SETTING_DATA_PATH, false);
            }
            catch (Exception e)
            {
                _jsonDataService.SaveData(Settings.SETTING_DATA_PATH, new Settings(), false);
                Debug.LogWarning("无法获取设置数据，将重新生成");
            }
        }
    }
}