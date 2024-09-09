using UnityEngine;
using ZiercCode.Test.Reference;
using ZiercCode.Test.StateMachine;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 敌人状态基类
    /// </summary>
    public abstract class EnemyState : IState, IReference
    {
        protected readonly Enemy EnemyBase;
        protected readonly Animator Animator;
        private readonly StateMachine _stateMachine;
        public IStateMachine StateMachine => _stateMachine;

        public EnemyState(Enemy enemyBase, StateMachine stateMachine, Animator animator)
        {
            EnemyBase = enemyBase;
            _stateMachine = stateMachine;
            Animator = animator;
        }

        /// <summary>
        /// 物理帧更新
        /// </summary>
        public virtual void PhysicsUpdate() { }

        /// <summary>
        /// 动画事件调用
        /// </summary>
        public virtual void AnimationTriggerEvent() { }

        public virtual void OnCreate(IStateMachine stateMachine) { }

        public virtual void OnEnter() { }

        public virtual void OnUpdate() { }

        public virtual void OnExit() { }

        public void OnSpawn()
        {
        }
    }
}