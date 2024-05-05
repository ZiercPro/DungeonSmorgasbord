using UnityEngine;
using UnityEngine.Pool;

namespace ZiercCode.Core.Pool
{
    public class SpawnHandle
    {
        /// <summary>
        /// 储存的游戏实例
        /// </summary>
        private GameObject _poolObject;

        /// <summary>
        /// 对象池对象数据
        /// </summary>
        private PoolObjectSo _poolObjectSo;

        /// <summary>
        /// 该实例所在的对象池
        /// </summary>
        private ObjectPool<GameObject> _pool;

        public SpawnHandle(GameObject poolObject, ObjectPool<GameObject> pool, PoolObjectSo objectSo)
        {
            _poolObjectSo = objectSo;
            _poolObject = poolObject;
            _pool = pool;
        }

        public GameObject GetObject()
        {
            if (_poolObjectSo.haveIPoolObject)
                if (_poolObject.TryGetComponent(out IPoolObject iPoolObject))
                {
                    iPoolObject.OnGet();
                    iPoolObject.SetSpawnHandle(this);
                }

            return _poolObject;
        }

        public void Release()
        {
            if (_poolObjectSo.haveIPoolObject)
                if (_poolObject.TryGetComponent(out IPoolObject iPoolObject))
                    iPoolObject.OnRelease();

            _pool.Release(_poolObject);
        }
    }
}