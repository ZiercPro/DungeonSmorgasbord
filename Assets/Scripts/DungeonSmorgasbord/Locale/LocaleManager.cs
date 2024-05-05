using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using ZiercCode.Core.Extend;
using ZiercCode.Core.Utilities;
using ZiercCode.Old.Helper;
using ZiercCode.Old.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Locale
{
    public class LocaleManager : USingletonComponentDontDestroy<LocaleManager>
    {
        /// <summary>
        /// 自定义文本本地化数据
        /// </summary>
        [SerializeField] private List<CustomTextDataSo> customTextDataSoList;

        /// <summary>
        /// 当前所选语言
        /// </summary>
        private LanguageEnum _currentLanguage;

        /// <summary>
        /// 设置语言的唯一途径
        /// </summary>
        /// <param name="language">语言</param>
        public void SetLanguage(LanguageEnum language)
        {
            _currentLanguage = language;
            StartCoroutine(SetLocal(language));
        }

        /// <summary>
        /// 获取本地化文本数据
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>文本</returns>
        public string GetLocaleText(int itemId)
        {
            int languageIDd = GetSelectedLanguageID();
            //寻找物品数据
            foreach (var customTextDataSo in customTextDataSoList)
            {
                EditableDictionary<int, CustomTextTable> customTextDictionary =
                    customTextDataSo.CustomTextDictionary;
                if (customTextDictionary.Contain(itemId))
                {
                    switch (_currentLanguage)
                    {
                        case LanguageEnum.Chinese:
                            return customTextDictionary[itemId].chinese;
                        case LanguageEnum.English:
                            return customTextDictionary[itemId].english;
                        default:
                            Debug.LogError("当前语言不存在");
                            break;
                    }
                }
            }

            Debug.LogError("无法获取文本!");
            return null;
        }

        /// <summary>
        /// 本地化设置协程
        /// </summary>
        /// <param name="languageID">语言id</param>
        /// <returns></returns>
        private IEnumerator SetLocal(LanguageEnum languageID)
        {
            if (LocalizationSettings.Instance.GetSelectedLocale() ==
                LocalizationSettings.AvailableLocales.Locales[(int)languageID])
            {
                // Debug.Log("已经选择该语言");
                yield break;
            }

            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)languageID];
            // Debug.Log("语言切换完毕");
        }

        /// <summary>
        /// 获取当前所选语言id
        /// </summary>
        /// <returns>id</returns>
        public int GetSelectedLanguageID()
        {
            return (int)_currentLanguage;
        }

        /// <summary>
        /// 获取所选的语言
        /// </summary>
        /// <returns>语言枚举</returns>
        public LanguageEnum GetSelectedLanguage()
        {
            return _currentLanguage;
        }
    }
}