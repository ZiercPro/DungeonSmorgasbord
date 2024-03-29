using UnityEngine;
using Runtime.Enemy.EnemyState.Base;

namespace Runtime.Enemy.EnemyState.States.Baby
{
    public class BabyIdleState : BabyNormalState
    {
        private static readonly int Idle = Animator.StringToHash("idle");

        public BabyIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, Enemy_Baby enemy) : base(enemyBase,
            stateMachine, enemy)
        {
        }

        public override void AnimationTriggerEvent()
        {
            base.AnimationTriggerEvent();
        }

        public override void EntryState()
        {
            base.EntryState();
            _enemy.animator.SetBool(Idle, true);
        }

        public override void ExitState()
        {
            base.ExitState();
            _enemy.animator.SetBool(Idle, false);
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            if (_enemy.attackTarget)
            {
                stateMachine.ChangeState(_enemy.moveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}