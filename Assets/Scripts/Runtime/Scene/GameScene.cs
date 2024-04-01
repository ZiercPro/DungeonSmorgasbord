using UnityEngine.SceneManagement;

namespace ZRuntime
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
            {
                panelManager.Push(new GamePanel());
                BattleManager.Instance.OnBattleEnd.AddListener(() => { panelManager.Push(new CardPanel()); });
                GameRoot.Instance.OnTab.AddListener(() => { panelManager.Push(new HeroAttributesPanel()); });
                AudioPlayer.Instance.PlayAudioAsync(AudioName.IdleBgm);
            }
        }

        public override void OnExit()
        {
            AudioPlayer.Instance.StopAudioAsync(AudioName.IdleBgm);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            BattleManager.Instance.OnBattleEnd.RemoveAllListeners();
            panelManager.PopAll();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            panelManager.Push(new GamePanel());
            BattleManager.Instance.OnBattleEnd.AddListener(() => { panelManager.Push(new CardPanel()); });
            GameRoot.Instance.OnTab.AddListener(() => { panelManager.Push(new HeroAttributesPanel()); });
            AudioPlayer.Instance.PlayAudioAsync(AudioName.IdleBgm);
        }
    }
}