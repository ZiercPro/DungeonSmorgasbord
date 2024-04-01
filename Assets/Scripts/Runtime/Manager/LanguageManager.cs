using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using ZiercCode.Runtime.ScriptObject;

namespace ZiercCode.Runtime.Manager
{

    public class LanguageManager : MonoBehaviour
    {
        //语言id数据
        [SerializeField] private LocalsDataSO _localsID;

        //卡片文本数据
        [SerializeField] private CustomTextDataSO cardTextDataSo;


        private void Awake()
        {
            Debug.Log(_localsID.localsIDTable.Count);
            Debug.Log(_localsID.LanguageIndexData);
        }

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
            {
                // Debug.Log("已经选择该语言");
                yield break;
            }

            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageID];
            // Debug.Log("语言切换完毕");
        }

        /// <summary>
        /// 获取当前所选语言id
        /// </summary>
        /// <returns>id</returns>
        public static int GetSelectedLocalID()
        {
            int result = 0;
            foreach (Locale locale in LocalizationSettings.AvailableLocales.Locales)
            {
                if (LocalizationSettings.Instance.GetSelectedLocale() == locale)
                    break;
                else result++;
            }

            return result;
        }
    }
}