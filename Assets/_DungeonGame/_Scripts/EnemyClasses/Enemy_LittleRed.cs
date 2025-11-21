using UnityEngine;
using ZiercCode.FiniteStateMachine;

namespace ZiercCode._DungeonGame._Scripts.EnemyClasses
{
    public class Enemy_LittleRed : Enemy_Base
    {
        public Vector2 chaseRange;

        private IState _idleState;
        private IState _chaseState;

        protected override void Awake()
        {
            base.Awake();
            _idleState = new EnemyState_LittleRed_IdleState(animator);
            _chaseState = new EnemyState_LittleRed_ChaseState(animator);
        }

        protected override void Start()
        {
            base.Start();
            StateMachine.AddState(_idleState);
            StateMachine.AddState(_chaseState);

            StateMachine.Run<EnemyState_LittleRed_IdleState>();
        }

        protected override void DeathCheck()
        {
            if (CurrentHealth <= 0 && !isDead)
            {
                isDead = true;

                StateMachine.ChangeState<EnemyState_LittleRed_IdleState>();
            }
        }

        protected override void PauseUpdate()
        {
            base.PauseUpdate();
            if (!isDead)
                StateMachine.CurrentState.OnUpdate();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, chaseRange.x);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRange.y);
        }
#endif
    }
}