using Runtime.Audio.Base;
using System.Collections;
using UnityEngine;

namespace Runtime.UI
{
    using Audio;

    /// <summary>
    /// ui播放音效的辅助脚本
    /// </summary>
    public class UISounds : MonoBehaviour
    {
        //private float playDuration = 0.5f;
        //private bool playing = false;
        public void ButtonClick()
        {
            AudioPlayerManager.Instance.PlayAudio(AudioName.ButtonClick1);
        }

        public void ButtonEnter()
        {
            AudioPlayerManager.Instance.PlayAudio(AudioName.ButtonEnter1);
        }

        public void CardClick()
        {
            AudioPlayerManager.Instance.PlayAudio(AudioName.CardClick1);
        }

        public void CardEnter()
        {
            AudioPlayerManager.Instance.PlayAudio(AudioName.CardEnter1);
        }
    }
}