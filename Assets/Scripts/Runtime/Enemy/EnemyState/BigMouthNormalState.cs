using UnityEngine;
using ZiercCode.Runtime.Damage;

namespace ZiercCode.Runtime.Enemy.EnemyState
{
    public abstract class BigMouthNormalState : EnemyState
    {
        protected Enemy_BigMouse _enemy;
        private float _attackInterval;
        private float _timer;

        public BigMouthNormalState(Enemy enemyBase, EnemyStateMachine stateMachine, Enemy_BigMouse enemy) : base(
            enemyBase,
            stateMachine)
        {
            _timer = 0f;
            _enemy = enemy;
            _attackInterval = 1.0f / _enemy.attribute.attackSpeed;
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
            if (_enemy.attackTarget && !_enemy.attackTarget.isDead)
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
}