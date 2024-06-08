using ZiercCode.Test.StateMachine;

namespace ZiercCode.Test.Procedure
{
    /// <summary>
    /// 流程接口
    /// </summary>
    public abstract class ProcedureBase : IState
    {
        public IStateMachine StateMachine => _stateMachine;

        private IStateMachine _stateMachine;

        public virtual void OnCreate(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}