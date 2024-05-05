using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace ZiercCode.Core.Pool
{
    /// <summary>
    /// 对象池物品生成器
    /// </summary>
    public class PoolObjectSpawner : MonoBehaviour
    {
        [Expandable] public List<PoolObjectSo> poolObjects;

        private Dictionary<GameObject, SpawnHandle> _handlerDictionary;

        private void Awake()
        {
            CreatePools();
            _handlerDictionary = new Dictionary<GameObject, SpawnHandle>();
        }

        /// <summary>
        /// 生成对象
        /// </summary>
        /// <param name="poolObjectSo"></param>
        /// <returns></returns>
        public SpawnHandle SpawnPoolObject(PoolObjectSo poolObjectSo)
        {
            GameObject result = PoolManager.Instance.GetPoolObject(poolObjectSo);

            return GetHandler(poolObjectSo, result);
        }


        /// <summary>
        /// 生成对象
        /// </summary>
        /// <param name="poolObjectSo"></param>
        /// <param name="parent">父物体</param>
        /// <returns></returns>
        public SpawnHandle SpawnPoolObject(PoolObjectSo poolObjectSo, Transform parent)
        {
            GameObject result = PoolManager.Instance.GetPoolObject(poolObjectSo, parent);

            return GetHandler(poolObjectSo, result);
        }

        /// <summary>
        /// 生成对象
        /// </summary>
        /// <param name="poolObjectSo"></param>
        /// <param name="parent">父物体</param>
        /// <param name="quaternion">角度</param>
        /// <returns></returns>
        public SpawnHandle SpawnPoolObject(PoolObjectSo poolObjectSo, Transform parent, Quaternion quaternion)
        {
            GameObject result = PoolManager.Instance.GetPoolObject(poolObjectSo, parent, quaternion);

            return GetHandler(poolObjectSo, result);
        }

        /// <summary>
        /// 获取生成处理类
        /// </summary>
        /// <param name="poolObjectSo"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private SpawnHandle GetHandler(PoolObjectSo poolObjectSo, GameObject obj)
        {
            SpawnHandle handle = _handlerDictionary.GetValueOrDefault(obj);
            if (handle == null)
            {
                handle = new SpawnHandle(obj, PoolManager.Instance.GetPool(poolObjectSo), poolObjectSo);
                _handlerDictionary.Add(obj, handle);
            }

            return handle;
        }

        /// <summary>
        /// 创建对象池
        /// </summary>
        private void CreatePools()
        {
            if (poolObjects == null || poolObjects.Count <= 0) return;
            foreach (var poolObject in poolObjects)
            {
                PoolManager.Instance.CreatePool(poolObject);
            }
        }
    }
}