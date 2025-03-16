using UnityEngine;
using ZiercCode.Audio;

namespace ZiercCode.Old.Helper
{
    /// <summary>
    /// ui播放音效的辅助脚本
    /// </summary>
    public class UISoundsHelper : MonoBehaviour
    {
        //private float playDuration = 0.5f;
        //private bool playing = false;
        public void ButtonClick()
        {
            AudioPlayer.Instance.PlaySfx("ButtonClick1");
        }

        public void ButtonEnter()
        {
            AudioPlayer.Instance.PlaySfx("ButtonEnter1");
        }

        public void CardClick()
        {
            AudioPlayer.Instance.PlaySfx("CardClick1");
        }

        public void CardEnter()
        {
            AudioPlayer.Instance.PlaySfx("CardEnter1");
        }

        public void Interact()
        {
            AudioPlayer.Instance.PlaySfx("Interactive1");
        }
    }
}