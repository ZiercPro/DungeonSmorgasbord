using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using ZiercCode.Runtime.Audio;
using ZiercCode.Runtime.Helper;
using AudioType = ZiercCode.Runtime.Audio.AudioType;
using Object = UnityEngine.Object;

namespace ZiercCode.Runtime.ScriptObject
{
    [CreateAssetMenu(menuName = "ScriptObj/AudioList", fileName = "AudioList")]
    public class AudioListSo : ScriptableObject
    {
        [field: SerializeField]
        public EditableDictionary<AudioName, AudioBase> AudioEditableDictionary
        {
            get;
            private set;
        }

        [SerializeField] private AudioClip toCreateAudioBase;
#if UNITY_EDITOR

        public void Delete()
        {
            if (AudioEditableDictionary is { Count: > 0 })
            {
                int last = AudioEditableDictionary.Count - 1;
                AudioEditableDictionary.RemoveAt(last);
            }
        }

        public void Clear()
        {
            AudioEditableDictionary?.Clear();
        }

        public void AutoConfig()
        {
            if (!toCreateAudioBase) return;
            if (AudioEditableDictionary == null) AudioEditableDictionary = new();
            AudioBase newAudioBase =
                new AudioBase(new AudioType(GetPrefabPath(toCreateAudioBase)), 1, false, false,
                    AudioPlayer.Instance.Music);
            AudioEditableDictionary.Add(AudioName.None, newAudioBase, newAudioBase.AudioType.Name);
        }

        public void ReSetConfig()
        {
            if (!toCreateAudioBase) return;
            toCreateAudioBase = null;
        }

        private string GetPrefabPath(Object prefab)
        {
            if (prefab == null)
            {
                Debug.LogWarning("prefab为空!");
                return null;
            }

            string prefabPath = AssetDatabase.GetAssetPath(prefab);
            return prefabPath;
        }
#endif
    }
}