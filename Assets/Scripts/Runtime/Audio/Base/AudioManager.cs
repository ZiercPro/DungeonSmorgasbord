using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频管理器
/// 帮助获取音频切片资源、加载到字典中并生成AudioBase
/// </summary>
public class AudioManager
{
    /// <summary>
    ///储存已经读取过的音频
    /// </summary>
    public Dictionary<AudioType, AudioSource> audioDic { get; private set; }

    /// <summary>
    /// 储存所以读取过的音频切片
    /// </summary>
    private Dictionary<AudioType, AudioClip> clips;

    public AudioManager()
    {
        audioDic = new Dictionary<AudioType, AudioSource>();
        clips = new Dictionary<AudioType, AudioClip>();
    }
    /// <summary>
    /// 通过audiobase创建新的音频
    /// </summary>
    /// <param name="audiosource"></param>
    /// <returns></returns>
    public AudioSource GetAudioSource(AudioBase audiosource)
    {
        GameObject parent = GameObject.Find("Audios");
        if (parent == null)
            parent = new GameObject("Audios");

        GameObject newAudioS = null;
        AudioClip newClip = null;
        AudioSource newComponent = null;

        if (clips.ContainsKey(audiosource.audioType))
            newClip = clips[audiosource.audioType];
        else
        {
            newClip = GameObject.Instantiate(Resources.Load<AudioClip>(audiosource.audioType.Path));
            clips.Add(audiosource.audioType, newClip);
        }

        if (newClip == null)
        {
            Debug.LogError($"资源 {audiosource.audioType.Name} 加载失败");
            return null;
        }

        if (audioDic.ContainsKey(audiosource.audioType))
        {
            if (audioDic[audiosource.audioType] == null)
            {
                newAudioS = new GameObject(audiosource.audioType.Name);
                newAudioS.transform.SetParent(parent.transform);
                newComponent = newAudioS.AddComponent<AudioSource>();

                newComponent.clip = newClip;
                newComponent.playOnAwake = audiosource.isPlayAwake;
                newComponent.loop = audiosource.isLoop;
                newComponent.volume = audiosource.volume;
                newComponent.outputAudioMixerGroup = audiosource.outputGroup;

                audioDic[audiosource.audioType] = newComponent;
            }
            return audioDic[audiosource.audioType];
        }

        newAudioS = new GameObject(audiosource.audioType.Name);
        newAudioS.transform.SetParent(parent.transform);
        newComponent = newAudioS.AddComponent<AudioSource>();

        newComponent.clip = newClip;
        newComponent.playOnAwake = audiosource.isPlayAwake;
        newComponent.loop = audiosource.isLoop;
        newComponent.volume = audiosource.volume;
        newComponent.outputAudioMixerGroup = audiosource.outputGroup;

        audioDic.Add(audiosource.audioType, newComponent);

        return newComponent;
    }

}
