using UnityEngine;
using ZiercCode.Test.StateSystem;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 敌人状态基类
    /// </summary>
    public class EnemyState : IState
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

        public virtual void OnCreate() { }

        /// <summary>
        /// 进入状态时调用
        /// </summary>
        public virtual void OnEnter() { }

        /// <summary>
        /// 帧更新
        /// </summary>
        public virtual void OnUpdate() { }

        /// <summary>
        /// 物理帧更新
        /// </summary>
        public virtual void PhysicsUpdate() { }

        /// <summary>
        /// 离开状态时调用
        /// </summary>
        public virtual void OnExit() { }

        /// <summary>
        /// 动画事件调用
        /// </summary>
        public virtual void AnimationTriggerEvent() { }
    }
}