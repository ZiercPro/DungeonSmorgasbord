using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZiercCode.Test.Scene
{
    public static class ZiercScene
    {
        private static readonly Dictionary<string, UnityEngine.SceneManagement.Scene> _activeScene =
            new Dictionary<string, UnityEngine.SceneManagement.Scene>();
        public static int ActiveSceneCount => _activeScene.Count;

        public static void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Additive)
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            load.completed += op =>
            {
                UnityEngine.SceneManagement.Scene newS = SceneManager.GetSceneByName(sceneName);
                _activeScene.Add(sceneName, newS);
            };
        }

        public static void UnLoadScene(string sceneName)
        {
            if (_activeScene.ContainsKey(sceneName))
            {
                AsyncOperation unLoad = SceneManager.UnloadSceneAsync(sceneName);
                unLoad.completed += op =>
                {
                    _activeScene.Remove(sceneName);
                };
            }
            else
            {
                Debug.LogWarning($"场景{sceneName}未加载!");
            }
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