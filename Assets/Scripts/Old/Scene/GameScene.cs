using UnityEngine.SceneManagement;
using ZiercCode.Old.Audio;

namespace ZiercCode.Old.Scene
{
    public class GameScene : BaseSceneState
    {
        private readonly string _name = "GameScene";

        public override void OnExit()
        {
            AudioPlayer.Instance.ClearAudioCache();
            SceneManager.sceneLoaded -= OnSceneLoaded;
            // BattleManager.Instance.onBattleEnd.RemoveAllListeners();
        }
        //todo 重写

        public override void DoOnSceneLoaded()
        {
            AudioPlayer.Instance.PlayAudioAsync(AudioName.IdleBgm);
        }
    }
}