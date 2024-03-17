using UnityEngine;

public class Enemy_BigMouse : Enemy
{
    [SerializeField] private GameObject deadParticle;
    public BigMouthIdleState idleState { get; private set; }
    public BigMouthMoveState moveState { get; private set; }

    private CanDropItems _canDropItems;
    private KnockBackFeedBack _knockBackFeedBack;
    private FlashWhiteFeedBack _flashWhiteFeedBack;

    protected override void Awake()
    {
        base.Awake();
        idleState = new BigMouthIdleState(this, stateMachine, this);
        moveState = new BigMouthMoveState(this, stateMachine, this);
        _canDropItems = GetComponent<CanDropItems>();
        _knockBackFeedBack = GetComponent<KnockBackFeedBack>();
        _flashWhiteFeedBack = GetComponent<FlashWhiteFeedBack>();
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        attackCheck.SetRadius(attribute.attackRange);
        AudioPlayerManager.Instance.PlayAudio(Audios.bigMouthSpawn_1);
    }

    public override void Dead()
    {
        Instantiate(deadParticle, transform.position, Quaternion.identity);
        AudioPlayerManager.Instance.PlayAudio(Audios.enemyDead_4);
        _canDropItems.DropItems();
        Destroy(gameObject);
    }

    public override void TakeDamage(DamageInfo info)
    {
        base.TakeDamage(info);
        _knockBackFeedBack.StartBackMove(info);
        _flashWhiteFeedBack.Flash();
        health.SetCurrent(current =>
        {
            current -= info.damageAmount;
            return current;
        });
        DamagePopupSpawner.Instance.InitDamagePopup(this.transform, Color.blue, info.damageAmount);
    }
}