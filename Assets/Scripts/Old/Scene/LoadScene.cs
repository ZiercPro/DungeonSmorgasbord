using UnityEngine.SceneManagement;
using ZiercCode.Core.UI;
using ZiercCode.DungeonSmorgasbord.UI;
using ZiercCode.Old.Audio;

namespace ZiercCode.Old.Scene
{
    public class LoadScene : SceneState
    {
        private readonly string name = "LoadingScene";
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
                ToDoOnSceneLoaded();
            }
        }

        public override void OnExit()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            AudioPlayer.Instance.ClearAudioCache();
            PanelManager.PopAll();
        }

        protected override void ToDoOnSceneLoaded()
        {
            PanelManager.Push(new LoadPanel());
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            ToDoOnSceneLoaded();
        }
    }
}