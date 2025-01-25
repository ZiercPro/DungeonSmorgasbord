using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using ZiercCode.Test.Base;
using Object = UnityEngine.Object;

namespace ZiercCode.Test.Resources
{
    /// <summary>
    /// 资源组件，进行资源和场景的批量管理
    /// 这个组件不需要
    /// </summary>
    public class ResourceComponent : ZiercComponent
    {
        private bool _isInitialized;
        public bool IsInitialized => _isInitialized;

        private List<AsyncOperationHandle<IList<Object>>> _loadedAssetsHandles;

        public AsyncOperationHandle<Object> LoadAsset(string key)
        {
            AsyncOperationHandle<Object> handle = Addressables.LoadAssetAsync<Object>(key);
            return handle;
        }

        public void UnloadAsset(string key)
        {
            Addressables.Release(key);
        }

        public void UnloadAsset(AsyncOperationHandle handle)
        {
            Addressables.Release(handle);
        }

        public AsyncOperationHandle<IList<Object>> LoadAssets(object key, Action<Object> callback)
        {
            AsyncOperationHandle<IList<Object>> handle = Addressables.LoadAssetsAsync<Object>(key, callback);
            _loadedAssetsHandles.Add(handle);
            return handle;
        }

        public AsyncOperationHandle<IList<Object>> LoadAssets(List<string> key, Action<Object> callback,
            Addressables.MergeMode mode)
        {
            AsyncOperationHandle<IList<Object>> handle = Addressables.LoadAssetsAsync<Object>(key, callback, mode);
            _loadedAssetsHandles.Add(handle);
            return handle;
        }

        public void UnloadAssets(string label)
        {
            Addressables.Release(label);
        }

        public void UnloadAssets(AsyncOperationHandle<IList<Object>> handle)
        {
            Addressables.Release(handle);
        }

        public void InitializeResource()
        {
            if (!_isInitialized)
            {
                _loadedAssetsHandles = new List<AsyncOperationHandle<IList<Object>>>();
                _isInitialized = true;
            }
        }

        public void ReleaseAllLoadedAssets()
        {
            if (_loadedAssetsHandles.Count == 0) return;

            foreach (var handle in _loadedAssetsHandles)
            {
                Addressables.Release(handle);
            }
        }
    }
}