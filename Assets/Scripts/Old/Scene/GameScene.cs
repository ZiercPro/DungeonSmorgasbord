using UnityEngine;
using UnityEngine.SceneManagement;
using ZiercCode.Core.UI;
using ZiercCode.DungeonSmorgasbord.UI;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Manager;

namespace ZiercCode.Old.Scene
{
    public class GameScene : SceneState
    {
        private readonly string _name = "GameScene";


        public AsyncOperation AsyncOperation { get; private set; }

        public override void OnEnter()
        {
            if (SceneManager.GetActiveScene().name != _name)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                AsyncOperation = SceneManager.LoadSceneAsync(_name);
            }
            else
                ToDoOnSceneLoaded();
        }

        public override void OnExit()
        {
            AudioPlayer.Instance.ClearAudioCache();
            SceneManager.sceneLoaded -= OnSceneLoaded;
            // BattleManager.Instance.onBattleEnd.RemoveAllListeners();
        }


        protected override void ToDoOnSceneLoaded()
        {
            AudioPlayer.Instance.PlayAudioAsync(AudioName.IdleBgm);
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            ToDoOnSceneLoaded();
        }
    }
}