using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 静息时不移动
    /// </summary>
    public class EnemyIdleNoMove : EnemyIdleStateBase
    {
        private MoveComponent _moveComponent;

        public EnemyIdleNoMove(Enemy enemyBase, EnemyStateMachine stateMachine, Animator animator,
            MoveComponent moveComponent) : base(enemyBase, stateMachine, animator)
        {
            _moveComponent = moveComponent;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _moveComponent.Stop();
        }
        
    }
}