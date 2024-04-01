using UnityEngine;


namespace ZRuntime
{

    public class Enemy_Baby : Enemy
    {
        [SerializeField] private GameObject deadParticle;
        public BabyIdleState idleState { get; private set; }
        public BabyMoveState moveState { get; private set; }

        private CanDropItems _canDropItems;
        private KnockBackFeedBack _knockBackFeedBack;
        private FlashWhiteFeedBack _flashWhiteFeedBack;

        protected override void Awake()
        {
            base.Awake();
            idleState = new BabyIdleState(this, stateMachine, this);
            moveState = new BabyMoveState(this, stateMachine, this);
            _canDropItems = GetComponent<CanDropItems>();
            _knockBackFeedBack = GetComponent<KnockBackFeedBack>();
            _flashWhiteFeedBack = GetComponent<FlashWhiteFeedBack>();
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(idleState);
            attackCheck.SetRadius(attribute.attackRange);
            AudioPlayer.Instance.PlayAudioAsync(AudioName.CoinCollected);
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
            base.TakeDamage(info);
            health.SetCurrent(current =>
            {
                current -= info.damageAmount;
                return current;
            });
            _knockBackFeedBack.StartBackMove(info);
            _flashWhiteFeedBack.Flash();
            TextPopupSpawner.Instance.InitPopupText(this.transform.position, Color.blue, info.damageAmount);
        }
    }
}