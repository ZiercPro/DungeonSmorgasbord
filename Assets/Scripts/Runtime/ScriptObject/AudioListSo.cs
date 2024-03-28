using Runtime.Helper;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.ScriptObject
{
    using Audio;
    using Audio.Base;

    [CreateAssetMenu(menuName = "ScriptObj/AudioList", fileName = "AudioList")]
    public class AudioListSo : ScriptableObject
    {
        [field: SerializeField] private EditableDictionary<AudioName, AudioBase> AudioList;
        public Dictionary<AudioName, AudioBase> Audios => AudioList.ToDictionary();

        public void Add()
        {
            if (AudioList == null) AudioList = new();
            AudioBase newAudioBase =
                new AudioBase(new AudioType(""), 1, false, false, AudioPlayerManager.Instance.Music);
            AudioList.Add(AudioName.None, newAudioBase);
        }

        public void Delete()
        {
            if (AudioList is { Count: > 0 })
            {
                int last = AudioList.Count - 1;
                AudioList.RemoveAt(last);
            }
        }
    }
}