using UnityEngine;

namespace Runtime.Audio.Base
{
    /// <summary>
    /// 音频资源类包含音频切片的名称和资源位置信息
    /// </summary>
    [System.Serializable]
    public class AudioType
    {
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        [field: SerializeField, TextArea(2, 6)]
        public string Path { get; private set; }

        public AudioType(string path)
        {
            Path = path;
        }
    }
}