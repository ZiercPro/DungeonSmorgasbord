using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame
{
    public class SceneComponent : USingleton<SceneComponent>
    {
        private bool _isChangingScene;

        public bool IsChangingScene => _isChangingScene;

        //加载场景 自动卸载之前活动场景
        public void LoadScene(string sceneName, bool unloadActiveSceneBeforeLoad = false)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName, unloadActiveSceneBeforeLoad));
        }

        public void UnloadScene(string sceneName)
        {
            Scene unloadScene = SceneManager.GetSceneByName(sceneName);
            SceneManager.UnloadSceneAsync(unloadScene);
        }

        //卸载所有场景 最后卸载核心场景 在游戏退出时调用 阻塞主线程
        public void UnloadAllScenes(string coreSceneName)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(coreSceneName));

            int sceneCount = SceneManager.sceneCount;

            for (int i = 0; i < sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name.Equals(coreSceneName)) continue;

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

        private IEnumerator LoadSceneCoroutine(string sceneName, bool unloadActiveSceneAfterLoad)
        {
            _isChangingScene = true;
            if (unloadActiveSceneAfterLoad)
            {
                Scene unloadScene = SceneManager.GetActiveScene();
                AsyncOperation unload = SceneManager.UnloadSceneAsync(unloadScene);
                yield return unload;
            }

            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Scene loadScene = SceneManager.GetSceneByName(sceneName);

            yield return load;

            SceneManager.SetActiveScene(loadScene);
            _isChangingScene = false;
        }
    }
}