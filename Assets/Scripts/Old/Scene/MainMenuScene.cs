using UnityEngine.SceneManagement;
using ZiercCode.Core.UI;
using ZiercCode.DungeonSmorgasbord.UI;
using ZiercCode.Old.Audio;

namespace ZiercCode.Old.Scene
{
    /// <summary>
    /// 初始场景
    /// </summary>
    public class MainMenuScene : SceneState
    {
        private readonly string _sceneName = "MainMenuScene";
        private PanelManager _panelManager;

        public override void OnEnter()
        {
            _panelManager = new PanelManager();

            if (SceneManager.GetActiveScene().name != _sceneName)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadSceneAsync(_sceneName);
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
            _panelManager.Push(new MainMenuPanel());
            AudioPlayer.Instance.PlayAudioAsync(AudioName.MenuBgm);
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            ToDoOnSceneLoaded();
        }
    }
}