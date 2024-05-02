using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using ZiercCode.Core.System;

namespace ZiercCode.Core.Pool
{
    /// <summary>
    /// 对象池管理器
    /// 管理所有物品的对象池
    /// </summary>
    public class PoolManager : USingletonComponentDontDestroy<PoolManager>
    {
        /// <summary>
        /// 默认初始值
        /// </summary>
        private const int DEFAULT_INIT_SIZE = 1;

        /// <summary>
        /// 默认最大值
        /// </summary>
        private const int DEFAULT_MAX_SIZE = 500;


        /// <summary>
        /// 储存所有的对象池
        /// </summary>
        private Dictionary<string, ObjectPool<GameObject>> _pools;

        /// <summary>
        /// 储存所有对象池的父类实例
        /// </summary>
        private Dictionary<string, GameObject> _poolObjectParents;

        /// <summary>
        /// 所有对象池实例的父类
        /// </summary>
        private GameObject _poolsContainer;

        protected override void Awake()
        {
            base.Awake();
            _pools = new Dictionary<string, ObjectPool<GameObject>>();
            _poolObjectParents = new Dictionary<string, GameObject>();
            _poolsContainer = new GameObject("Pools");
        }

        /// <summary>
        /// 创建对象池
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <param name="createFunc">对象创建方法</param>
        /// <param name="onGet">对象获取方法</param>
        /// <param name="onRelease">对象释放方法</param>
        /// <param name="onDestroy">对象销毁方法</param>
        /// <param name="collectionCheck"></param>
        /// <param name="initSize">对象池初始大小</param>
        /// <param name="maxSize">对象池最大大小</param>
        public ObjectPool<GameObject> CreatePool(string poolName, Func<GameObject> createFunc, Action<GameObject> onGet,
            Action<GameObject> onRelease, Action<GameObject> onDestroy, bool collectionCheck = false,
            int initSize = DEFAULT_INIT_SIZE,
            int maxSize = DEFAULT_MAX_SIZE)
        {
            if (_pools.TryGetValue(poolName, out ObjectPool<GameObject> result))
                return result;

            //创建新的对象池
            ObjectPool<GameObject> newPool = new(createFunc, onGet, onRelease, onDestroy,
                collectionCheck, initSize, maxSize);
            //添加到对象池字典
            _pools.Add(poolName, newPool);
            //创建新的对象池实例
            GameObject newContainer = new GameObject(poolName);
            newContainer.transform.SetParent(_poolsContainer.transform);
            //添加到对象池实例字典
            _poolObjectParents.Add(poolName, newContainer);
            return newPool;
        }
        
        /// <summary>
        /// 获取指定对象池
        /// </summary>
        /// <param name="poolName"></param>
        /// <returns></returns>
        public ObjectPool<GameObject> GetPool(string poolName)
        {
            return _pools.GetValueOrDefault(poolName);
        }

        /// <summary>
        /// 获取对象池实例
        /// </summary>
        /// <returns></returns>
        public GameObject GetPoolContainer(string poolName)
        {
            return _poolObjectParents.GetValueOrDefault(poolName);
        }

        /// <summary>
        /// 获取指定对象
        /// </summary>
        /// <param name="poolName"></param>
        /// <returns></returns>
        public GameObject GetPoolObject(string poolName)
        {
            ObjectPool<GameObject> objectPool = GetPool(poolName);
            if (objectPool != null)
                return objectPool.Get();

            return null;
        }

        /// <summary>
        /// 获取指定对象
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <param name="parentTransform">对象父物体</param>
        /// <returns></returns>
        public GameObject GetPoolObject(string poolName, Transform parentTransform)
        {
            GameObject result = GetPoolObject(poolName);
            if (result)
            {
                result.transform.SetParent(parentTransform);
                result.transform.localPosition = Vector3.zero;
            }

            return result;
        }

        /// <summary>
        /// 获取指定对象
        /// </summary>
        /// <param name="poolName">对象名称</param>
        /// <param name="parentTransform">对象父物体</param>
        /// <param name="quaternion">对象初始角度</param>
        /// <returns></returns>
        public GameObject GetPoolObject(string poolName, Transform parentTransform, Quaternion quaternion)
        {
            GameObject result = GetPoolObject(poolName, parentTransform);
            if (result)
                result.transform.localRotation = quaternion;

            return result;
        }

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <param name="objectToRelease">要释放的对象实例</param>
        public void ReleasePoolObject(string poolName, GameObject objectToRelease)
        {
            ObjectPool<GameObject> objectPool = GetPool(poolName);
            if (objectPool != null)
                objectPool.Release(objectToRelease);
        }

        /// <summary>
        /// 默认释放方法
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <param name="g">释放的对象</param>
        public void DefaultReleaseFunc(string poolName, GameObject g)
        {
            g.SetActive(false);
            g.transform.SetParent(GetPoolContainer(poolName).transform);
        }
    }
}