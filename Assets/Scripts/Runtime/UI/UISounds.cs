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
            AudioPlayerManager.Instance.PlayAudioAsync(AudioName.ButtonClick1);
        }

        public void ButtonEnter()
        {
            AudioPlayerManager.Instance.PlayAudioAsync(AudioName.ButtonEnter1);
        }

        public void CardClick()
        {
            AudioPlayerManager.Instance.PlayAudioAsync(AudioName.CardClick1);
        }

        public void CardEnter()
        {
            AudioPlayerManager.Instance.PlayAudioAsync(AudioName.CardEnter1);
        }
    }
}