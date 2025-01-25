using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Manager;
using ZiercCode.DungeonSmorgasbord.Component;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class BigMouseEnemy : Enemy
    {
        [Header("粒子效果"), Space] [SerializeField]
        private PoolObjectSo deadParticle;

        private BigMouseEnemyState.Idle idleState;
        private BigMouseEnemyState.Chase chaseState;

        protected override void Awake()
        {
            base.Awake();
            idleState = new BigMouseEnemyState.Idle(this, StateMachine, animator, "idle");
            chaseState = new BigMouseEnemyState.Chase(this, StateMachine, animator, "running", moveComponent);
        }

        protected override void Start()
        {
            base.Start();
            StateMachine.AddState(idleState);
            StateMachine.AddState(chaseState);

            StateMachine.Run<BigMouseEnemyState.Idle>();
        }

        public override void OnAttack(Transform target)
        {
            //弹出文本
            TextPopupSpawner.Instance.InitPopupText(target.position, Color.red, -attribute.AttributesData.damageAmount);
            //击退
            KnockBackFeedBack knockBackFeedBack = GetComponent<KnockBackFeedBack>();
            knockBackFeedBack.StartBackMove(transform, 0.5f);
        }

        public override void Dead()
        {
            poolObjectSpawner.SpawnPoolObjectWithAutoRelease(deadParticle, transform.position, 5f);
            AudioPlayer.Instance.PlaySfx("EnemyDead4");
            base.Dead();
        }
    }
}