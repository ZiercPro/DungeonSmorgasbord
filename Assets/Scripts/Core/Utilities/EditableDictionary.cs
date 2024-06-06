using System;
using UnityEngine;
using NaughtyAttributes.Scripts.Core.DrawerAttributes;
using NaughtyAttributes.Scripts.Core.MetaAttributes;
using System.Collections.Generic;

namespace ZiercCode.Core.Utilities
{
    /// <summary>
    /// 可编辑物品字典
    /// 不动态修改
    /// </summary>
    /// <typeparam name="TKey">键值</typeparam>
    /// <typeparam name="TObject">值</typeparam>
    [Serializable]
    public class EditableDictionary<TKey, TObject>
    {
        [SerializeField] private List<EditableDictionaryItem<TKey, TObject>> dictionaryList;

        private Dictionary<TKey, TObject> _dictionary;

        /// <summary>
        /// 可编辑物品，用于在可编辑字典中储存
        /// </summary>
        /// <typeparam name="TK">键值</typeparam>
        /// <typeparam name="TO">值</typeparam>
        [Serializable]
        private class EditableDictionaryItem<TK, TO>
        {
#if UNITY_EDITOR
            //只用来在检视界面显示
            [ReadOnly, SerializeField, AllowNesting, ShowIf("HaveInspectorName")]
            private string _inspectorName;

            //是否有检视界面名称
            private bool HaveInspectorName => _inspectorName != null;

#endif
            public TKey keyValue;
            public TO objectValue;

            public EditableDictionaryItem(TKey keyValue, TO objectValue, string itemName = null)
            {
#if UNITY_EDITOR
                _inspectorName = itemName;
#endif
                this.keyValue = keyValue;
                this.objectValue = objectValue;
            }
        }

        public int Count => dictionaryList?.Count ?? 0;

        public EditableDictionary()
        {
            dictionaryList = new();
            _dictionary = new();
        }

        /// <summary>
        /// 生成字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<TKey, TObject> ToDictionary()
        {
            if (_dictionary.Count == 0)
            {
                foreach (var item in dictionaryList)
                {
                    _dictionary.Add(item.keyValue, item.objectValue);
                }
            }

            return _dictionary;
        }

        /// <summary>
        /// 添加物品
        /// </summary>
        /// <param name="keyValue">键值</param>
        /// <param name="objectValue">值</param>
        /// <param name="itemName">该物品在检视器中的名称</param>
        public void Add(TKey keyValue, TObject objectValue, string itemName = null)
        {
            if (dictionaryList == null)
            {
                Debug.LogWarning("链表为空");
                return;
            }

            dictionaryList.Add(new EditableDictionaryItem<TKey, TObject>(keyValue, objectValue, itemName));
        }

        /// <summary>
        /// 移除物品
        /// </summary>
        /// <param name="keyValue">键值</param>
        /// <returns>是否移除成功</returns>
        public bool Remove(TKey keyValue)
        {
            if (dictionaryList == null || dictionaryList.Count == 0)
            {
                Debug.LogWarning("链表为空");
                return false;
            }

            foreach (var item in dictionaryList)
            {
                if (item.keyValue.Equals(keyValue))
                {
                    dictionaryList.Remove(item);
                    return true;
                }
            }

            Debug.LogWarning($"{keyValue}不存在");
            return false;
        }

        /// <summary>
        /// 根据序号移除
        /// </summary>
        /// <param name="index">物品在字典中序号</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveAt(int index)
        {
            if (dictionaryList.Count <= index)
            {
                Debug.LogWarning($"{index}超出链表范围");
                return false;
            }

            dictionaryList.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// 清除字典所有物品
        /// </summary>
        public void Clear()
        {
            if (dictionaryList == null)
                Debug.LogError("链表不存在!");
            if (dictionaryList.Count == 0)
                dictionaryList.Clear();
        }

        /// <summary>
        /// 判断是否字典中是否存在物品
        /// </summary>
        /// <param name="keyValue">键值</param>
        /// <returns></returns>
        public bool Contain(TKey keyValue)
        {
            if (dictionaryList == null || dictionaryList.Count == 0)
            {
                Debug.LogWarning("链表为空");
                return false;
            }

            foreach (var item in dictionaryList)
            {
                if (item.keyValue.Equals(keyValue))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 获取物品
        /// </summary>
        /// <param name="keyValue">物品键值</param>
        /// <returns>若存在则返回结果，若不存在则返回默认值</returns>
        public TObject Get(TKey keyValue)
        {
            if (dictionaryList == null || dictionaryList.Count == 0)
            {
                Debug.LogWarning("链表为空");
                return default;
            }

            foreach (var item in dictionaryList)
            {
                if (item.keyValue.Equals(keyValue))
                    return item.objectValue;
            }

            return default;
        }

        /// <summary>
        /// 获取物品
        /// </summary>
        /// <param name="index">链表序号</param>
        /// <returns></returns>
        public TObject Get(int index)
        {
            if (dictionaryList == null || dictionaryList.Count == 0)
            {
                Debug.LogWarning("链表为空");
                return default;
            }

            if (Count <= index)
            {
                Debug.LogError($"{index} 超出链表范围");
                return default;
            }

            return dictionaryList[index].objectValue;
        }

        /// <summary>
        /// 设置物品
        /// </summary>
        /// <param name="keyValue">物品键值</param>
        /// <param name="objectValue">值</param>
        /// <returns></returns>
        public bool Set(TKey keyValue, TObject objectValue)
        {
            if (dictionaryList == null || dictionaryList.Count == 0)
            {
                Debug.LogWarning("链表为空");
                return false;
            }

            foreach (var item in dictionaryList)
            {
                if (item.keyValue.Equals(keyValue))
                {
                    item.objectValue = objectValue;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 设置物品
        /// </summary>
        /// <param name="index">链表序号</param>
        /// <param name="objectValue">值</param>
        /// <returns></returns>
        public bool Set(int index, TObject objectValue)
        {
            if (dictionaryList == null || dictionaryList.Count == 0)
            {
                Debug.LogWarning("链表为空");
                return false;
            }

            if (Count <= index)
            {
                Debug.LogError($"{index} 超出链表范围");
                return false;
            }

            dictionaryList[index].objectValue = objectValue;

            return true;
        }

        /// <summary>
        /// 重载'[]'符号
        /// </summary>
        /// <param name="keyValue">字典名</param>
        public TObject this[TKey keyValue]
        {
            get
            {
                if (Contain(keyValue))
                {
                    return Get(keyValue);
                }

                return default;
            }
            set => Set(keyValue, value);
        }

        /// <summary>
        /// 重载'[]'符号
        /// </summary>
        /// <param name="index">链表序号</param>
        public TObject this[int index]
        {
            get => Get(index);

            set => Set(index, value);
        }
    }
}