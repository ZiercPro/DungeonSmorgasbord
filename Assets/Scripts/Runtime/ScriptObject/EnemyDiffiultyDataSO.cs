using UnityEngine;

namespace ScriptObject
{
    /// <summary>
    /// 怪物难度值
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptObj/EnemyDiffiultyData", fileName = "EnemyDiffiultyData")]
    public class EnemyDiffiultyDataSO : ScriptableObject
    {
        public TextAsset enemyDiffiultyDataFile;
        public SerDictionary<string, int> enemyDiffiultyDic;

        private void OnValidate()
        {
            if (enemyDiffiultyDataFile == null) return;
            enemyDiffiultyDic = new SerDictionary<string, int>();

            string[] lines = enemyDiffiultyDataFile.text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string[] rows = lines[i].Split(',');
                enemyDiffiultyDic.Add(rows[0], int.Parse(rows[1]));
            }
        }
    }
}