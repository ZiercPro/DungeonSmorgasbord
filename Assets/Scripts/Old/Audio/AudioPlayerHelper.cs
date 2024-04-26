using System;
using System.Linq;
using UnityEngine;

namespace ZiercCode.Old.Audio
{
    public class AudioPlayerHelper : MonoBehaviour
    {
        [SerializeField] private AudioName[] audioNames;
        private bool _canPlay;

        public void OnEnable()
        {
            _canPlay = true;
        }

        public void PlayAudio(int audioNameIndex)
        {
            if (!_canPlay) return;
            AudioPlayer.Instance.PlayAudioAsync(audioNames[audioNameIndex]);
        }

        public void PlayAudioRandom()
        {
            if (!_canPlay) return;
            AudioPlayer.Instance.PlayAudiosRandomAsync(audioNames.ToList());
        }

        public void Enable()
        {
            _canPlay = true;
        }

        public void Disable()
        {
            _canPlay = false;
        }
    }
}