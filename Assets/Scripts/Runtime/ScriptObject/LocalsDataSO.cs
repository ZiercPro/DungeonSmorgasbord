using System;
using UnityEngine;

/// <summary>
/// 储存语言对应编号
/// </summary>
[CreateAssetMenu(menuName = "ScriptObj/LocalID", fileName = "LocalID")]
public class LocalsDataSO : ScriptableObject
{
    [field: SerializeField] public TextAsset LanguageIndexData { get; private set; }
    [field: SerializeField] public SerDictionary<string, int> localsIDTable { get; private set; }

    private void OnEnable()
    {
        if (LanguageIndexData == null) return;

        localsIDTable = new SerDictionary<string, int>();
        string[] lines = LanguageIndexData.text.Split('\n');
        for (int i = 0; i < lines.Length - 1; i++)
        {
            string[] rows = lines[i].Split(',');
            localsIDTable.Add(rows[0], int.Parse(rows[1]));
        }
    }

    private void OnValidate()
    {
        if (LanguageIndexData == null) return;

        localsIDTable = new SerDictionary<string, int>();
        string[] lines = LanguageIndexData.text.Split('\n');
        for (int i = 0; i < lines.Length - 1; i++)
        {
            string[] rows = lines[i].Split(',');
            localsIDTable.Add(rows[0], int.Parse(rows[1]));
        }
    }
}