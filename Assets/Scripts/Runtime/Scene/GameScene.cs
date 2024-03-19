using UnityEngine.SceneManagement;

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
            AudioPlayerManager.Instance.PlayAudio(Audios.gameidleBgm);
        }
    }

    public override void OnExit()
    {
        AudioPlayerManager.Instance.StopAudio(Audios.gameidleBgm);
        SceneManager.sceneLoaded -= OnSceneLoaded;
        BattleManager.Instance.OnBattleEnd.RemoveAllListeners();
        panelManager.PopAll();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        panelManager.Push(new GamePanel());
        BattleManager.Instance.OnBattleEnd.AddListener(() => { panelManager.Push(new CardPanel()); });
        GameRoot.Instance.OnTab.AddListener(() => { panelManager.Push(new HeroAttributesPanel()); });
        AudioPlayerManager.Instance.PlayAudio(Audios.gameidleBgm);
    }
}