using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts
{
    public class SceneComponent : USingleton<SceneComponent>
    {
        private bool _isChangingScene;
        public bool IsChangingScene => _isChangingScene;

        [HideInInspector]
        public UnityEvent<string, AsyncOperation> onSceneStartLoad = new();

        [HideInInspector]
        public UnityEvent<string> onSceneLoaded = new();

        /// <summary>
        /// 加载场景 
        /// </summary>
        /// <param name="sceneName">需要加载的场景名称</param>
        /// <param name="unloadActiveSceneBeforeLoad">是否自动卸载当前的场景</param>
        public void LoadScene(string sceneName, bool unloadActiveSceneBeforeLoad = false)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName, unloadActiveSceneBeforeLoad));
        }

        public void UnloadScene(string sceneName)
        {
            Scene unloadScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.UnloadSceneAsync(unloadScene);
        }

        /// <summary>
        /// 卸载所有场景 最后卸载核心场景 在游戏退出时调用 阻塞主线程
        /// </summary>
        /// <param name="coreSceneName">核心场景名</param>
        public void UnloadAllScenes(string coreSceneName)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(coreSceneName));

            int sceneCount = SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name.Equals(coreSceneName))
                {
                    continue;
                }

                AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            }

            //StartCoroutine(UnLoadSceneCoroutine(coreSceneName));
        }

        private IEnumerator UnLoadSceneCoroutine(string coreSceneName)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(coreSceneName));

            int sceneCount = SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name.Equals(coreSceneName))
                {
                    yield return null;
                    continue;
                }

                AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
                yield return unload;
            }
        }


        private IEnumerator LoadSceneCoroutine(string targetSceneName, bool unloadActiveSceneAfterLoad)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            _isChangingScene = true;
            if (unloadActiveSceneAfterLoad)
            {
                Scene unloadScene = SceneManager.GetActiveScene();
                AsyncOperation unload = SceneManager.UnloadSceneAsync(unloadScene);
                yield return unload;
            }

            AsyncOperation load = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);

            onSceneStartLoad?.Invoke(targetSceneName, load);

            yield return load;

            Scene loadScene = SceneManager.GetSceneByName(targetSceneName);
            SceneManager.SetActiveScene(loadScene);

            _isChangingScene = false;

            onSceneLoaded?.Invoke(currentSceneName);
        }
    }
}