using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// 基础的音频类 储存音频的基本信息
/// </summary>
public class AudioBase
{
    /// <summary>
    /// 音频资源
    /// </summary>
    public AudioType audioType { private set; get; }

    /// <summary>
    /// 音频音量 默认为1
    /// </summary>
    [Range(0, 1)]
    public float volume = 1;

    /// <summary>
    /// 是否循环
    /// </summary>
    public bool isLoop { private set; get; }

    /// <summary>
    /// 是否立即播放
    /// </summary>
    public bool isPlayAwake { private set; get; }

    /// <summary>
    /// 输出类型
    /// </summary>
    public AudioMixerGroup outputGroup { private set; get; }
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type"></param>
    public AudioBase(AudioType type, float volume, bool isLoop, bool isplayawake, AudioMixerGroup group)
    {
        audioType = type;
        this.volume = volume;
        this.isLoop = isLoop;
        outputGroup = group;
        isPlayAwake = isplayawake;
    }
}




