using NaughtyAttributes.Scripts.Core.MetaAttributes;
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
        [SerializeField] private bool loadFromFile;

        [SerializeField, ShowIf("loadFromFile")]
        private TextAsset customTextTableFile;

        [field: SerializeField]
        public EditableDictionary<string, CustomTextTable> CustomTextDictionary { get; private set; }

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (!loadFromFile) return;
            if (customTextTableFile == null) return;

            CustomTextDictionary = new EditableDictionary<string, CustomTextTable>();

            string[] lines = customTextTableFile.text.Split('\n');
            for (int i = 1; i < lines.Length; i++)
            {
                string[] rows = lines[i].Split(',');
                CustomTextTable temp = new CustomTextTable();
                temp.chinese = rows[1];
                temp.english = rows[2];
                CustomTextDictionary.Add(rows[0], temp);
            }
#endif
        }
    }
}