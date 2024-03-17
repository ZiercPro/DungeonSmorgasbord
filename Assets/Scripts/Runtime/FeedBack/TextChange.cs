using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextChange : MonoBehaviour
{
    private TextMeshProUGUI text;
    private string originalT;
    private string targetT;
    //�����õ��ַ�
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
    //���Ը���ʱ����
    public void UpdateText()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
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
