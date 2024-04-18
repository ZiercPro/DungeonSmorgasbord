using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using ZiercCode.Old.Basic;
using ZiercCode.Old.Helper;
using ZiercCode.Old.ScriptObject;

namespace ZiercCode.Old.Manager
{
    public class LocaleManager : USingletonComponentDontDestroy<LocaleManager>
    {
        /// <summary>
        /// 本地化语言id数据
        /// </summary>
        [SerializeField] private LocalsDataSo localsID;

        /// <summary>
        /// 自定义文本本地化数据
        /// </summary>
        [SerializeField] private EditableDictionary<string, CustomTextDataSo> customTextTables;

        /// <summary>
        /// 本地化语言对应id 字典
        /// </summary>
        private Dictionary<string, int> _localesIdDictionary;

        /// <summary>
        /// 卡片文本数据字典
        /// </summary>
        private static Dictionary<int, CustomTextTable> _cardTextTable;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            _localesIdDictionary = localsID.LocalsIDTable.ToDictionary();
            _cardTextTable = customTextTables["CardText"].CustomTextTable.ToDictionary();
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="language">语言名称</param>
        public void SetLanguage(int language)
        {
            StartCoroutine(SetLocal(language));
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="language">语言id</param>
        public void SetLanguage(string language)
        {
            int id = _localesIdDictionary[language];
            StartCoroutine(SetLocal(id));
        }

        /// <summary>
        /// 获取当前语言id
        /// </summary>
        /// <returns>当前所选语言id</returns>
        public LocalsDataSo GetLocalID()
        {
            return localsID;
        }

        /// <summary>
        /// 设置卡片文本
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 本地化设置协程
        /// </summary>
        /// <param name="languageID">语言id</param>
        /// <returns></returns>

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