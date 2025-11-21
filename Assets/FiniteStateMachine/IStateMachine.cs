using System;

namespace ZiercCode.FiniteStateMachine
{
    /// <summary>
    /// 状态机接口
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface IStateMachine
    {
        public IState CurrentState { get; }

        public object Owner { get; }

        public void Run<T>() where T : IState;
        public void Run(Type startStateType);

        public void ChangeState<T>() where T : IState;
        public void ChangeState(Type stateType);
    }
}