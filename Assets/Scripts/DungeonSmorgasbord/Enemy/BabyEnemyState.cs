using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.Old.Component.Enemy;
using ZiercCode.Test.StateMachine;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class BabyEnemyState
    {
        public class Idle : EnemyState
        {
            private readonly int _idle = Animator.StringToHash("idle");
            private readonly MoveComponent _moveComponent;

            public Idle(Enemy enemyBase, StateMachine stateMachine, Animator animator,
                MoveComponent moveComponent) : base(enemyBase,
                stateMachine, animator)
            {
                _moveComponent = moveComponent;
            }

            public override void OnEnter()
            {
                Animator.SetBool(_idle, true);
                _moveComponent.Stop();
            }

            public override void OnUpdate()
            {
                //寻找目标
                if (EnemyBase.GetTarget() && EnemyBase.TargetIsAlive())
                {
                    //如果寻找到目标且目标存活 则根据情况 进入追击、攻击或逃离的状态

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
            private readonly int _running = Animator.StringToHash("running");
            private readonly EnemyAttackCheck _enemyAttackCheck;
            private readonly MoveComponent _moveComponent;
            private Vector2 _moveDir;

            public Chase(Enemy enemyBase, StateMachine stateMachine, Animator animator,
                MoveComponent moveComponent, EnemyAttackCheck enemyAttackCheck) : base(enemyBase,
                stateMachine, animator)
            {
                _moveComponent = moveComponent;
                _enemyAttackCheck = enemyAttackCheck;
            }

            public override void OnEnter()
            {
                Animator.SetBool(_running, true);
            }

            public override void OnUpdate()
            {
                if (EnemyBase.GetTarget() && EnemyBase.TargetIsAlive())
                {
                    _moveDir = (EnemyBase.GetTarget().position - EnemyBase.transform.position).normalized;
                    _moveComponent.Move(_moveDir);

                    if (_enemyAttackCheck.isEnter)
                    {
                        EnemyBase.OnAttack(EnemyBase.GetTarget());
                    }
                }
                else
                {
                    StateMachine.ChangeState<Chase>();
                }
            }

            public override void OnExit()
            {
                _moveComponent.Stop();
                Animator.SetBool(_running, false);
            }
        }
    }
}