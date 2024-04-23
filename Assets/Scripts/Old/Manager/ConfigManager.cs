using ZiercCode.DungeonSmorgasbord.Data;

namespace ZiercCode.Old.Manager
{
    /// <summary>
    ///  全局配置数据管理
    /// </summary>
    public class ConfigManager
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        public static SettingsData SettingsData { get; private set; }

        public ConfigManager()
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