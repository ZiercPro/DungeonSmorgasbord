using UnityEngine;
using ZiercCode.Core.Utilities;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.Component.Enemy;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 追击的同时自动攻击
    /// </summary>
    public class EnemyChaseToTargetWithAttack : EnemyChaseStateBase
    {
        private EnemyAttribute _enemyAttribute;
        private Timer _attackTimer;
        private bool _canAttack;

        public EnemyChaseToTargetWithAttack(Enemy enemyBase, EnemyStateMachine stateMachine, Animator animator,
            AutoFlipComponent autoFlipComponent, MoveComponent moveComponent, EnemyAttackCheck enemyAttackCheck,
            EnemyAttribute enemyAttribute) : base(enemyBase, stateMachine, animator, autoFlipComponent, moveComponent,
            enemyAttackCheck)
        {
            _enemyAttribute = enemyAttribute;
            _attackTimer = new Timer(1 / _enemyAttribute.AttributesData.attackSpeed);
            _attackTimer.TimerTrigger += () => _canAttack = true;
            _canAttack = true;
        }

        public override void OnUpdate()
        {
            _attackTimer.Tick();

            if (EnemyBase.GetTarget() && EnemyBase.TargetIsAlive())
            {
                //如果目标存活 则追击
                AutoFlipComponent.FaceTo(EnemyBase.GetTarget().position);
                MoveDir = EnemyBase.GetTarget().transform.position - EnemyBase.transform.position;
                MoveComponent.Move(MoveDir.normalized);

                if (EnemyAttackCheck.isEnter && _canAttack)
                {
                    //如果目标进入攻击范围内，则直接攻击
                    _canAttack = false;
                    _attackTimer.StartTimer();
                    DamageInfo damageInfo = new DamageInfo(_enemyAttribute.AttributesData.damageAmount,
                        _enemyAttribute.AttributesData.damageType, EnemyBase.transform);
                    if (EnemyAttackCheck.collier.TryGetComponent(out IDamageable damageable))
                        damageable.TakeDamage(damageInfo);
                    EnemyBase.OnAttack(EnemyAttackCheck.collier.transform);
                }
            }
            else
            {
                //目标不存在则回到静息状态
                StateMachine.ChangeState(EnemyBase.GetEnemyIdleState());
            }
        }
    }
}