using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Runtime.Helper;

namespace ZiercCode.Runtime.ScriptObject
{


    [CreateAssetMenu(menuName = "ScriptObj/BattleDifficulty", fileName = "BattleDifficulty")]
    public class BattleDifficultyDataSO : ScriptableObject
    {
        public TextAsset difDataFile;

        //每一层对应难度总数
        public SerDictionary<int, int> difOfLevel;

        //每层对应的波数
        public List<int> wavesOfLevel;

        //每层波生成间隔
        public List<float> intervalOfLevel;

        private void OnEnable()
        {
            if (difDataFile == null) return;

            difOfLevel = new SerDictionary<int, int>();
            wavesOfLevel = new List<int>();
            intervalOfLevel = new List<float>();

            string[] lines = difDataFile.text.Split('\n');

            for (int i = 1; i < lines.Length - 1; i++)
            {
                string[] row = lines[i].Split(',');
                difOfLevel.Add(i, int.Parse(row[0]));
                wavesOfLevel.Add(int.Parse(row[1]));
                intervalOfLevel.Add(float.Parse(row[2]));
            }
        }

        private void OnValidate()
        {
            if (difDataFile == null) return;

            difOfLevel = new SerDictionary<int, int>();
            wavesOfLevel = new List<int>();
            intervalOfLevel = new List<float>();

            string[] lines = difDataFile.text.Split('\n');

            for (int i = 1; i < lines.Length - 1; i++)
            {
                string[] row = lines[i].Split(',');
                difOfLevel.Add(i, int.Parse(row[0]));
                wavesOfLevel.Add(int.Parse(row[1]));
                intervalOfLevel.Add(float.Parse(row[2]));
            }
        }
    }
}