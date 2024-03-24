using System;

namespace Runtime.Helper
{
    [Serializable]
    public class EditableDictionaryItem<TKey, TObject>
    {
        public TKey keyValue;
        public TObject objectValue;

        public EditableDictionaryItem(TKey keyValue, TObject objectValue)
        {
            this.keyValue = keyValue;
            this.objectValue = objectValue;
        }
    }
}