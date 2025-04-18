using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Utilities;
using Object = UnityEngine.Object;

namespace ZiercCode.ObjectPool
{
    public class PoolManager : USingleton<PoolManager>
    {
        private readonly Dictionary<string, ObjectPool> _pools =
            new Dictionary<string, ObjectPool>();

        // private readonly Dictionary<string, Transform> _poolParents = new Dictionary<string, Transform>(); //用于存储对象池容器

        // private void UpdatePoolParents() //更新对象池容器 添加新的对象池容器 每次注册之后调用
        // {
        //     foreach (var pool in _pools)
        //     {
        //         if (!_poolParents.ContainsKey(pool.Key))
        //         {
        //             GameObject newPoolParent = new GameObject(pool.Key);
        //             newPoolParent.transform.SetParent(gameObject.transform);
        //             _poolParents.Add(pool.Key, newPoolParent.transform);
        //         }
        //     }
        // }
        public void Register(string objName, Object rootObject, int min, int max, bool check = false)
        {
            if (_pools.ContainsKey(objName))
            {
                //Debug.LogWarning($"{objName}已经注册物品池");
                return;
            }

            ObjectPool newO = new ObjectPool(objName, rootObject, check, min, max);
            newO.Release(newO.Get()); //初始化时实例化一个 防止卡顿
            _pools.Add(objName, newO);


            // UpdatePoolParents();
        }

        public void Register(string objName, Object rootObject)
        {
            if (_pools.ContainsKey(objName))
            {
                //Debug.LogWarning($"{objName}已经注册物品池");
                return;
            }

            ObjectPool newO = new ObjectPool(objName, rootObject);
            newO.Release(newO.Get()); //初始化时实例化一个 防止卡顿
            _pools.Add(objName, newO);
            //UpdatePoolParents();
        }

        public Object Get(string objName)
        {
            if (_pools.TryGetValue(objName, out ObjectPool pool))
            {
                // if (obj.GameObject())
                // {
                //     obj.GameObject().transform.SetParent(_poolParents[objName]);
                // }

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
                _pools.Remove(poolName);
        }

        public void DisposeAll(bool releasePool = true) //销毁所有对象池中的全部对象
        {
            foreach (var pool in _pools)
            {
                pool.Value.Dispose();
            }

            if (releasePool)
                _pools.Clear();
        }
    }
}