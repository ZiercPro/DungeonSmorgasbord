using ZiercCode.DungeonSmorgasbord.Data;

namespace ZiercCode.DungeonSmorgasbord.Manager
{
    /// <summary>
    ///  全局数据管理
    /// </summary>
    public static class DataManager
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        public static SettingsData SettingsData { get; private set; } = new SettingsData();

        public static void Initialize()
        {
            Load();
        }

        private static void Load()
        {
            if (SettingsData.Load(out SettingsData result))
                SettingsData = result;
        }

        private static void Save()
        {
            SettingsData.Save();
        }
    }
}