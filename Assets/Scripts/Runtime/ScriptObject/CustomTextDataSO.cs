using NaughtyAttributes;
using System;
using UnityEngine;

/// <summary>
/// 自定义文本数据
/// </summary>
[CreateAssetMenu(menuName = "ScriptObj/CustomTextData", fileName = "CustomTextData")]
public class CustomTextDataSO : ScriptableObject
{
    public TextAsset customTextTableFile;
    public SerDictionary<int, CustomTextTable> customTextTable;

    private void OnEnable()
    {
        if (customTextTableFile == null) return;

        customTextTable = new SerDictionary<int, CustomTextTable>();

        string[] lines = customTextTableFile.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] rows = lines[i].Split(',');
            CustomTextTable temp = new CustomTextTable();
            temp.Chinese = rows[1];
            temp.English = rows[2];
            customTextTable.Add(int.Parse(rows[0]), temp);
        }
    }

    private void OnValidate()
    {
        if (customTextTableFile == null) return;

        customTextTable = new SerDictionary<int, CustomTextTable>();

        string[] lines = customTextTableFile.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            string[] rows = lines[i].Split(',');
            CustomTextTable temp = new CustomTextTable();
            temp.Chinese = rows[1];
            temp.English = rows[2];
            customTextTable.Add(int.Parse(rows[0]), temp);
        }
    }
}