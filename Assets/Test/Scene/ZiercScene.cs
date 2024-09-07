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
        private static readonly Dictionary<string, SceneInstance> _activeScene =
            new Dictionary<string, SceneInstance>();

        public static int ActiveSceneCount => _activeScene.Count;

        public static void UnloadScene(SceneInstance scene)
        {
            Addressables.UnloadSceneAsync(scene);
        }

        public static AsyncOperationHandle<SceneInstance> LoadScene(string sceneName)
        {
            AsyncOperationHandle<SceneInstance> loadOperation =
                Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            loadOperation.Completed += op =>
            {
                _activeScene.Add(sceneName, op.Result);
            };

            return loadOperation;
        }

        public static AsyncOperation UnloadScene(string sceneName)
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
                    UnloadScene(kv.Value);
                }
            }
        }
    }
}