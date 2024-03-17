using UnityEngine;
using Runtime.FeedBack;

public class Enemy_Baby : Enemy
{
    [SerializeField] private GameObject deadParticle;
    public BabyIdleState idleState { get; private set; }
    public BabyMoveState moveState { get; private set; }

    private KnockBackFeedBack _knockBackFeedBack;
    private FlashWhiteFeedBack _flashWhiteFeedBack;

    protected override void Awake()
    {
        base.Awake();
        idleState = new BabyIdleState(this, stateMachine, this);
        moveState = new BabyMoveState(this, stateMachine, this);
        _knockBackFeedBack = GetComponent<KnockBackFeedBack>();
        _flashWhiteFeedBack = GetComponent<FlashWhiteFeedBack>();
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        attackCheck.SetRadius(attribute.attackRange);
        AudioPlayerManager.Instance.PlayAudio(Audios.babySpawn_1);
    }

    public override void Dead()
    {
        Instantiate(deadParticle, transform.position, Quaternion.identity);
        AudioPlayerManager.Instance.PlayAudio(Audios.enemyDead_3);
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
        TextPopupSpawner.Instance.InitPopupText(this.transform, Color.blue, info.damageAmount);
    }
}