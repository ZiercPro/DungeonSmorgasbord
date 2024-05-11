using UnityEngine;

namespace ZiercCode.Test.Script
{
    public class Summerize : MonoBehaviour
    {
        // Start is called before the first frame update
        public TextAsset textAsset;

        void Start()
        {
            SummerizeToText();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void SummerizeToText()
        {
            if (textAsset == null) return;

            string[] lines = textAsset.text.Split('\n');
            string result = "";
            for (int i = 1; i < lines.Length - 1; i++)
            {
                string[] row = lines[i].Split(',');
                result += i + $".在2023年{row[0]}月{row[1]}日发布了名为\"{row[2]}\"的推送，主要内容为\"{row[3]}\"，阅读量为{row[4]} \n";
            }
            Debug.Log(result);
        }
    }
}
