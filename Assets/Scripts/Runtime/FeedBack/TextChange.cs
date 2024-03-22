using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.FeedBack
{
    public class TextChange : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private string originalT;

        private string targetT;

        //修饰用的字符
        private readonly static string s_modifier = "-";

        private void Awake()
        {
            UpdateText();
        }

        private void ToggleState()
        {
            if (TryGetComponent(out Toggle tgl))
            {
                if (tgl.isOn)
                {
                    Change();
                }
                else
                {
                    Recover();
                }
            }
        }

        private void OnDisable()
        {
            Recover();
            ToggleState();
        }

        public void Change()
        {
            text.text = targetT;
        }

        public void Recover()
        {
            text.text = originalT;
        }

        public void TextSwitch(bool tigger)
        {
            if (tigger) Change();
            else Recover();
        }

        //语言更新时调用
        public void UpdateText()
        {
            text = GetComponentInChildren<TextMeshProUGUI>(true);
            originalT = text.text;
            if (originalT.Contains(s_modifier))
            {
                string[] temp = originalT.Split(s_modifier);
                originalT = temp[1];
            }

            targetT = s_modifier + originalT + s_modifier;

            ToggleState();
        }
    }
}