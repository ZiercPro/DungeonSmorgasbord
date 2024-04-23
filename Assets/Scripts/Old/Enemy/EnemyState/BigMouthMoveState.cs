using UnityEngine;

namespace ZiercCode.Old.Enemy.EnemyState
{
    public class BigMouthMoveState : BigMouthNormalState
    {
        private Vector2 _moveDir;
        private Transform _targetTransform;

        private static readonly int Running = Animator.StringToHash("running");

        public BigMouthMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, Enemy_BigMouse enemy) : base(
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
            _enemy.animator.SetBool(Running, true);
            _targetTransform = _enemy.attackTarget.transform;
        }

        public override void ExitState()
        {
            base.ExitState();
            _enemy.animator.SetBool(Running, false);
            _targetTransform = null;
            enemyBase.MoveComponent.Stop();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            _moveDir = ((Vector2)(_targetTransform.position - _enemy.transform.position)).normalized;
            if (_enemy.attackTarget.isDead)
            {
                stateMachine.ChangeState(_enemy.idleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            enemyBase.MoveComponent.Move(_moveDir);
        }
    }
}