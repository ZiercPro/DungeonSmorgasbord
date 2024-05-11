using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 静息状态
    /// </summary>
    public abstract class EnemyIdleStateBase : EnemyState
    {
        private readonly int _idle = Animator.StringToHash("idle");

        public EnemyIdleStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, Animator animator) : base(enemyBase,
            stateMachine, animator)
        {
        }

        public override void EntryState()
        {
            Animator.SetBool(_idle, true);
        }

        public override void FrameUpdate()
        {
            //寻找目标
            if (EnemyBase.GetTarget() && EnemyBase.TargetIsAlive())
            {
                //如果寻找到目标且目标存活 则直接转换到追击状态
                StateMachine.ChangeState(EnemyBase.GetEnemyChaseState());
            }
        }

        public override void ExitState()
        {
            Animator.SetBool(_idle, false);
        }
    }
}