using ZiercCode.Core.Data;
using ZiercCode.Test.Base;
using ZiercCode.Test.Config;

namespace ZiercCode.DungeonSmorgasbord.Manager
{
    /// <summary>
    ///  全局数据管理
    /// </summary>
    public class DataComponent : ZiercComponent
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        public Settings Settings;

        public void Initialize()
        {
            IDataService jsonDataService = new JsonDataService();

            Settings = jsonDataService.LoadData<Settings>(Settings.SETTING_DATA_PATH, false);
        }
    }
}