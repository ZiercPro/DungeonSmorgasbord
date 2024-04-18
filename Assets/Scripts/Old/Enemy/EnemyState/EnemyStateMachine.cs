namespace ZiercCode.Old.Enemy.EnemyState
{
    /// <summary>
    /// 敌人状态机，储存状态信息,控制状态的改变
    /// </summary>
    public class EnemyStateMachine
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        public EnemyState currentState { get; private set; }

        /// <summary>
        /// 初始化状态
        /// </summary>
        /// <param name="startState">初始状态</param>
        public void Initialize(EnemyState startState)
        {
            currentState = startState;
            currentState.EntryState();
        }

        public void ChangeState(EnemyState targetState)
        {
            currentState.ExitState();
            currentState = targetState;
            currentState.EntryState();
        }
    }
}