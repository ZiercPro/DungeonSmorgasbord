using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Manager;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class BabyEnemy : Enemy
    {
        [Header("粒子效果"), Space] [SerializeField]
        private PoolObjectSo deadParticle;

        protected override void Start()
        {
            base.Start();

            StateMachine.AddState<BabyEnemyState.Idle>();
            StateMachine.AddState<BabyEnemyState.Chase>();

            StateMachine.Run<BabyEnemyState.Idle>();
        }

        public override void OnAttack(Transform target)
        {
            //弹出文本
            TextPopupSpawner.Instance.InitPopupText(target.position, Color.red, -attribute.AttributesData.damageAmount);
            //击退
            KnockBackFeedBack knockBackFeedBack = target.GetComponent<KnockBackFeedBack>();
            knockBackFeedBack.StartBackMove(transform, 0.1f);
        }

        public override void Dead()
        {
            poolObjectSpawner.SpawnPoolObjectWithAutoRelease(deadParticle, transform.position, 5f);
            AudioPlayer.Instance.PlayAudio(AudioName.EnemyDead3);
            base.Dead();
        }

        public override void TakeDamage(DamageInfo info)
        {
            base.TakeDamage(info);
            scaleShakeFeedBack.StartShake();
            flashFeedBack.Flash();
        }
    }
}