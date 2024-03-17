using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// ��������Ƶ�� ������Ƶ�Ļ�����Ϣ
/// </summary>
public class AudioBase
{
    /// <summary>
    /// ��Ƶ��Դ
    /// </summary>
    public AudioType audioType { private set; get; }

    /// <summary>
    /// ��Ƶ���� Ĭ��Ϊ1
    /// </summary>
    [Range(0, 1)]
    public float volume = 1;

    /// <summary>
    /// �Ƿ�ѭ��
    /// </summary>
    public bool isLoop { private set; get; }

    /// <summary>
    /// �Ƿ���������
    /// </summary>
    public bool isPlayAwake { private set; get; }

    /// <summary>
    /// �������
    /// </summary>
    public AudioMixerGroup outputGroup { private set; get; }
    /// <summary>
    /// ���캯��
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




