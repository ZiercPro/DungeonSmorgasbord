using UnityEngine;
using ZiercCode.Core.Utilities;

namespace ZiercCode.Old.ScriptObject
{
    /// <summary>
    /// 储存语言对应编号
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/LocalID", fileName = "LocalID")]
    public class LocalsDataSo : ScriptableObject
    {
        [SerializeField] private TextAsset languageIndexData;
        [field: SerializeField] public EditableDictionary<string, int> LocalsIDTable { get; private set; }


        private void OnValidate()
        {
            if (languageIndexData == null) return;

            LocalsIDTable = new EditableDictionary<string, int>();
            string[] lines = languageIndexData.text.Split('\n');
            for (int i = 0; i < lines.Length - 1; i++)
            {
                string[] rows = lines[i].Split(',');
                LocalsIDTable.Add(rows[0], int.Parse(rows[1]), rows[0]);
            }
        }
    }
}