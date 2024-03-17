using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ƶ��Դ�������Ƶ��Ƭ�����ƺ���Դλ����Ϣ
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

