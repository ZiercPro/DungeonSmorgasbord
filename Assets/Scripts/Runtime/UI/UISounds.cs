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
        AudioPlayerManager.Instance.PlayAudio(GameRoot.Instance.AudioList.buttonClick);
    }
    public void ButtonEnter()
    {
        AudioPlayerManager.Instance.PlayAudio(GameRoot.Instance.AudioList.buttonEnter);
    }
    public void CardClick()
    {
        AudioPlayerManager.Instance.PlayAudio(GameRoot.Instance.AudioList.cardClick);
    }
    public void CardEnter()
    {
        AudioPlayerManager.Instance.PlayAudio(GameRoot.Instance.AudioList.cardEnter);
    }

}
