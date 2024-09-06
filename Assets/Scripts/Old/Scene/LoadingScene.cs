using UnityEngine.SceneManagement;
using ZiercCode.Core.UI;
using ZiercCode.Old.Audio;

namespace ZiercCode.Old.Scene
{
    public class LoadingScene : BaseSceneState
    {
        private readonly string _name = "LoadingScene";
        private PanelManager _panelManager;

        //todo 重写

        public override void OnExit()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            //AudioPlayer.Instance.ClearAudioCache();
            _panelManager.PopAll();
        }
    }
}