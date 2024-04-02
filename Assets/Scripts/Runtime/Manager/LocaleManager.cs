using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using ZiercCode.Runtime.Basic;
using ZiercCode.Runtime.Helper;
using ZiercCode.Runtime.ScriptObject;

namespace ZiercCode.Runtime.Manager
{
    public class LocaleManager : USingletonComponentDontDestroy<LocaleManager>
    {
        //语言id数据
        [SerializeField] private LocalsDataSo localsID;

        //自定义文本数据
        [SerializeField] private EditableDictionary<string, CustomTextDataSo> customTextTables;

        private Dictionary<string, int> _localesIdDictionary;
        private static Dictionary<int, CustomTextTable> _cardTextTable;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            _localesIdDictionary = localsID.LocalsIDTable.ToDictionary();
            _cardTextTable = customTextTables["CardText"].CustomTextTable.ToDictionary();
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

        public static string GetCardText(int itemId)
        {
            int languageIDd = GetSelectedLocalID();
            switch (languageIDd)
            {
                case 0:
                    //中文
                    return _cardTextTable[itemId].Chinese;
                case 1:
                    //English
                    return _cardTextTable[itemId].English;
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