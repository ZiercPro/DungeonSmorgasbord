using UnityEngine.SceneManagement;
using ZiercCode.Runtime.Audio;
using ZiercCode.Runtime.UI;
using ZiercCode.Runtime.UI.Panel;

namespace ZiercCode.Runtime.Scene
{
    /// <summary>
    /// 初始场景
    /// </summary>
    public class StartScene : SceneState
    {
        private readonly string _sceneName = "Start";
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
                //执行第一次进入该场景后应该做的事情
                _panelManager.Push(new StartPanel());
            }
        }

        public override void OnExit()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            _panelManager.PopAll();
        }

        /// <summary>
        /// 场景加载完毕后执行方法
        /// </summary>
        /// <param name="scene">被加载的场景</param>
        /// <param name="mode">场景加载模式</param>
        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            _panelManager.Push(new StartPanel());
        }
    }
}