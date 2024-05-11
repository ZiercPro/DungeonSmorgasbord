using NaughtyAttributes.Scripts.Core.DrawerAttributes;
using UnityEngine;
using System.Collections.Generic;

namespace ZiercCode.Core.Pool
{
    /// <summary>
    /// 对象池物品生成器
    /// </summary>
    public class PoolObjectSpawner : MonoBehaviour
    {
        [Expandable, SerializeField] private List<PoolObjectSo> poolObjects;

        private Dictionary<GameObject, SpawnHandle> _handlerDictionary;


        private void Awake()
        {
            CreatePools();
            _handlerDictionary = new Dictionary<GameObject, SpawnHandle>();
        }


        /// <summary>
        /// 生成对象
        /// </summary>
        /// <param name="poolObjectSo">池对象数据</param>
        /// <returns></returns>
        public SpawnHandle SpawnPoolObject(PoolObjectSo poolObjectSo)
        {
            GameObject result = PoolManager.Instance.GetPoolObject(poolObjectSo);
            return GetHandler(poolObjectSo, result);
        }

        /// <summary>
        /// 生成对象
        /// </summary>
        /// <param name="poolObjectSo">池对象数据</param>
        /// <param name="liveTime">存活时间</param>
        /// <returns></returns>
        public SpawnHandle SpawnPoolObjectWithAutoRelease(PoolObjectSo poolObjectSo, float liveTime)
        {
            SpawnHandle handle = SpawnPoolObject(poolObjectSo);
            PoolManager.Instance.AddToAutoRelease(handle, liveTime);
            return handle;
        }


        /// <summary>
        /// 生成对象
        /// </summary>
        /// <param name="poolObjectSo">池对象数据</param>
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
        /// <param name="poolObjectSo">池对象数据</param>
        /// <param name="position">位置</param>
        /// <returns></returns>
        public SpawnHandle SpawnPoolObject(PoolObjectSo poolObjectSo, Vector3 position)
        {
            GameObject result = PoolManager.Instance.GetPoolObject(poolObjectSo, position);

            return GetHandler(poolObjectSo, result);
        }


        /// <summary>
        /// 生成对象，且自动销毁
        /// </summary>
        /// <param name="poolObjectSo">池对象数据</param>
        /// <param name="parent">父物体</param>
        /// <param name="liveTime">存活时间</param>
        public SpawnHandle SpawnPoolObjectWithAutoRelease(PoolObjectSo poolObjectSo, Transform parent, float liveTime)
        {
            SpawnHandle handle = SpawnPoolObject(poolObjectSo, parent);
            PoolManager.Instance.AddToAutoRelease(handle, liveTime);
            return handle;
        }

        /// <summary>
        /// 生成对象，且自动销毁
        /// </summary>
        /// <param name="poolObjectSo">池对象数据</param>
        /// <param name="position">位置</param>
        /// <param name="liveTime">存活时间</param>
        public SpawnHandle SpawnPoolObjectWithAutoRelease(PoolObjectSo poolObjectSo, Vector3 position, float liveTime)
        {
            SpawnHandle handle = SpawnPoolObject(poolObjectSo, position);
            PoolManager.Instance.AddToAutoRelease(handle, liveTime);
            return handle;
        }

        /// <summary>
        /// 生成对象
        /// </summary>
        /// <param name="poolObjectSo">池对象数据</param>
        /// <param name="parent">父物体</param>
        /// <param name="quaternion">角度</param>
        /// <returns></returns>
        public SpawnHandle SpawnPoolObject(PoolObjectSo poolObjectSo, Transform parent, Quaternion quaternion)
        {
            GameObject result = PoolManager.Instance.GetPoolObject(poolObjectSo, parent, quaternion);

            return GetHandler(poolObjectSo, result);
        }


        /// <summary>
        /// 生成对象
        /// </summary>
        /// <param name="poolObjectSo">池对象数据</param>
        /// <param name="position">位置</param>
        /// <param name="quaternion">角度</param>
        /// <returns></returns>
        public SpawnHandle SpawnPoolObject(PoolObjectSo poolObjectSo, Vector3 position, Quaternion quaternion)
        {
            GameObject result = PoolManager.Instance.GetPoolObject(poolObjectSo, position, quaternion);

            return GetHandler(poolObjectSo, result);
        }

        /// <summary>
        /// 生成对象，且自动销毁
        /// </summary>
        /// <param name="poolObjectSo">池对象数据</param>
        /// <param name="parent">父物体</param>
        /// <param name="quaternion">初始旋转角</param>
        /// <param name="liveTime">存活时间</param>
        public SpawnHandle SpawnPoolObjectWithAutoRelease(PoolObjectSo poolObjectSo, Transform parent,
            Quaternion quaternion,
            float liveTime)
        {
            SpawnHandle handle = SpawnPoolObject(poolObjectSo, parent, quaternion);
            PoolManager.Instance.AddToAutoRelease(handle, liveTime);
            return handle;
        }

        /// <summary>
        /// 生成对象，且自动销毁
        /// </summary>
        /// <param name="poolObjectSo">池对象数据</param>
        /// <param name="position">位置</param>
        /// <param name="quaternion">旋转角</param>
        /// <param name="liveTime">存活时间</param>
        public SpawnHandle SpawnPoolObjectWithAutoRelease(PoolObjectSo poolObjectSo, Vector3 position,
            Quaternion quaternion,
            float liveTime)
        {
            SpawnHandle handle = SpawnPoolObject(poolObjectSo, position, quaternion);
            PoolManager.Instance.AddToAutoRelease(handle, liveTime);
            return handle;
        }

        /// <summary>
        /// 删除池
        /// </summary>
        /// <param name="poolObjectSo">池对象数据</param>
        public void DestroyPool(PoolObjectSo poolObjectSo)
        {
            PoolManager.Instance.ReleasePool(poolObjectSo);
        }

        /// <summary>
        /// 获取生成处理类
        /// </summary>
        /// <param name="poolObjectSo">池对象数据</param>
        /// <param name="obj">对象实例</param>
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