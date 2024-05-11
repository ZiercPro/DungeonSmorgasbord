using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 敌人状态基类
    /// </summary>
    public class EnemyState
    {
        protected Enemy EnemyBase;
        protected Animator Animator;
        protected EnemyStateMachine StateMachine;

        public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, Animator animator)
        {
            EnemyBase = enemyBase;
            StateMachine = stateMachine;
            Animator = animator;
        }

        /// <summary>
        /// 进入状态时调用
        /// </summary>
        public virtual void EntryState() { }

        /// <summary>
        /// 帧更新
        /// </summary>
        public virtual void FrameUpdate() { }

        /// <summary>
        /// 物理帧更新
        /// </summary>
        public virtual void PhysicsUpdate() { }

        /// <summary>
        /// 离开状态时调用
        /// </summary>
        public virtual void ExitState() { }

        /// <summary>
        /// 动画事件调用
        /// </summary>
        public virtual void AnimationTriggerEvent() { }
    }
}