using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using ZiercCode.DungeonSmorgasbord.Locale;
using ZiercCode.Old.Helper;
using ZiercCode.Utilities;

namespace ZiercCode.Locale
{
    public class LocalizationComponent : USingleton<LocalizationComponent>
    {
        // /// <summary>
        // /// 自定义文本本地化数据
        // /// </summary>
        // [SerializeField, Expandable] private List<CustomTextDataSo> customTextDataSoList;

        [SerializeField] private AssetLabelReference customTextTableLabel;
        private Dictionary<string, CustomTextTable> _customTextTable;

        /// <summary>
        /// 当前所选语言
        /// </summary>
        private LanguageEnum _currentLanguage;

        public void InitializeCustomText()
        {
            _customTextTable = new();

            //加载配表文件 解析配表文件
            AsyncOperationHandle<IList<TextAsset>> load =
                Addressables.LoadAssetsAsync<TextAsset>(customTextTableLabel, ParseCustomTextTable);

            load.WaitForCompletion();
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
            if (_customTextTable.TryGetValue(itemId, out CustomTextTable customT))
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

        //解析自定义文本配表(csv
        private void ParseCustomTextTable(TextAsset customTextTableFile)
        {
            if (!customTextTableFile) return;

            string[] lines = customTextTableFile.text.Split('\n'); //将行通过换行分离

            for (int i = 1; i < lines.Length; i++)
            {
                string[] rows = lines[i].Split(','); //通过,将列分离

                if (rows.Length > 2)
                {
                    CustomTextTable temp = new CustomTextTable { chinese = rows[1], english = rows[2] };
                    _customTextTable.Add(rows[0], temp);
                }
            }
        }
    }
}