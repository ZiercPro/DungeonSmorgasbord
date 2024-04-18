using UnityEditor;
using UnityEngine;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Helper;
using AudioType = ZiercCode.Old.Audio.AudioType;
using Object = UnityEngine.Object;

namespace ZiercCode.Old.ScriptObject
{
    [CreateAssetMenu(menuName = "ScriptObj/AudioList", fileName = "AudioList")]
    public class AudioListSo : ScriptableObject
    {
        [SerializeField] private EditableDictionary<AudioName, AudioBase> audioEditableDictionary;
        public EditableDictionary<AudioName, AudioBase> AudioEditableDictionary => audioEditableDictionary;

        [field: SerializeField] private AudioClip toCreateAudioBase;
#if UNITY_EDITOR

        public void Delete()
        {
            if (audioEditableDictionary is { Count: > 0 })
            {
                int last = audioEditableDictionary.Count - 1;
                audioEditableDictionary.RemoveAt(last);
            }
        }

        public void Clear()
        {
            audioEditableDictionary?.Clear();
        }

        public void AutoConfig()
        {
            if (!toCreateAudioBase) return;
            if (audioEditableDictionary == null) audioEditableDictionary = new();
            AudioBase newAudioBase =
                new AudioBase(new AudioType(GetPrefabPath(toCreateAudioBase)), 1, false, false,
                   null);
            audioEditableDictionary.Add(AudioName.None, newAudioBase, newAudioBase.AudioType.Name);
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