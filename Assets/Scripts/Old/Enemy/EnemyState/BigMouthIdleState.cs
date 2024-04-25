using UnityEngine;

namespace ZiercCode.Old.Enemy.EnemyState
{
    public class BigMouthIdleState : BigMouthNormalState
    {
        private static readonly int Idle = Animator.StringToHash("idle");

        public BigMouthIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, Enemy_BigMouse enemy) : base(
            enemyBase,
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
            if (!_enemy.attackTarget.IsDead)
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