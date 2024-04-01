using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using ZiercCode.Runtime.ScriptObject;

namespace ZiercCode.Runtime.Manager
{
    public class LanguageManager : MonoBehaviour
    {
        //语言id数据
        [SerializeField] private LocalsDataSo localsID;
        private Dictionary<string, int> _localesIdDictionary;

        //卡片文本数据
        [SerializeField] private CustomTextDataSo cardTextDataSo;

        private void Awake()
        {
            _localesIdDictionary = localsID.LocalsIDTable.ToDictionary();
        }

        public void SetLanguage(int language)
        {
            StartCoroutine(SetLocal(language));
        }

        public void SetLanguage(string language)
        {
            int id = _localesIdDictionary[language];
            StartCoroutine(SetLocal(id));
        }

        public LocalsDataSo GetLocalID()
        {
            return localsID;
        }

        public string GetCardText(int itemId)
        {
            int languageIDd = GetSelectedLocalID();
            switch (languageIDd)
            {
                case 0:
                    //中文
                    return cardTextDataSo.CustomTextTable[itemId].Chinese;
                case 1:
                    //English
                    return cardTextDataSo.CustomTextTable[itemId].English;
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