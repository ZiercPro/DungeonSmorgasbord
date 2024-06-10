using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using ZiercCode.Test.Base;
using ZiercCode.Test.ObjectPool;

namespace ZiercCode.Test.Resources
{
    public class ResourceComponent : ZiercComponent
    {
        private bool _isInitialized;

        private AsyncOperationHandle<IList<Object>> _resourcesHandle;
        public bool IsInitialized => _isInitialized;

        [SerializeField] private List<string> loadAssetLabels;


        public void InitializeResource()
        {
            if (!_isInitialized)
            {
                LoadAllAssetsFromAssetLabel();
            }
            else
            {
                Debug.LogWarning("资源已经实例化!");
            }
        }

        public void Release()
        {
            Addressables.Release(_resourcesHandle);
        }

        private void LoadAllAssetsFromAssetLabel()
        {
            if (loadAssetLabels == null || loadAssetLabels.Count == 0)
            {
                _isInitialized = true;
                return;
            }

            foreach (var labelReference in loadAssetLabels)
            {
                Addressables.LoadAssetsAsync<Object>(labelReference, OnAssetLoaded).Completed += OnAssetsLoadCompleted;
            }
        }

        private void OnAssetLoaded(Object obj)
        {
            if (obj == null)
            {
                Debug.LogError("资源加载失败!");
                return;
            }

            Debug.Log($"加载资源 | {obj.name}");

            ZiercPool.Register(obj.name, obj);
        }

        private void OnAssetsLoadCompleted(AsyncOperationHandle<IList<Object>> handle)
        {
            _resourcesHandle = handle;
            _isInitialized = true;
        }
    }
}