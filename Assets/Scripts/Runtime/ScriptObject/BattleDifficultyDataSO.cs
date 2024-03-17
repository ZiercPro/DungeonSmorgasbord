using System.Collections.Generic;
using UnityEngine;

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


    private void OnValidate()
    {
        if (difDataFile == null) return;

        if (difOfLevel != null)
            difOfLevel.Clear();

        if (wavesOfLevel != null)
            wavesOfLevel.Clear();

        if (intervalOfLevel != null)
            intervalOfLevel.Clear();

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