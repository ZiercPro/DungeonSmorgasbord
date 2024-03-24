using UnityEngine;

public class BabyNormalState : EnemyState
{
    protected Enemy_Baby _enemy;
    private float _attackInterval;
    private float _timer;

    public BabyNormalState(Enemy enemyBase, EnemyStateMachine stateMachine, Enemy_Baby enemy) : base(enemyBase,
        stateMachine)
    {
        _timer = 0f;
        _enemy = enemy;
        _attackInterval = 1 / _enemy.attribute.attackSpeed;
    }

    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    public override void EntryState()
    {
        base.EntryState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        if (_enemy.attackTarget && _enemy.attackTarget.activeInHierarchy)
        {
            if (enemyBase.attackCheck.isEnter)
            {
                if (_timer <= 0)
                {
                    DamageInfo info = new DamageInfo(_enemy.attribute.damageAmount, _enemy.attribute.damageType,
                        _enemy.transform);
                    IDamageable damageable =
                        _enemy.attackCheck.collier.GetComponent(typeof(IDamageable)) as IDamageable;
                    damageable.TakeDamage(info);
                    _timer = _attackInterval;
                }
                else
                {
                    _timer -= Time.deltaTime;
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}