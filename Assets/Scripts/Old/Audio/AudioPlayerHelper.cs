using UnityEngine;
using ZiercCode.Audio;

namespace ZiercCode.Old.Audio
{
    public class AudioPlayerHelper : MonoBehaviour
    {
        public void PlaySfx(string audioName)
        {
            AudioPlayer.Instance.PlaySfx(audioName);
        }

        public void PlayMusic(string audioName)
        {
            AudioPlayer.Instance.PlayMusic(audioName);
        }
    }
}