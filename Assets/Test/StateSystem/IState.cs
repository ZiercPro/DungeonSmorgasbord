namespace ZiercCode.Test.StateSystem
{
    public interface IState
    {
        public void OnCreate();
        public void OnEnter();
        public void OnUpdate();
        public void OnExit();
    }
}