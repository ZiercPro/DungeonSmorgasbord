using ZiercCode.Test.StateSystem;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 敌人状态机，储存状态信息,控制状态的改变
    /// </summary>
    public class EnemyStateMachine : IStateMachine<EnemyState>
    {
        private EnemyState _currentState;

        /// <summary>
        /// 当前状态
        /// </summary>
        public EnemyState CurrentState => _currentState;

        /// <summary>
        /// 初始化状态
        /// </summary>
        /// <param name="startState">初始状态</param>
        public void Initialize(EnemyState startState)
        {
            _currentState = startState;
            CurrentState.OnEnter();
        }

        public void ChangeState(EnemyState targetState)
        {
            CurrentState.OnExit();
            _currentState = targetState;
            CurrentState.OnEnter();
        }
    }
}