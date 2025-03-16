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

        public void Register(string objName, Object rootObject, int min, int max)
        {
            if (_pools.ContainsKey(objName))
            {
                //Debug.LogWarning($"{objName}已经注册物品池");
                return;
            }

            ObjectPool newO = new ObjectPool(objName, rootObject, false, min, max);
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
            _pools.Add(objName, newO);
            //UpdatePoolParents();
        }

        public Object Get(string objName)
        {
            if (_pools.ContainsKey(objName))
            {
                // if (obj.GameObject())
                // {
                //     obj.GameObject().transform.SetParent(_poolParents[objName]);
                // }

                return _pools[objName].Get();
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
    }
}