using UnityEngine;

namespace ZiercCode.Old.Data
{
    /// <summary>
    /// 储存设置信息
    /// </summary>
    public struct SettingsData
    {
        /// <summary>
        /// 主音量
        /// </summary>
        [Range(-70, 5)] public float MasterVolume;

        /// <summary>
        /// 音乐音量
        /// </summary>
        [Range(-60, 10)] public float MusicVolume;

        /// <summary>
        /// 音效
        /// </summary>
        [Range(-60, 10)] public float SFXVolume;

        /// <summary>
        /// 环境音量
        /// </summary>
        [Range(-40, 20)] public float EnvironmentVolume;

        /// <summary>
        /// 是否显示fps
        /// </summary>
        public bool FPSOn;

        /// <summary>
        /// 默认语言
        /// </summary>
        public int Language;
    }
}