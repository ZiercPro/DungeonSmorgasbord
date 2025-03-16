using ZiercCode.Core.UI;

namespace ZiercCode.Old.Scene
{
    /// <summary>
    /// 初始场景
    /// </summary>
    public class MainMenuScene : BaseSceneState
    {

        private PanelManager _panelManager;

     
        public override void OnExit()
        {
            base.OnExit();
            // AudioPlayer.Instance.ClearAudioCache();
            _panelManager.PopAll();
        }
    }
}