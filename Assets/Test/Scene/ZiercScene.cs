using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace ZiercCode.Test.Scene
{
    public static class ZiercScene
    {
        private static readonly Dictionary<string, UnityEngine.SceneManagement.Scene> _activeScene =
            new Dictionary<string, UnityEngine.SceneManagement.Scene>();

        public static int ActiveSceneCount => _activeScene.Count;

        public static AsyncOperationHandle<SceneInstance> LoadSceneAsset(string sceneName)
        {
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneName);
            return handle;
        }

        public static void UnloadScene(SceneInstance scene)
        {
            Addressables.UnloadSceneAsync(scene);
        }

        public static AsyncOperation LoadScene(string sceneName)
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            load.completed += op =>
            {
                UnityEngine.SceneManagement.Scene newS = SceneManager.GetSceneByName(sceneName);
                _activeScene.Add(sceneName, newS);
            };
            return load;
        }

        public static AsyncOperation UnLoadScene(string sceneName)
        {
            AsyncOperation unLoad;
            if (_activeScene.ContainsKey(sceneName))
            {
                unLoad = SceneManager.UnloadSceneAsync(sceneName);
                unLoad.completed += op =>
                {
                    _activeScene.Remove(sceneName);
                };
            }
            else
            {
                unLoad = null;
                Debug.LogWarning($"场景{sceneName}未加载!");
            }

            return unLoad;
        }

        public static void UnloadAllActiveScene()
        {
            if (_activeScene.Count > 0)
            {
                foreach (var kv in _activeScene)
                {
                    UnLoadScene(kv.Key);
                }
            }
        }
    }
}