using NaughtyAttributes;
using System;
using UnityEngine;

namespace ZRuntime
{
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