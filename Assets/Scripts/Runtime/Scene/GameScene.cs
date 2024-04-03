using UnityEngine.SceneManagement;
using ZiercCode.Runtime.Audio;
using ZiercCode.Runtime.Manager;
using ZiercCode.Runtime.UI;
using ZiercCode.Runtime.UI.Panel;

namespace ZiercCode.Runtime.Scene
{
    public class GameScene : SceneState
    {
        private readonly string name = "Game";

        public PanelManager panelManager { get; private set; }

        public override void OnEnter()
        {
            panelManager = new PanelManager();
            if (SceneManager.GetActiveScene().name != name)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(name);
            }
            else
                ToDoOnSceneLoaded();
        }

        public override void OnExit()
        {
            AudioPlayer.Instance.ClearAudioCache();
            SceneManager.sceneLoaded -= OnSceneLoaded;
            BattleManager.Instance.onBattleEnd.RemoveAllListeners();
            panelManager.PopAll();
        }


        protected override void ToDoOnSceneLoaded()
        {
            panelManager.Push(new GamePanel());
            BattleManager.Instance.onBattleEnd.AddListener(() => { panelManager.Push(new CardPanel()); });
            AudioPlayer.Instance.PlayAudioAsync(AudioName.IdleBgm);
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            ToDoOnSceneLoaded();
        }
    }
}