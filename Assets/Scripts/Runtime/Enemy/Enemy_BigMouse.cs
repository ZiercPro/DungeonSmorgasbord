using UnityEngine;
using Runtime.FeedBack;
using UnityEngine.Serialization;

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
        AudioPlayerManager.Instance.PlayAudio(AudioName.BigMouthSpawn1);
    }

    public override void Dead(bool dropItem = true)
    {
        Instantiate(deadParticle, transform.position, Quaternion.identity);
        AudioPlayerManager.Instance.PlayAudio(AudioName.EnemyDead4);
        if (dropItem)
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
        TextPopupSpawner.Instance.InitPopupText(this.transform.position, Color.blue, info.damageAmount);
    }

}