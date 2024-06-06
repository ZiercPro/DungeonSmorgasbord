namespace ZiercCode.Old.Scene
{
    /// <summary>
    /// 场景管理系统
    /// </summary>
    public class SceneSystem 
    {
        /// <summary>
        /// 当前运行的场景
        /// </summary>
        private static ISceneState _currentS;

        /// <summary>
        /// 设置并进入当前场景 
        /// </summary>
        /// <param name="state">要进入的场景</param>
        public static void SetScene(ISceneState state)
        {
            _currentS?.OnExit();
            _currentS = state;
            _currentS?.OnEnter();
        }
    }
}