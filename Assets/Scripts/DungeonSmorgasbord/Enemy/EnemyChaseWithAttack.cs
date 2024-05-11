using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.Old.Component;
using ZiercCode.Old.Component.Enemy;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 追击的同时自动攻击
    /// </summary>
    public class EnemyChaseWithAttack : EnemyChaseStateBase
    {
        private Health _targetHealth;
        private EnemyAttackCheck _enemyAttackCheck;

        private Vector2 _moveDir;

        public EnemyChaseWithAttack(Enemy enemyBase, EnemyStateMachine stateMachine, Animator animator,
            AutoFlipComponent autoFlipComponent, MoveComponent moveComponent, EnemyAttackCheck enemyAttackCheck
        ) : base(enemyBase, stateMachine, animator, autoFlipComponent,
            moveComponent)
        {
            _enemyAttackCheck = enemyAttackCheck;
        }

        public override void EntryState()
        {
            base.EntryState();
            _targetHealth = EnemyBase.GetTarget().GetComponent<Health>();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();

            if (EnemyBase.GetTarget() && EnemyBase.TargetIsAlive())
            {
                //如果目标存在且活着则直接追击
                _moveDir = _targetHealth.transform.position - EnemyBase.transform.position;
                MoveComponent.Move(_moveDir.normalized);

                if (_enemyAttackCheck.isEnter)
                {
                    //如果目标进入攻击范围内，则直接攻击
                    EnemyBase.Attack(_enemyAttackCheck.collier.transform);
                }
            }
            else
            {
                //目标不存在则回到静息状态
                StateMachine.ChangeState(EnemyBase.GetEnemyIdleState());
            }
        }

        public override void ExitState()
        {
            //停止移动
            MoveComponent.Stop();
            base.ExitState();
        }
    }
}