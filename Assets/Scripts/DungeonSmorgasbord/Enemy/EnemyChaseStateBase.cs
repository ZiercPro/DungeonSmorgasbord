using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.Old.Component.Enemy;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 追击状态基类
    /// 只进行动画的处理
    /// </summary>
    public abstract class EnemyChaseStateBase : EnemyState
    {
        private readonly int _running = Animator.StringToHash("running");

        protected AutoFlipComponent AutoFlipComponent;
        protected EnemyAttackCheck EnemyAttackCheck;
        protected MoveComponent MoveComponent;
        protected Vector2 MoveDir;

        public EnemyChaseStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, Animator animator,
            AutoFlipComponent autoFlipComponent, MoveComponent moveComponent, EnemyAttackCheck enemyAttackCheck) : base(
            enemyBase, stateMachine, animator)
        {
            AutoFlipComponent = autoFlipComponent;
            EnemyAttackCheck = enemyAttackCheck;
            MoveComponent = moveComponent;
        }

        public override void OnEnter()
        {
            Animator.SetBool(_running, true);
        }

        public override void OnUpdate()
        {
            if (EnemyBase.GetTarget() && EnemyBase.TargetIsAlive())
            {
                //如果目标存活 则追击
                AutoFlipComponent.FaceTo(EnemyBase.GetTarget().position);
                MoveDir = EnemyBase.GetTarget().transform.position - EnemyBase.transform.position;
                MoveComponent.Move(MoveDir.normalized);

                if (EnemyAttackCheck.isEnter)
                {
                    //如果进入了攻击范围 则进入攻击状态
                    StateMachine.ChangeState(EnemyBase.GetEnemyAttackState());
                }
            }
            else
            {
                //目标不存在则回到静息状态
                StateMachine.ChangeState(EnemyBase.GetEnemyIdleState());
            }
        }

        public override void OnExit()
        {
            MoveComponent.Stop();
            Animator.SetBool(_running, false);
        }
    }
}