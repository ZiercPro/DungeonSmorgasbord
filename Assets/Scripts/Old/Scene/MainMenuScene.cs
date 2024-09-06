using UnityEngine.SceneManagement;
using ZiercCode.Core.UI;
using ZiercCode.Old.Audio;

namespace ZiercCode.Old.Scene
{
    /// <summary>
    /// 初始场景
    /// </summary>
    public class MainMenuScene : BaseSceneState
    {
        private readonly string _sceneName = "MainMenuScene";

        private PanelManager _panelManager;

        //todo 重写
        public override void OnExit()
        {
            base.OnExit();
            // AudioPlayer.Instance.ClearAudioCache();
            _panelManager.PopAll();
        }
    }
}