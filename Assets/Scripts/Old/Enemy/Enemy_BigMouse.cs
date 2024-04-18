using UnityEngine;
using ZiercCode.Runtime.Audio;
using ZiercCode.Runtime.Component;
using ZiercCode.Runtime.Component.Enemy;
using ZiercCode.Runtime.Damage;
using ZiercCode.Runtime.Enemy.EnemyState;
using ZiercCode.Runtime.Manager;

namespace ZiercCode.Runtime.Enemy
{
    public class Enemy_BigMouse : Enemy
    {
        [SerializeField] private GameObject deadParticle;
        public BigMouthIdleState idleState { get; private set; }
        public BigMouthMoveState moveState { get; private set; }

        private CanDropItems _canDropItems;
        private KnockBackFeedBack _knockBackFeedBack;
        private ScaleShakeFeedBack _scaleShakeFeedBack;
        private FlashWhiteFeedBack _flashWhiteFeedBack;

        protected override void Awake()
        {
            base.Awake();
            idleState = new BigMouthIdleState(this, stateMachine, this);
            moveState = new BigMouthMoveState(this, stateMachine, this);
            _canDropItems = GetComponent<CanDropItems>();
            _knockBackFeedBack = GetComponent<KnockBackFeedBack>();
            _scaleShakeFeedBack = GetComponent<ScaleShakeFeedBack>();
            _flashWhiteFeedBack = GetComponent<FlashWhiteFeedBack>();
        }

        protected override void Start()
        {
            base.Start();
            stateMachine.Initialize(idleState);
            attackCheck.SetRadius(attribute.attackRange);
            AudioPlayer.Instance.PlayAudioAsync(AudioName.BigMouthSpawn1);
        }

        public override void Dead(bool dropItem = true)
        {
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            AudioPlayer.Instance.PlayAudioAsync(AudioName.EnemyDead4);
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
            _scaleShakeFeedBack.StartShake();
            _knockBackFeedBack.StartBackMove(info);
            _flashWhiteFeedBack.Flash();
            TextPopupSpawner.Instance.InitPopupText(this.transform.position, Color.blue, info.damageAmount);
        }
    }
}