using UnityEngine;
using ZiercCode.FiniteStateMachine;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.EnemyClasses
{
    public abstract class EnemyState_Base : IState
    {
        public IStateMachine StateMachine => stateMachine;

        protected IStateMachine stateMachine;

        protected Animator Animator;

        protected Enemy_Base MyEnemy => (Enemy_Base)stateMachine.Owner;

        /// <summary>
        /// 默认只检测一个目标
        /// </summary>
        protected RangeDetector RangeDetector;

        public EnemyState_Base(Animator myAnimator)
        {
            Animator = myAnimator;
            RangeDetector = new RangeDetector(1);
        }

        public void OnCreate(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();
    }
}