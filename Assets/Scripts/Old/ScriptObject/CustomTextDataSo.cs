using UnityEngine;
using ZiercCode.Core.Extend;
using ZiercCode.Core.Utilities;
using ZiercCode.Old.Helper;

namespace ZiercCode.Old.ScriptObject
{
    /// <summary>
    /// 自定义文本数据
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/CustomTextData", fileName = "CustomTextData")]
    public class CustomTextDataSo : ScriptableObject
    {
        [SerializeField] private TextAsset customTextTableFile;
        [field: SerializeField] public EditableDictionary<int, CustomTextTable> CustomTextDictionary { get; private set; }


        private void OnValidate()
        {
            if (customTextTableFile == null) return;

            CustomTextDictionary = new EditableDictionary<int, CustomTextTable>();

            string[] lines = customTextTableFile.text.Split('\n');
            for (int i = 1; i < lines.Length; i++)
            {
                string[] rows = lines[i].Split(',');
                CustomTextTable temp = new CustomTextTable();
                temp.chinese = rows[1];
                temp.english = rows[2];
                CustomTextDictionary.Add(int.Parse(rows[0]), temp);
            }
        }
    }
}