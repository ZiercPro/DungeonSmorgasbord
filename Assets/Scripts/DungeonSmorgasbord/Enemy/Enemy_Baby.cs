using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Manager;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class Enemy_Baby : Enemy
    {
        [Header("粒子效果"), Space] [SerializeField]
        private PoolObjectSo deadParticle;

        private EnemyIdleStateBase _enemyIdleStateBase;
        private EnemyChaseStateBase _enemyChaseStateBase;

        protected override void Awake()
        {
            base.Awake();
            _enemyIdleStateBase =
                new EnemyIdleNoMove(this, StateMachine, animator, moveComponent);
            _enemyChaseStateBase = new EnemyChaseToTargetWithAttack(this, StateMachine, animator, autoFlipComponent,
                moveComponent, enemyAttackCheck, attribute);
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
            AudioPlayer.Instance.PlayAudioAsync(AudioName.EnemyDead3);
            base.Dead();
        }

        public override EnemyIdleStateBase GetEnemyIdleState()
        {
            return _enemyIdleStateBase;
        }

        public override EnemyChaseStateBase GetEnemyChaseState()
        {
            return _enemyChaseStateBase;
        }

        public override EnemyAttackStateBase GetEnemyAttackState()
        {
            Debug.LogError($"{name} 没有攻击状态");
            return null;
        }

        public override void TakeDamage(DamageInfo info)
        {
            base.TakeDamage(info);
            scaleShakeFeedBack.StartShake();
            flashFeedBack.Flash();
        }
    }
}