using UnityEngine;
using ZiercCode.Core.Extend;
using ZiercCode.Old.Helper;

namespace ZiercCode.Old.ScriptObject
{
    /// <summary>
    /// 怪物难度值
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptObj/EnemyDifficultyData", fileName = "EnemyDifficultyData")]
    public class EnemyDifficultyDataSo : ScriptableObject
    {
        [SerializeField] private TextAsset enemyDifficultyDataFile;
        [field: SerializeField] public EditableDictionary<string, int> EnemyDifficultyDictionary { get; private set; }


        private void OnValidate()
        {
            if (enemyDifficultyDataFile == null) return;
            EnemyDifficultyDictionary = new EditableDictionary<string, int>();

            string[] lines = enemyDifficultyDataFile.text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string[] rows = lines[i].Split(',');
                EnemyDifficultyDictionary.Add(rows[0], int.Parse(rows[1]));
            }
        }
    }
}