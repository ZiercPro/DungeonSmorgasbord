using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{
    //语言id数据
    [SerializeField] private LocalsDataSO _localsID;

    //卡片文本数据
    [SerializeField] private CustomTextDataSO cardTextDataSo;

    public void SetLanguage(int language)
    {
        StartCoroutine(SetLocal(language));
    }

    public void SetLanguage(string language)
    {
        int id = _localsID.localsIDTable[language];
        StartCoroutine(SetLocal(id));
    }

    public LocalsDataSO GetLocalID()
    {
        return _localsID;
    }

    public string GetCardText(int itemId)
    {
        int languageIDd = GetSelectedLocalID();
        switch (languageIDd)
        {
            case 0:
                //中文
                return cardTextDataSo.customTextTable[itemId].Chinese;
            case 1:
                //English
                return cardTextDataSo.customTextTable[itemId].English;
            default:
                Debug.LogError("id不存在");
                break;
        }
        Debug.LogError("无法获取文本!");
        return null;
    }

    private IEnumerator SetLocal(int languageID)
    {
        if (LocalizationSettings.Instance.GetSelectedLocale() ==
            LocalizationSettings.AvailableLocales.Locales[languageID])
            yield break;

        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageID];
    }

    /// <summary>
    /// 获取当前所选语言id
    /// </summary>
    /// <returns>id</returns>
    public static int GetSelectedLocalID()
    {
        int result = 0;
        foreach (var local in LocalizationSettings.AvailableLocales.Locales)
        {
            if (LocalizationSettings.Instance.GetSelectedLocale() == local)
                break;
            else result++;
        }

        return result;
    }
}