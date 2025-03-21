using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZiercCode.Utilities
{
    [Serializable]
    public class ObserverValue<T>//用于对mvc架构中的模型数据的变化进行监听
    {
        [SerializeField] private T value;
        [SerializeField] private UnityEvent<T> onValueChange;

        public T Value { get => value; set => Set(value); }

        public ObserverValue(T v)
        {
            value = v ?? default;
            onValueChange = new UnityEvent<T>();
        }

        public void Set(T newValue)
        {
            if (!newValue.Equals(value))
            {
                value = newValue;
                onValueChange?.Invoke(value);
            }
        }

        public void AddListener(UnityAction<T> action)
        {
            if (action != null) onValueChange.AddListener(action);
        }

        public void RemoveListener(UnityAction<T> action)
        {
            if (action != null) onValueChange.RemoveListener(action);
        }

        public void Dispose()
        {
            onValueChange.RemoveAllListeners();
            onValueChange = null;
            value = default;
        }
    }
}