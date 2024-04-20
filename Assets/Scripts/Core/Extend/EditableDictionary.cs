using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode.Core.Extend
{
    /// <summary>
    /// 可编辑物品字典
    /// </summary>
    /// <typeparam name="TKey">键值</typeparam>
    /// <typeparam name="TObject">值</typeparam>
    [Serializable]
    public class EditableDictionary<TKey, TObject>
    {
        [SerializeField] private List<EditableDictionaryItem<TKey, TObject>> dictionaryList;

        public int Count => dictionaryList?.Count ?? 0;

        public EditableDictionary()
        {
            dictionaryList = new();
        }

        /// <summary>
        /// 生成字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<TKey, TObject> ToDictionary()
        {
            Dictionary<TKey, TObject> result = new();
            foreach (var item in dictionaryList)
            {
                result.Add(item.keyValue, item.objectValue);
            }

            return result;
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
        /// <returns></returns>
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
        /// 设置物品
        /// </summary>
        /// <param name="keyValue">物品键值</param>
        /// <param name="objectValue">想要修改的值</param>
        /// <returns></returns>
        public bool Set(TKey keyValue, TObject objectValue)
        {
            if (dictionaryList == null || dictionaryList.Count == 0)
            {
                Debug.LogWarning("链表为空");
                return default;
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
        /// 重载'[]'符号
        /// </summary>
        /// <param name="keyValue"></param>
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
    }
}