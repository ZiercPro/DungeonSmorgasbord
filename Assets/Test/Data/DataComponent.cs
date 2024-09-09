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
           
        }
    }
}