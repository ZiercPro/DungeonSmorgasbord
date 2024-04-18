using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Runtime.Helper;

namespace ZiercCode.Runtime.ScriptObject
{
    /// <summary>
    /// 自定义文本数据
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptObj/CustomTextData", fileName = "CustomTextData")]
    public class CustomTextDataSo : ScriptableObject
    {
        [SerializeField] private TextAsset customTextTableFile;
        [field: SerializeField] public EditableDictionary<int, CustomTextTable> CustomTextTable { get; private set; }


        private void OnValidate()
        {
            if (customTextTableFile == null) return;

            CustomTextTable = new EditableDictionary<int, CustomTextTable>();

            string[] lines = customTextTableFile.text.Split('\n');
            for (int i = 1; i < lines.Length; i++)
            {
                string[] rows = lines[i].Split(',');
                CustomTextTable temp = new CustomTextTable();
                temp.Chinese = rows[1];
                temp.English = rows[2];
                CustomTextTable.Add(int.Parse(rows[0]), temp);
            }
        }
    }
}