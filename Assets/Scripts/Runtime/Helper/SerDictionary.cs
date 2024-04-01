using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ZiercCode.Runtime.Helper
{
    /// <summary>
    /// 可序列化字典
    /// 通过能够序列化的链表帮助字典序列化
    /// </summary>
    [Serializable, Obsolete("use EditableDictionary instead")]
    public class SerDictionary<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, ISerializationCallbackReceiver
    {
        [SerializeField] List<Tkey> _keyList;
        [SerializeField] List<Tvalue> _valueList;

        //反序列化之后，把链表的值重新存到字典中
        public void OnAfterDeserialize()
        {
            Clear();
            for (int i = 0; i < Count; i++)
            {
                Add(_keyList[i], _valueList[i]);
            }
        }

        //序列化之前，将字典的值存入到链表中
        public void OnBeforeSerialize()
        {
            _keyList = new List<Tkey>(Count);
            _valueList = new List<Tvalue>(Count);
            foreach (var key in Keys)
            {
                _keyList.Add(key);
                _valueList.Add(base[key]);
            }
        }
    }
}