using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Component;
using ZiercCode.Old.Component.Enemy;
using ZiercCode.Old.Enemy.EnemyState;
using ZiercCode.Old.Manager;

namespace ZiercCode.Old.Enemy
{
    public class Enemy_BigMouse : Enemy
    {
        [SerializeField] private GameObject deadParticle;
        public BigMouthIdleState idleState { get; private set; }
        public BigMouthMoveState moveState { get; private set; }

        private CanDropItems _canDropItems;
        private ScaleShakeFeedBack _scaleShakeFeedBack;
        private FlashFeedBack _flashFeedBack;

        protected override void Awake()
        {
            base.Awake();
            idleState = new BigMouthIdleState(this, stateMachine, this);
            moveState = new BigMouthMoveState(this, stateMachine, this);
            _canDropItems = GetComponent<CanDropItems>();
            _scaleShakeFeedBack = GetComponent<ScaleShakeFeedBack>();
            _flashFeedBack = GetComponent<FlashFeedBack>();
        }

        public override void Init()
        {
            base.Init();
            stateMachine.Initialize(idleState);
            attackCheck.SetRadius(Attribute.attackRange);
        }

        public override void Dead()
        {
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            AudioPlayer.Instance.PlayAudioAsync(AudioName.EnemyDead4);
            _canDropItems.DropItems();
            base.Dead();
        }


        public override void TakeDamage(DamageInfo info)
        {
            int healthValue = health.GetCurrentHealth() - info.damageAmount;
            health.SetCurrentHealth(healthValue, Health.HealthChangeType.Damage);
            _scaleShakeFeedBack.StartShake();
            _flashFeedBack.Flash();
            TextPopupSpawner.Instance.InitPopupText(transform, Color.blue, info.damageAmount);
        }
    }
}