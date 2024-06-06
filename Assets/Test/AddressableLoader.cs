using NaughtyAttributes.Scripts.Core.DrawerAttributes_SpecialCase;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace ZiercCode.Test
{
    public class AddressableLoader : MonoBehaviour
    {
        private Dictionary<string, Object> _entityDic;
        [SerializeField] private string id;

        private void Awake()
        {
            _entityDic = new Dictionary<string, Object>();
        }

        private void Start()
        {
            Debug.Log(Time.realtimeSinceStartup);
            //  Addressables.LoadAssetAsync<GameObject>(id).Completed += OnLoadComplete;

            GameObject g = AssetDatabase.LoadAssetAtPath<GameObject>(id);
            Debug.Log(Time.realtimeSinceStartup);

            _entityDic.Add(id, g);
        }

        private void OnLoadComplete(AsyncOperationHandle<GameObject> handle)
        {
            GameObject g = Instantiate(handle.Result);
            _entityDic.Add(id, g);
            Addressables.Release(handle);
        }

        [Button("测试")]
        public void InfoLog()
        {
            Instantiate(_entityDic[id]);
        }
    }
}