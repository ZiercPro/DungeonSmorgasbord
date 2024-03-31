using Runtime.Helper;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.ScriptObject
{
    using Audio;
    using Audio.Base;

    [CreateAssetMenu(menuName = "ScriptObj/AudioList", fileName = "AudioList")]
    public class AudioListSo : ScriptableObject
    {
        [field: SerializeField] private EditableDictionary<AudioName, AudioBase> audioDic;
        [SerializeField] private AudioClip toCreateAudioBase;
        public Dictionary<AudioName, AudioBase> Audios => audioDic.ToDictionary();
#if UNITY_EDITOR

        public void Delete()
        {
            if (audioDic is { Count: > 0 })
            {
                int last = audioDic.Count - 1;
                audioDic.RemoveAt(last);
            }
        }

        public void Clear()
        {
            audioDic?.Clear();
        }

        public void AutoConfig()
        {
            if (!toCreateAudioBase) return;
            if (audioDic == null) audioDic = new();
            AudioBase newAudioBase =
                new AudioBase(new AudioType(GetPrefabPath(toCreateAudioBase)), 1, false, false,
                    AudioPlayerManager.Instance.Music);
            audioDic.Add(AudioName.None, newAudioBase, newAudioBase.AudioType.Name);
        }

        public void ReSetConfig()
        {
            if (!toCreateAudioBase) return;
            toCreateAudioBase = null;
        }

        public string GetPrefabPath(Object prefab)
        {
            if (prefab == null)
            {
                Debug.LogWarning("prefab为空!");
                return null;
            }

            string prefabPath = AssetDatabase.GetAssetPath(prefab);
            return prefabPath;
        }

        public struct AudioConfig
        {
            public string Name;
            public EditableDictionaryItem<AudioType, AudioSource> AudioItem;
        }
#endif
    }
}