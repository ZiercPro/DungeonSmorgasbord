using ZiercCode.DungeonSmorgasbord.Data;

namespace ZiercCode.DungeonSmorgasbord.Manager
{
    /// <summary>
    ///  全局数据管理
    /// </summary>
    public class DataManager
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        public static SettingsData SettingsData { get; private set; }

        public DataManager()
        {
            if (SettingsData == null)
                SettingsData = new SettingsData();
        }

        public void Load()
        {
            if (SettingsData.Load(out SettingsData result))
            {
                SettingsData = result;
            }
        }

        public void Save()
        {
            SettingsData.Save();
        }
    }
}