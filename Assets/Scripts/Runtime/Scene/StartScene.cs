using UnityEngine.SceneManagement;

/// <summary>
/// 初始场景
/// </summary>
public class StartScene : SceneState
{
    private readonly string name="Start";
    private PanelManager panelManager;
    public override void OnEnter()
    {
        panelManager=new PanelManager();

        if (SceneManager.GetActiveScene().name != name)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(name);
		}
        else
        {
            //执行第一次进入该场景后应该做的事情
            panelManager.Push(new StartPanel());
            AudioPlayerManager.Instance.PlayAudio(Audios.menuBgm);
        }
    }

    public override void OnExit()
    {
        AudioPlayerManager.Instance.StopAudio(Audios.menuBgm);
        SceneManager.sceneLoaded -= OnSceneLoaded;
        panelManager.PopAll();
    }
    /// <summary>
    /// 场景加载完毕后执行方法
    /// </summary>
    /// <param name="scene">被加载的场景</param>
    /// <param name="mode">场景加载模式</param>
    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        panelManager.Push(new StartPanel());
        AudioPlayerManager.Instance.PlayAudio(Audios.menuBgm);
    }
}
