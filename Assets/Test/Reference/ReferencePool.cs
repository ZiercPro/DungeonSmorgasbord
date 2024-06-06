using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode
{
    public class ReferencePool
    {
        /// 引用缓存
        /// <summary>
        /// </summary>
        private Stack<IReference> _referenceCache;

        /// <summary>
        /// 引用类型
        /// </summary>
        public Type ReferenceType { get; private set; }

        public ReferencePool(Type referenceType, int capacity)
        {
            Type interfaceType = referenceType.GetInterface(nameof(IReference));

            if (interfaceType == null)
            {
                throw new Exception($"引用类型{referenceType}没有实现接口{typeof(IReference).FullName}");
            }

            ReferenceType = referenceType;

            _referenceCache = new Stack<IReference>(capacity);
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        public IReference Get()
        {
            if (_referenceCache.Count > 0)
            {
                return _referenceCache.Pop();
            }

            IReference newReference = (IReference)Activator.CreateInstance(ReferenceType);

            newReference.OnSpawn();

            return newReference;
        }

        /// <summary>
        /// 释放实例
        /// </summary>
        /// <param name="instance">要释放的实例</param>
        public void Release(IReference instance)
        {
            Type type = instance.GetType();

            if (type != ReferenceType)
            {
                Debug.LogError($"{type.FullName}类型不属于{ReferenceType.FullName}引用池");
                return;
            }

            _referenceCache.Push(instance);
        }

        /// <summary>
        /// 清空引用池
        /// </summary>
        public void Clear()
        {
            _referenceCache.Clear();
        }
    }
}