using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Manager;
using ZiercCode.DungeonSmorgasbord.Component;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class Enemy_BigMouse : Enemy
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
            _enemyChaseStateBase = new EnemyChaseWithAttack(this, StateMachine, animator, autoFlipComponent,
                moveComponent, enemyAttackCheck);
        }

        public override void Init()
        {
            base.Init();
            StateMachine.SetStartState(_enemyIdleStateBase);
            enemyAttackCheck.SetRadius(attribute.AttributesData.attackRange);
        }

        public override void Attack(Transform target)
        {
            if (!CanAttack) return;
            base.Attack(target);
            //弹出文本
            TextPopupSpawner.Instance.InitPopupText(target, Color.red, -attribute.AttributesData.damageAmount);
            //击退
            KnockBackFeedBack knockBackFeedBack = GetComponent<KnockBackFeedBack>();
            knockBackFeedBack.StartBackMove(transform, 0.5f);
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

        public override void Dead()
        {
            poolObjectSpawner.SpawnPoolObjectWithAutoRelease(deadParticle, transform.position, 5f);
            AudioPlayer.Instance.PlayAudioAsync(AudioName.EnemyDead4);
            base.Dead();
        }
    }
}