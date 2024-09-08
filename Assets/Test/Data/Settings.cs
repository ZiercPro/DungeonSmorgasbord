using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Locale;

namespace ZiercCode.Test.Data
{
    public struct Settings
    {
        /// <summary>
        /// 设置数据存储地址
        /// </summary>
        public const string SETTING_DATA_PATH = "/settings.json";

        /// <summary>
        /// 主音量
        /// </summary>
        [Range(-50, 15)] public float MasterVolume;

        /// <summary>
        /// 音乐音量
        /// </summary>
        [Range(-50, 15)] public float MusicVolume;

        /// <summary>
        /// 音效
        /// </summary>
        [Range(-50, 15)] public float SfxVolume;

        /// <summary>
        /// 环境音量
        /// </summary>
        [Range(-50, 15)] public float EnvironmentVolume;

        /// <summary>
        /// 是否显示fps
        /// </summary>
        public bool FPSOn;

        /// <summary>
        /// 默认语言
        /// </summary>
        public LanguageEnum Language;
    }
}