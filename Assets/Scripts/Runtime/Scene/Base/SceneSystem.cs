/// <summary>
/// 场景管理系统
/// </summary>
public class SceneSystem
{
    /// <summary>
    /// 当前运行的场景
    /// </summary>
    public SceneState currentS { get; private set; }

    /// <summary>
    /// 设置并进入当前场景 
    /// </summary>
    /// <param name="scenestate">要进入的场景</param>
    public void SetScene(SceneState state)
    {
        currentS?.OnExit();
        currentS = state;
        currentS?.OnEnter();
    }
}