using UnityEngine.SceneManagement;
using ZiercCode.Core.UI;
using ZiercCode.Old.Audio;
using ZiercCode.Old.UI.Panel;

namespace ZiercCode.Old.Scene
{
    /// <summary>
    /// 初始场景
    /// </summary>
    public class StartScene : SceneState
    {
        private readonly string _sceneName = "MainMenuScene";
        private PanelManager _panelManager;

        public override void OnEnter()
        {
            _panelManager = new PanelManager();

            if (SceneManager.GetActiveScene().name != _sceneName)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(_sceneName);
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
            _panelManager.Push(new StartPanel());
            AudioPlayer.Instance.PlayAudioAsync(AudioName.MenuBgm);
        }

        /// <summary>
        /// 场景加载完毕后执行方法
        /// </summary>
        /// <param name="scene">被加载的场景</param>
        /// <param name="mode">场景加载模式</param>
        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            ToDoOnSceneLoaded();
        }
    }
}