using UnityEngine.SceneManagement;

namespace ZiercCode.Old.Scene
{
    public class GameScene : BaseSceneState
    {
        public override void OnExit()
        {
            //  AudioPlayer.Instance.ClearAudioCache();
            SceneManager.sceneLoaded -= OnSceneLoaded;
            // BattleManager.Instance.onBattleEnd.RemoveAllListeners();
        }

        public override void DoOnSceneLoaded()
        {
            //AudioPlayer.Instance.PlayAudio(AudioName.IdleBgm);
        }
    }
}