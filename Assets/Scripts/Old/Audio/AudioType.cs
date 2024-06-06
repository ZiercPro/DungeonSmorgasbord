using NaughtyAttributes.Scripts.Core.DrawerAttributes;
using NaughtyAttributes.Scripts.Core.MetaAttributes;
using UnityEngine;

namespace ZiercCode.Old.Audio
{
    /// <summary>
    /// 音频资源类包含音频切片的名称和资源位置信息
    /// </summary>
    [System.Serializable]
    public class AudioType
    {
        [field: SerializeField, ReadOnly, AllowNesting]
        public string Name { get; private set; }

        [field: SerializeField, ReadOnly, AllowNesting]
        public string Path { get; private set; }

        public AudioType(string path)
        {
            Path = path;
            Name = System.IO.Path.GetFileNameWithoutExtension(Path);
        }
    }
}