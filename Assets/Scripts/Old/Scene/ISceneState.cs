namespace ZiercCode.Old.Scene
{
    /// <summary>
    /// 游戏场景状态 
    /// </summary>
    public interface ISceneState
    {
        public string SceneName { get; }

        /// <summary>
        /// 场景进入时执行方法
        /// </summary>
        public void OnEnter();

        /// <summary>
        /// 场景退出时执行的方法
        /// </summary>
        public void OnExit();

        /// <summary>
        /// 加载完场景调用
        /// </summary>
        public void DoOnSceneLoaded();
    }
}