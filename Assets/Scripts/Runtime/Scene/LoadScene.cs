using UnityEngine.SceneManagement;

namespace ZRuntime
{

    public class LoadScene : SceneState
    {
        private readonly string name = "Loading";
        private PanelManager PanelManager;

        public override void OnEnter()
        {
            PanelManager = new PanelManager();
            if (SceneManager.GetActiveScene().name != name)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(name);
            }
            else
            {
                PanelManager.Push(new LoadPanel());
            }
        }

        public override void OnExit()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            PanelManager.PopAll();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            PanelManager.Push(new LoadPanel());
        }
    }
}