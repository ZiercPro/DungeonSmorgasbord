using UnityEngine;
using UnityEngine.SceneManagement;
using ZiercCode.Core.UI;
using ZiercCode.DungeonSmorgasbord.UI;
using ZiercCode.Old.Audio;

namespace ZiercCode.Old.Scene
{
    public class LoadingScene : SceneState
    {
        private readonly string _name = "LoadingScene";
        private PanelManager _panelManager;


        public override void OnEnter()
        {
            _panelManager = new PanelManager();
            if (SceneManager.GetActiveScene().name != _name)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(_name);
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
            _panelManager.PopAll();
        }

        protected override void ToDoOnSceneLoaded()
        {
            _panelManager.Push(new LoadPanel());
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            ToDoOnSceneLoaded();
        }
    }
}