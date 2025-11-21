using UnityEngine;

namespace ZiercCode.Audio
{
    public class SfxPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        public void Play(string audioName)
        {
            AudioPlayer.Instance.PlaySfx(audioSource, audioName);
        }
    }
}