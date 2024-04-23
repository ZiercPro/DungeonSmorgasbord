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
    public class Enemy_Baby : Enemy
    {
        [SerializeField] private GameObject deadParticle;
        public BabyIdleState idleState { get; private set; }
        public BabyMoveState moveState { get; private set; }

        private CanDropItems _canDropItems;
        private KnockBackFeedBack _knockBackFeedBack;
        private ScaleShakeFeedBack _scaleShakeFeedBack;
        private FlashFeedBack _flashFeedBack;

        protected override void Awake()
        {
            base.Awake();
            idleState = new BabyIdleState(this, stateMachine, this);
            moveState = new BabyMoveState(this, stateMachine, this);
            _canDropItems = GetComponent<CanDropItems>();
            _knockBackFeedBack = GetComponent<KnockBackFeedBack>();
            _scaleShakeFeedBack = GetComponent<ScaleShakeFeedBack>();
            _flashFeedBack = GetComponent<FlashFeedBack>();
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(idleState);
            attackCheck.SetRadius(Attribute.attackRange);
        }

        public override void Dead(bool dropItem = true)
        {
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            AudioPlayer.Instance.PlayAudioAsync(AudioName.EnemyDead3);
            if (dropItem)
                _canDropItems.DropItems();
            Destroy(gameObject);
        }


        public override void TakeDamage(DamageInfo info)
        {
            health.SetCurrent(current =>
            {
                current -= info.damageAmount;
                return current;
            });
            _scaleShakeFeedBack.StartShake();
            _knockBackFeedBack.StartBackMove(info);
            _flashFeedBack.Flash();
            TextPopupSpawner.Instance.InitPopupText(this.transform.position, Color.blue, info.damageAmount);
        }
    }
}