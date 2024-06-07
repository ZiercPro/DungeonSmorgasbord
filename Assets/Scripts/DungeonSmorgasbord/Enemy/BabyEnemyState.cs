using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.Old.Component.Enemy;
using ZiercCode.Test.StateSystem;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class BabyEnemyState
    {
        public class Idle : EnemyState
        {
            private readonly int _idle = Animator.StringToHash("idle");
            private EnemyAttackCheck _enemyAttackCheck;
            private MoveComponent _moveComponent;

            public Idle(Enemy enemyBase, StateMachine stateMachine, Animator animator,
                EnemyAttackCheck enemyAttackCheck, MoveComponent moveComponent) : base(enemyBase,
                stateMachine, animator)
            {
                _enemyAttackCheck = enemyAttackCheck;
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

                    if (_enemyAttackCheck.isEnter)
                    {
                        StateMachine.ChangeState<BabyEnemyState.Chase>();
                    }
                }
            }

            public override void OnExit()
            {
                Animator.SetBool(_idle, false);
            }
        }

        public class Chase : EnemyState
        {
            private EnemyAttackCheck _enemyAttackCheck;
            private MoveComponent _moveComponent;

            public Chase(Enemy enemyBase, StateMachine stateMachine, Animator animator,
                MoveComponent moveComponent, EnemyAttackCheck enemyAttackCheck) : base(enemyBase,
                stateMachine, animator)
            {
                _moveComponent = moveComponent;
            }

            public override void OnEnter()
            {
            }

            public override void OnUpdate()
            {
                if (_enemyAttackCheck.isEnter)
                {
                }
            }

            public override void OnExit()
            {
            }
        }
    }
}