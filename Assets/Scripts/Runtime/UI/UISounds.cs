using UnityEngine;
using ZiercCode.Runtime.Audio;

namespace ZiercCode.Runtime.UI
{
    /// <summary>
    /// ui播放音效的辅助脚本
    /// </summary>
    public class UISounds : MonoBehaviour
    {
        //private float playDuration = 0.5f;
        //private bool playing = false;
        public void ButtonClick()
        {
            AudioPlayer.Instance.PlayAudioAsync(AudioName.ButtonClick1);
        }

        public void ButtonEnter()
        {
            AudioPlayer.Instance.PlayAudioAsync(AudioName.ButtonEnter1);
        }

        public void CardClick()
        {
            AudioPlayer.Instance.PlayAudioAsync(AudioName.CardClick1);
        }

        public void CardEnter()
        {
            AudioPlayer.Instance.PlayAudioAsync(AudioName.CardEnter1);
        }

        public void Interact()
        {
            AudioPlayer.Instance.PlayAudioAsync(AudioName.Interactive1);
        }
    }
}