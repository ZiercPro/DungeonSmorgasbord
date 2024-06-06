using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using ZiercCode.DungeonSmorgasbord.Locale;
using ZiercCode.Old.Helper;
using ZiercCode.Old.ScriptObject;
using ZiercCode.Test.Base;

namespace ZiercCode.Test.Locale
{
    public class LocalizationComponent : ZiercComponent
    {
        /// <summary>
        /// 自定义文本本地化数据
        /// </summary>
        [SerializeField] private List<CustomTextDataSo> customTextDataSoList;

        private Dictionary<string, CustomTextTable> _customTextData;

        /// <summary>
        /// 当前所选语言
        /// </summary>
        private LanguageEnum _currentLanguage;

        public void InitializeCustomText()
        {
            _customTextData = new Dictionary<string, CustomTextTable>();

            //将每一个自定义文本中储存的数据 存到字典中
            foreach (var customTextDataSo in customTextDataSoList)
            {
                foreach (var kv in customTextDataSo.CustomTextDictionary.ToDictionary())
                {
                    _customTextData.Add(kv.Key, kv.Value);
                }
            }
        }

        /// <summary>
        /// 设置游戏语言
        /// </summary>
        /// <param name="language"></param>
        /// <returns>设置语言协程</returns>
        public Coroutine SetLanguage(LanguageEnum language)
        {
            _currentLanguage = language;
            return StartCoroutine(SetLocal(language));
        }

        /// <summary>
        /// 获取本地化文本数据
        /// </summary>
        /// <param name="itemId">物品id</param>
        /// <returns>文本</returns>
        public string GetText(string itemId)
        {
            int languageIDd = GetSelectedLanguageID();

            if (_customTextData.TryGetValue(itemId, out CustomTextTable customT))
            {
                switch (_currentLanguage)
                {
                    case LanguageEnum.Chinese:
                        return customT.chinese;
                    case LanguageEnum.English:
                        return customT.english;
                    default:
                        Debug.LogError("当前语言不支持"); //默认返回中文
                        return customT.chinese;
                }
            }

            Debug.LogError($"物品id {itemId} 自定义本地化文本不存在！");

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
                // 已经选择该语言
                yield break;
            }

            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)languageID];
            // 语言切换完毕
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