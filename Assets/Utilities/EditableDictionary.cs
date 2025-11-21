using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode.Utilities
{
    /// <summary>
    /// 可编辑物品字典
    /// 无法直接序列化
    /// ToDictionary只能运行时使用
    /// </summary>
    /// <typeparam name="TKey">键值</typeparam>
    /// <typeparam name="TObject">值</typeparam>
    [Serializable]
    public class EditableDictionary<TKey, TObject>
    {
        /// <summary>
        /// 这个暴露出来是为了编辑器状态时用代码配置
        /// 运行时如果要用ToDictionary
        /// </summary>
        public List<EditableDictionaryItem<TKey, TObject>> dictionaryList;

        private Dictionary<TKey, TObject> _dictionary;

        public Dictionary<TKey, TObject> ToDictionary
        {
            get
            {
                if (_dictionary == null)
                {
                    _dictionary = new Dictionary<TKey, TObject>(dictionaryList.Count);

                    for (int i = 0; i < dictionaryList.Count; i++)
                    {
                        if (!_dictionary.TryAdd(dictionaryList[i].keyValue, dictionaryList[i].objectValue))
                        {
                            Debug.LogWarning($"存在相同的键值{dictionaryList[i].keyValue}，保留第一次添加的值");
                        }
                    }
                }

                return _dictionary;
            }
        }

        /// <summary>
        /// 可编辑物品，用于在可编辑字典中储存
        /// </summary>
        /// <typeparam name="TK">键值</typeparam>
        /// <typeparam name="TO">值</typeparam>
        [Serializable]
        public struct EditableDictionaryItem<TK, TO> : IEquatable<EditableDictionaryItem<TK, TO>>
        {
            public TKey keyValue;
            public TO objectValue;

            public EditableDictionaryItem(TKey keyValue, TO objectValue)
            {
                this.keyValue = keyValue;
                this.objectValue = objectValue;
            }

            #region 优化相等判断

            public bool Equals(EditableDictionaryItem<TK, TO> other)
            {
                return EqualityComparer<TKey>.Default.Equals(keyValue, other.keyValue) &&
                       EqualityComparer<TO>.Default.Equals(objectValue, other.objectValue);
            }

            public override bool Equals(object obj)
            {
                return obj is EditableDictionaryItem<TK, TO> other && Equals(other);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(keyValue, objectValue);
            }

            #endregion
        }

        public EditableDictionary()
        {
            dictionaryList = new List<EditableDictionaryItem<TKey, TObject>>();
        }
    }
}