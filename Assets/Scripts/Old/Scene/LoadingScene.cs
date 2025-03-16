using UnityEngine.SceneManagement;
using ZiercCode.Core.UI;

namespace ZiercCode.Old.Scene
{
    public class LoadingScene : BaseSceneState
    {
     
        private PanelManager _panelManager;
        

        public override void OnExit()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            //AudioPlayer.Instance.ClearAudioCache();
            _panelManager.PopAll();
        }
    }
}