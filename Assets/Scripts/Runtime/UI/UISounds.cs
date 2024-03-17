using System.Collections;
using UnityEngine;

/// <summary>
/// ui播放音效的辅助脚本
/// </summary>
public class UISounds : MonoBehaviour
{
    //private float playDuration = 0.5f;
    //private bool playing = false;
    public void ButtonClick()
    {
        AudioPlayerManager.Instance.PlayAudio(Audios.buttonClick);
    }
    public void ButtonEnter()
    {
        AudioPlayerManager.Instance.PlayAudio(Audios.buttonEnter);
    }
    public void CardClick()
    {
        AudioPlayerManager.Instance.PlayAudio(Audios.cardClick);
    }
    public void CardEnter()
    {
        AudioPlayerManager.Instance.PlayAudio(Audios.cardEnter);
    }

}
