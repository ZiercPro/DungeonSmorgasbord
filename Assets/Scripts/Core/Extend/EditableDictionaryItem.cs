using NaughtyAttributes;
using System;
using UnityEngine;

namespace ZiercCode.Core.Extend
{
    /// <summary>
    /// 可编辑物品，用于在可编辑字典中储存
    /// </summary>
    /// <typeparam name="TKey">键值</typeparam>
    /// <typeparam name="TObject">值</typeparam>
    [Serializable]
    public class EditableDictionaryItem<TKey, TObject>
    {
#if UNITY_EDITOR
        //只用来在检视界面显示
        [ReadOnly, SerializeField, AllowNesting]
        private string _inspectorName;
#endif
        public TKey keyValue;
        public TObject objectValue;

        public EditableDictionaryItem(TKey keyValue, TObject objectValue,string itemName=null)
        {
#if UNITY_EDITOR
            _inspectorName = itemName;
#endif
            this.keyValue = keyValue;
            this.objectValue = objectValue;
        }
    }
}