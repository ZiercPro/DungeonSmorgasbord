namespace ZiercCode.Test.StateMachine
{
    /// <summary>
    /// 状态接口
    /// </summary>
    public interface IState
    {
        public IStateMachine StateMachine { get; }

        /// <summary>
        /// 实例化时 需要专属的状态机实例
        /// </summary>
        /// <param name="stateMachine"></param>
        public void OnCreate(IStateMachine stateMachine);

        public void OnEnter();
        public void OnUpdate();
        public void OnExit();
    }
}