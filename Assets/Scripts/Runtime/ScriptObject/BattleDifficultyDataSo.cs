using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using ZiercCode.Runtime.Helper;

namespace ZiercCode.Runtime.ScriptObject
{
    [CreateAssetMenu(menuName = "ScriptObj/BattleDifficulty", fileName = "BattleDifficulty")]
    public class BattleDifficultyDataSo : ScriptableObject
    {
        [SerializeField] private TextAsset difDataFile;

        //每一层对应难度总数
        [SerializeField] private EditableDictionary<int, int> difficultyPerLevel;

        private Dictionary<int, int> _difficultyPerLevel;
        private bool _initialized;

        public Dictionary<int, int> DifficultyPerLevel
        {
            get
            {
                if (!_initialized)
                {
                    _difficultyPerLevel = difficultyPerLevel.ToDictionary();
                    _initialized = true;
                }

                return _difficultyPerLevel;
            }
        }

        //每层对应的波数
        public List<int> wavesOfLevel;

        //每层波生成间隔
        public List<float> intervalOfLevel;
        

        private void OnValidate()
        {
            _initialized = false;
            if (difDataFile == null) return;

            difficultyPerLevel = new EditableDictionary<int, int>();
            wavesOfLevel = new List<int>();
            intervalOfLevel = new List<float>();

            string[] lines = difDataFile.text.Split('\n');

            for (int i = 1; i < lines.Length - 1; i++)
            {
                string[] row = lines[i].Split(',');
                difficultyPerLevel.Add(i, int.Parse(row[0]));
                wavesOfLevel.Add(int.Parse(row[1]));
                intervalOfLevel.Add(float.Parse(row[2]));
            }
        }
    }
}