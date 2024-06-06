using System;
using System.Collections.Generic;

namespace ZiercCode
{
    public static class ZiercReference
    {
        private static readonly Dictionary<Type, ReferencePool> _referencePools = new Dictionary<Type, ReferencePool>();

        /// <summary>
        /// 引用池初始大小
        /// </summary>
        public static int InitCapacity = 100;

        /// <summary>
        /// 获取引用实例
        /// </summary>
        /// <typeparam name="T">引用类型</typeparam>
        /// <returns></returns>
        public static T GetReference<T>()
        {
            Type type = typeof(T);
            if (_referencePools.TryGetValue(type, out ReferencePool pool))
            {
                return (T)pool.Get();
            }

            ReferencePool newPool = new ReferencePool(type, InitCapacity);

            _referencePools.Add(type, newPool);

            return (T)newPool.Get();
        }

        /// <summary>
        /// 释放引用实例
        /// </summary>
        /// <param name="instance">引用实例</param>
        public static void ReleaseReference(IReference instance)
        {
            Type type = instance.GetType();

            if (_referencePools.TryGetValue(type, out ReferencePool pool))
            {
                pool.Release(instance);
                return;
            }

            ReferencePool newPool = new ReferencePool(type, InitCapacity);

            _referencePools.Add(type, newPool);

            newPool.Release(instance);
        }

        /// <summary>
        /// 清除所有引用缓存
        /// </summary>
        public static void Clear()
        {
            foreach (var referencePool in _referencePools)
            {
                referencePool.Value.Clear();
            }

            _referencePools.Clear();
        }
    }
}