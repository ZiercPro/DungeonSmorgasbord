using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace ZiercCode.ObjectPool
{
    public class ObjectPool
    {
        private const int DEFAULT_MAX = 300;
        private const int DEFAULT_MIN = 10;

        private readonly string _objName;
        private readonly Object _rootObject;
        private readonly ObjectPool<Object> _objectPool;

        public string ObjectName => _objName;

        public ObjectPool(string objName, Object rootObject, bool collectionCheck = false, int min = DEFAULT_MIN,
            int max = DEFAULT_MAX)
        {
            _rootObject = rootObject;
            _objName = objName;

            _objectPool = new ObjectPool<Object>(CreateFunc, GetFunc, ReleaseFunc, DestroyFunc, collectionCheck,
                min, max);
        }

        public Object Get()
        {
            if (_objectPool == null)
            {
                throw new Exception($"对象池为空!");
            }

            return _objectPool.Get();
        }

        public void Release(Object obj)
        {
            if (_objectPool == null)
            {
                throw new Exception($"{_objName}为空!");
            }

            _objectPool.Release(obj);
        }

        public void Dispose()
        {
            if (_objectPool == null)
            {
                throw new Exception($"{_objName}为空!");
            }

            _objectPool.Dispose();
        }

        private Object CreateFunc()
        {
            Object newO = Object.Instantiate(_rootObject);
            return newO;
        }

        private void GetFunc(Object obj)
        {
            if (obj is GameObject go)
            {
                go.SetActive(true);
            }
        }

        private void ReleaseFunc(Object obj)
        {
            if (obj is GameObject go)
            {
                go.SetActive(false);
            }
        }

        private void DestroyFunc(Object obj)
        {
            Object.Destroy(obj);
        }
    }
}