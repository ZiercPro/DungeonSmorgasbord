using UnityEngine;
using UnityEngine.Audio;

namespace Runtime.Audio.Base
{
    /// <summary>
    /// 基础的音频类 储存音频的基本信息
    /// </summary>
    [System.Serializable]
    public class AudioBase
    {
        /// <summary>
        /// 音频资源
        /// </summary>
        [field: SerializeField]
        public AudioType AudioType { private set; get; }

        /// <summary>
        /// 音频音量 默认为1
        /// </summary>
        [field: SerializeField] [Range(0, 1)] public float volume = 1;

        /// <summary>
        /// 是否循环
        /// </summary>
        [field: SerializeField]
        public bool isLoop { private set; get; }

        /// <summary>
        /// 是否立即播放
        /// </summary>
        [field: SerializeField]
        public bool isPlayAwake { private set; get; }

        /// <summary>
        /// 输出类型
        /// </summary>
        [field: SerializeField]
        public AudioMixerGroup outputGroup { private set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clipSource">audio类型</param>
        /// <param name="volume">默认音量</param>
        /// <param name="isLoop">是否循环</param>
        /// <param name="isplayawake">是否在初始时播放</param>
        /// <param name="group">音频分组</param>
        public AudioBase(AudioType audioType, float volume, bool isLoop, bool isplayawake, AudioMixerGroup group)
        {
            outputGroup = group;
            this.volume = volume;
            this.isLoop = isLoop;
            AudioType = audioType;
            isPlayAwake = isplayawake;
        }
    }
}