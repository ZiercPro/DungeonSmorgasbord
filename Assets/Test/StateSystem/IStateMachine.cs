namespace ZiercCode.Test.StateSystem
{
    public interface IStateMachine<T> where T : IState
    {
        public T CurrentState { get; }

        public void Initialize(T v);
        public void ChangeState(T v);
    }
}