using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频资源类包含音频切片的名称和资源位置信息
/// </summary>
public class AudioType
{
    public string Name { get; private set; }
    public string Path { get; private set; }

    public AudioType(string path)
    {
        Path = path;
        Name = path.Substring(path.LastIndexOf('/') + 1);
    }

}

