using System;
using UnityEngine;
using System.Collections.Generic;

namespace ZiercCode.Runtime.Helper
{
    [Serializable]
    public class EditableDictionary<TKey, TObject>
    {
        [SerializeField] private List<EditableDictionaryItem<TKey, TObject>> dicList;

        public int Count => dicList?.Count ?? 0;

        public EditableDictionary()
        {
            dicList = new();
        }

        public Dictionary<TKey, TObject> ToDictionary()
        {
            Dictionary<TKey, TObject> result = new();
            foreach (var item in dicList)
            {
                result.Add(item.keyValue, item.objectValue);
            }

            return result;
        }

        public void Add(TKey keyValue, TObject objectValue, string itemName = null)
        {
            if (dicList == null)
            {
                Debug.LogWarning("链表为空");
                return;
            }

            dicList.Add(new EditableDictionaryItem<TKey, TObject>(keyValue, objectValue, itemName));
        }


        public bool Remove(TKey keyValue)
        {
            if (dicList == null || dicList.Count == 0)
            {
                Debug.LogWarning("链表为空");
                return false;
            }

            foreach (var item in dicList)
            {
                if (item.keyValue.Equals(keyValue))
                {
                    dicList.Remove(item);
                    return true;
                }
            }

            Debug.LogWarning($"{keyValue}不存在");
            return false;
        }

        public bool RemoveAt(int index)
        {
            if (dicList.Count <= index)
            {
                Debug.LogWarning($"{index}超出链表范围");
                return false;
            }

            dicList.RemoveAt(index);
            return true;
        }

        public bool Clear()
        {
            if (dicList == null)
            {
                Debug.LogError("链表不存在!");
                return false;
            }

            if (dicList.Count == 0) return true;
            dicList.Clear();
            return true;
        }

        public bool Contain(TKey keyValue)
        {
            if (dicList == null || dicList.Count == 0)
            {
                Debug.LogWarning("链表为空");
                return false;
            }

            foreach (var item in dicList)
            {
                if (item.keyValue.Equals(keyValue))
                    return true;
            }

            return false;
        }

        public TObject Get(TKey keyValue)
        {
            if (dicList == null || dicList.Count == 0)
            {
                Debug.LogWarning("链表为空");
                return default;
            }

            foreach (var item in dicList)
            {
                if (item.keyValue.Equals(keyValue))
                    return item.objectValue;
            }

            return default;
        }

        public bool Set(TKey keyValue, TObject objectValue)
        {
            if (dicList == null || dicList.Count == 0)
            {
                Debug.LogWarning("链表为空");
                return default;
            }

            foreach (var item in dicList)
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