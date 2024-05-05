using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using ZiercCode.Core.Utilities;

namespace ZiercCode.Core.Pool
{
    /// <summary>
    /// 对象池管理器
    /// 管理所有物品的对象池
    /// </summary>
    public class PoolManager : USingletonComponentDestroy<PoolManager>
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
        private Dictionary<PoolObjectSo, ObjectPool<GameObject>> _pools;

        /// <summary>
        /// 储存所有对象池的父类实例
        /// </summary>
        private Dictionary<PoolObjectSo, GameObject> _poolObjectParents;

        /// <summary>
        /// 所有对象池实例的父类
        /// </summary>
        private GameObject _poolsContainer;

        protected override void Awake()
        {
            base.Awake();
            _pools = new Dictionary<PoolObjectSo, ObjectPool<GameObject>>();
            _poolObjectParents = new Dictionary<PoolObjectSo, GameObject>();
            _poolsContainer = new GameObject("Pools");
        }

        private void OnDestroy()
        {
            //清空所有池对象
            foreach (var pool in _pools)
            {
                pool.Value.Dispose();
            }

            //清除保存的数据
            _poolObjectParents.Clear();
            _pools.Clear();

            //删除所有实例化的对象
            Destroy(_poolsContainer);
        }

        /// <summary>
        /// 创建对象池
        /// </summary>
        /// <param name="objectSo">对象数据</param>
        /// <param name="collectionCheck"></param>
        public ObjectPool<GameObject> CreatePool(PoolObjectSo objectSo,
            bool collectionCheck = false)
        {
            ObjectPool<GameObject> result = GetPool(objectSo);
            if (result != null)
                return result;

            //创建新的对象池
            ObjectPool<GameObject> newPool = new(() =>
                {
                    GameObject newPoolObject = Instantiate(objectSo.prefab);
                    return newPoolObject;
                }, GetFunc, poolObject => ReleaseFunc(objectSo, poolObject), DestroyFunc,
                collectionCheck, objectSo.initSize, objectSo.maxSize);
            //添加到对象池字典
            _pools.Add(objectSo, newPool);
            //创建新的对象池实例
            GameObject newContainer = new(objectSo.poolName);
            newContainer.transform.SetParent(_poolsContainer.transform);
            //添加到对象池实例字典
            _poolObjectParents.Add(objectSo, newContainer);
            return newPool;
        }

        /// <summary>
        /// 创建对象池
        /// </summary>
        /// <param name="objectSo">对象数据</param>
        /// <param name="createFunc">对象创建方法</param>
        /// <param name="onGet">对象获取方法</param>
        /// <param name="onRelease">对象释放方法</param>
        /// <param name="onDestroy">对象销毁方法</param>
        /// <param name="collectionCheck"></param>
        /// <param name="initSize">对象池初始大小</param>
        /// <param name="maxSize">对象池最大大小</param>
        public ObjectPool<GameObject> CreatePool(PoolObjectSo objectSo, Func<GameObject> createFunc,
            Action<GameObject> onGet,
            Action<GameObject> onRelease, Action<GameObject> onDestroy, bool collectionCheck = false,
            int initSize = DEFAULT_INIT_SIZE,
            int maxSize = DEFAULT_MAX_SIZE)
        {
            ObjectPool<GameObject> result = GetPool(objectSo);
            if (result != null)
                return result;

            //创建新的对象池
            ObjectPool<GameObject> newPool = new(createFunc, onGet, onRelease, onDestroy,
                collectionCheck, initSize, maxSize);
            //添加到对象池字典
            _pools.Add(objectSo, newPool);
            //创建新的对象池实例
            GameObject newContainer = new(objectSo.poolName);
            newContainer.transform.SetParent(_poolsContainer.transform);
            //添加到对象池实例字典
            _poolObjectParents.Add(objectSo, newContainer);
            return newPool;
        }

        /// <summary>
        /// 获取指定对象池
        /// </summary>
        /// <param name="objectSo">对象数据</param>
        /// <returns></returns>
        public ObjectPool<GameObject> GetPool(PoolObjectSo objectSo)
        {
            return _pools.GetValueOrDefault(objectSo);
        }

        /// <summary>
        /// 获取对象池储存器实例
        /// </summary>
        /// <returns></returns>
        public GameObject GetPoolContainer(PoolObjectSo objectSo)
        {
            return _poolObjectParents.GetValueOrDefault(objectSo);
        }

        /// <summary>
        /// 获取指定对象
        /// </summary>
        /// <param name="objectSo">对象数据</param>
        /// <returns></returns>
        public GameObject GetPoolObject(PoolObjectSo objectSo)
        {
            ObjectPool<GameObject> objectPool = GetPool(objectSo);
            if (objectPool != null)
                return objectPool.Get();

            Debug.LogError($"{objectSo}无对象池");
            return null;
        }

        /// <summary>
        /// 获取指定对象
        /// </summary>
        /// <param name="objectSo">对象数据</param>
        /// <param name="position">对象生成位置</param>
        /// <returns></returns>
        public GameObject GetPoolObject(PoolObjectSo objectSo, Vector3 position)
        {
            GameObject poolObject = GetPoolObject(objectSo);

            if (!poolObject) return null;

            poolObject.transform.position = position;
            return poolObject;
        }

        /// <summary>
        /// 获取指定对象
        /// </summary>
        /// <param name="objectSo">对象数据</param>
        /// <param name="parentTransform">对象父物体</param>
        /// <returns></returns>
        public GameObject GetPoolObject(PoolObjectSo objectSo, Transform parentTransform)
        {
            GameObject result = GetPoolObject(objectSo);
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
        /// <param name="objectSo">对象数据</param>
        /// <param name="parentTransform">对象父物体</param>
        /// <param name="quaternion">对象初始角度</param>
        /// <returns></returns>
        public GameObject GetPoolObject(PoolObjectSo objectSo, Transform parentTransform, Quaternion quaternion)
        {
            GameObject result = GetPoolObject(objectSo, parentTransform);
            if (result)
                result.transform.localRotation = quaternion;

            return result;
        }

        /// <summary>
        /// 释放指定对象
        /// </summary>
        /// <param name="objectSo">对象数据</param>
        /// <param name="objectToRelease">要释放的对象实例</param>
        public void ReleasePoolObject(PoolObjectSo objectSo, GameObject objectToRelease)
        {
            ObjectPool<GameObject> objectPool = GetPool(objectSo);
            if (objectPool != null)
                objectPool.Release(objectToRelease);
        }

        /// <summary>
        /// 释放对象池
        /// </summary>
        /// <param name="objectSo">池对象数据</param>
        public void ReleasePool(PoolObjectSo objectSo)
        {
            ObjectPool<GameObject> objectPool = GetPool(objectSo);
            objectPool.Dispose();
        }

        /// <summary>
        /// 默认释放方法
        /// </summary>
        /// <param name="objectSo">对象数据</param>
        /// <param name="objectToRelease">释放的对象</param>
        private void ReleaseFunc(PoolObjectSo objectSo, GameObject objectToRelease)
        {
            objectToRelease.transform.SetParent(GetPoolContainer(objectSo).transform);
            objectToRelease.SetActive(false);
        }


        /// <summary>
        /// 默认获取方法
        /// </summary>
        private void GetFunc(GameObject poolObject)
        {
            poolObject.SetActive(true);
        }

        /// <summary>
        /// 默认销毁方法
        /// </summary>
        private void DestroyFunc(GameObject poolObject)
        {
            Destroy(poolObject);
        }
    }
}