using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace ZiercCode.Runtime.Helper
{
    [Serializable]
    public class EditableDictionary<TKey, TObject>
    {
        [FormerlySerializedAs("dicList")] [SerializeField] private List<EditableDictionaryItem<TKey, TObject>> dictionaryList;

        public int Count => dictionaryList?.Count ?? 0;

        public EditableDictionary()
        {
            dictionaryList = new();
        }

        public Dictionary<TKey, TObject> ToDictionary()
        {
            Dictionary<TKey, TObject> result = new();
            foreach (var item in dictionaryList)
            {
                result.Add(item.keyValue, item.objectValue);
            }

            return result;
        }

        public void Add(TKey keyValue, TObject objectValue, string itemName = null)
        {
            if (dictionaryList == null)
            {
                Debug.LogWarning("链表为空");
                return;
            }

            dictionaryList.Add(new EditableDictionaryItem<TKey, TObject>(keyValue, objectValue, itemName));
        }


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

        public bool Clear()
        {
            if (dictionaryList == null)
            {
                Debug.LogError("链表不存在!");
                return false;
            }

            if (dictionaryList.Count == 0) return true;
            dictionaryList.Clear();
            return true;
        }

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