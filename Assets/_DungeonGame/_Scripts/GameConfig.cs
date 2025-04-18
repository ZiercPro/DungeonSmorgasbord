namespace ZiercCode._DungeonGame._Scripts
{
    //游戏过程中的配置
    public struct GameConfig
    {
        /// <summary>
        /// 存档文件路径
        /// </summary>
        public const string SAVE_FILE_PATH = "/save.json";

        public const string DEFAULT_SAVE_CONFIG_PATH = "/default.json";

        /// <summary>
        ///角色资源名
        /// </summary>
        public string CurrentPlayerName;

        /// <summary>
        /// 当前武器名
        /// </summary>
        public string CurrentWeaponName;
    }
}