using UnityEngine;

namespace ZiercCode.Audio
{
    /// <summary>
    /// 随机播放音效
    /// </summary>
    public class SfxRandomPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource sfxPlayer;

        [SerializeField]
        private AudioClip[] shootSfx; //射击音效 随机播放

        [SerializeField]
        private float clipVolume; //音频资源声音

        public void PlayRandomSfx()
        {
            AudioPlayer.Instance.PlaySfx(sfxPlayer, shootSfx[Random.Range(0, shootSfx.Length)], clipVolume);
        }
    }
}