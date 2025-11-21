using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Utilities;
using Object = UnityEngine.Object;

namespace ZiercCode.ObjectPool
{
    public class PoolManager : USingleton<PoolManager>
    {
        private readonly Dictionary<string, ObjectPool> _pools = new();

        public void Register(string objName, Object rootObject, int min, int max, bool check = false)
        {
            if (_pools.ContainsKey(objName))
            {
                return;
            }

            ObjectPool newO = new(objName, rootObject, check, min, max);
            newO.Release(newO.Get()); //初始化时实例化一个 防止卡顿第一次实例化时卡顿
            _pools.Add(objName, newO);
        }

        public void Register(string objName, Object rootObject)
        {
            if (_pools.ContainsKey(objName))
            {
                return;
            }

            ObjectPool newO = new(objName, rootObject);
            newO.Release(newO.Get()); //初始化时实例化一个 防止卡顿第一次实例化时卡顿
            _pools.Add(objName, newO);
        }

        public Object Get(string objName)
        {
            if (_pools.TryGetValue(objName, out ObjectPool pool))
            {
                return pool.Get();
            }

            Debug.LogWarning($"{objName}还未注册对象池");
            return null;
        }

        public void Release(string objName, Object obj)
        {
            if (_pools.TryGetValue(objName, out ObjectPool pool))
            {
                pool.Release(obj);
            }
            else
            {
                Debug.LogWarning($"{objName}还未注册对象池");
            }
        }

        public void Dispose(string poolName, bool releasePool = true) //销毁对象池中的全部对象 是否移除对象池
        {
            if (_pools.TryGetValue(poolName, out ObjectPool pool))
            {
                pool.Dispose();
            }

            if (releasePool)
            {
                _pools.Remove(poolName);
            }
        }

        public void DisposeAll(bool releasePool = true) //销毁所有对象池中的全部对象
        {
            foreach (KeyValuePair<string, ObjectPool> pool in _pools)
            {
                pool.Value.Dispose();
            }

            if (releasePool)
            {
                _pools.Clear();
            }
        }
    }
}