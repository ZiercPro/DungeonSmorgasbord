using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZiercCode.Old.Scene
{
    public class BaseSceneState : ISceneState
    {
        public string SceneName => _sceneName;
        private string _sceneName;

        public virtual void OnEnter()
        {
            if (SceneManager.GetActiveScene().name != _sceneName)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadSceneAsync(_sceneName);
            }
            else
                DoOnSceneLoaded();
        }

        protected virtual void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
        {
            DoOnSceneLoaded();
        }

        public virtual void OnExit()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public virtual void DoOnSceneLoaded()
        {
            Debug.LogWarning($"加载之后没有没有方法调用");
        }
    }
}