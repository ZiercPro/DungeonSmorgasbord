using UnityEngine;
using ZiercCode.Audio;

namespace ZiercCode
{
    public class SFXPlayer : MonoBehaviour
    {
        public void Play(string audioName)
        {
            AudioPlayer.Instance.PlaySfx(audioName);
        }
    }
}