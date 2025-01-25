using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.Test.StateMachine;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class BigMouseEnemyState
    {
        public class Idle : EnemyState
        {
            private readonly int _idle;

            public Idle(Enemy enemyBase, StateMachine stateMachine, Animator animator, string idleAnimationPara) : base(
                enemyBase, stateMachine,
                animator)
            {
                _idle = Animator.StringToHash(idleAnimationPara);
            }

            public override void OnEnter()
            {
                Animator.SetBool(_idle, true);
            }

            public override void OnUpdate()
            {
                if (EnemyBase.GetTarget() && EnemyBase.TargetIsAlive())
                {
                    StateMachine.ChangeState<Chase>();
                }
            }

            public override void OnExit()
            {
                Animator.SetBool(_idle, false);
            }
        }

        public class Chase : EnemyState
        {
            private readonly int _chase;
            private readonly MoveComponent _moveComponent;
            private Vector2 _moveDir;

            public Chase(Enemy enemyBase, StateMachine stateMachine, Animator animator, string chaseAnimationPara,
                MoveComponent moveComponent) : base(enemyBase, stateMachine,
                animator)
            {
                _chase = Animator.StringToHash(chaseAnimationPara);
                _moveComponent = moveComponent;
            }

            public override void OnEnter()
            {
                Animator.SetBool(_chase, true);
            }

            public override void OnUpdate()
            {
                if (EnemyBase.GetTarget() && EnemyBase.TargetIsAlive())
                {
                    _moveDir = (EnemyBase.GetTarget().position - EnemyBase.transform.position).normalized;
                    _moveComponent.Move(_moveDir);
                }
                else
                {
                    StateMachine.ChangeState<Idle>();
                }
            }

            public override void OnExit()
            {
                Animator.SetBool(_chase, false);
                _moveComponent.Stop();
            }
        }
    }
}