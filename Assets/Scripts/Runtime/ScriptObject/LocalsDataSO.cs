using UnityEngine;
/// <summary>
/// 储存语言对应编号
/// </summary>

[CreateAssetMenu(menuName = "ScriptObj/LocalID", fileName = "LocalID")]
public class LocalsDataSO : ScriptableObject
{
    public TextAsset LanguageIndexData;
    public SerDictionary<string, int> localsIDTable;

    private void OnValidate()
    {
        if (LanguageIndexData == null) return;

        if (localsIDTable != null) localsIDTable.Clear();

        string[] lines = LanguageIndexData.text.Split('\n');
        for (int i = 0; i < lines.Length - 1; i++)
        {
            string[] rows = lines[i].Split(',');
            localsIDTable.Add(rows[0], int.Parse(rows[1]));
        }
    }
}
