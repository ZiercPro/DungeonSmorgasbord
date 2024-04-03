namespace ZiercCode.Runtime.Scene
{
    /// <summary>
    /// 游戏场景状态 
    /// </summary>
    public abstract class SceneState
    {
        /// <summary>
        /// 场景进入时执行方法
        /// </summary>
        public abstract void OnEnter();

        /// <summary>
        /// 场景退出时执行的方法
        /// </summary>
        public abstract void OnExit();

        /// <summary>
        /// 场景加载完成之后调用方法
        /// </summary>
        protected abstract void ToDoOnSceneLoaded();
    }
}