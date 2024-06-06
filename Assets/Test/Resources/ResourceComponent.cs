using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ZiercCode.Test.Resources
{
    public class ResourceComponent : MonoBehaviour
    {
        /// <summary>
        /// 要加载的资源标签
        /// </summary>
        [Header("要加载的资源标签")] [SerializeField] private List<AssetLabelReference> _assetLabelReferences;

        private void Awake()
        {
            InitializeResource();
        }

        public void InitializeResource()
        {
            LoadAllAssetsFromAssetLabel();
        }

        private void LoadAllAssetsFromAssetLabel()
        {
            if (_assetLabelReferences == null || _assetLabelReferences.Count == 0)
            {
                Debug.LogWarning("没有设置要加载的资源!");
                return;
            }

            foreach (var labelReference in _assetLabelReferences)
            {
                Addressables.LoadAssetsAsync<Object>(labelReference, OnAssetLoaded);
            }
        }


        private void OnAssetLoaded(Object obj)
        {
            if (obj == null)
            {
                Debug.LogError("资源加载失败!");
                return;
            }

            Debug.Log(obj.name);
            //todo 注册到对象池
        }
    }
}