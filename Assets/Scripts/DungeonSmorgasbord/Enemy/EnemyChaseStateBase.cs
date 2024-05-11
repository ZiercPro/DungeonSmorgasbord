using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 追击状态
    /// </summary>
    public abstract class EnemyChaseStateBase : EnemyState
    {
        private readonly int _running = Animator.StringToHash("running");

        protected AutoFlipComponent AutoFlipComponent;
        protected MoveComponent MoveComponent;

        public EnemyChaseStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, Animator animator,
            AutoFlipComponent autoFlipComponent , MoveComponent moveComponent) : base(enemyBase, stateMachine, animator)
        {
            AutoFlipComponent = autoFlipComponent;
            MoveComponent = moveComponent;
        }

        public override void EntryState()
        {
            Animator.SetBool(_running,true);
        }

        public override void FrameUpdate()
        {
            AutoFlipComponent.FaceTo(EnemyBase.GetTarget().position);
        }

        public override void ExitState()
        {
            Animator.SetBool(_running,false);
        }
    }
}