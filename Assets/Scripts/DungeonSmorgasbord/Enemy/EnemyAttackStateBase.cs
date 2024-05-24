using UnityEngine;
using ZiercCode.Core.Utilities;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.Component.Enemy;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 攻击状态基类
    /// 处理状态转换 并体提供攻击所需数据
    /// </summary>
    public class EnemyAttackStateBase : EnemyState
    {
        private readonly int _attack = Animator.StringToHash("attack");


        protected EnemyAttackCheck EnemyAttackCheck;
        protected EnemyAttribute EnemyAttribute;
        protected DamageInfo DamageInfo;
        protected Timer AttackCoolDownTimer;
        protected bool CanAttack;


        public EnemyAttackStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, Animator animator,
            EnemyAttackCheck enemyAttackCheck, EnemyAttribute enemyAttribute) : base(
            enemyBase, stateMachine, animator)
        {
            EnemyAttackCheck = enemyAttackCheck;
            EnemyAttribute = enemyAttribute;
            AttackCoolDownTimer = new Timer(1 / EnemyAttribute.AttributesData.attackSpeed);
            AttackCoolDownTimer.TimerTrigger += () => CanAttack = true;
            DamageInfo = new DamageInfo(EnemyAttribute.AttributesData.damageAmount,
                EnemyAttribute.AttributesData.damageType, EnemyBase.transform);
            CanAttack = true;
        }

        public override void EntryState()
        {
            Animator.SetBool(_attack, true);
        }

        public override void FrameUpdate()
        {
            AttackCoolDownTimer.Tick();
            if (EnemyBase.GetTarget() && EnemyBase.TargetIsAlive() && !EnemyAttackCheck.isEnter)
            {
                //如果目标存活 但不在攻击范围内 则转换到追击状态
                StateMachine.ChangeState(EnemyBase.GetEnemyChaseState());
            }
            else
            {
                //如果目标不再存活 则转换到静息状态
                StateMachine.ChangeState(EnemyBase.GetEnemyIdleState());
            }
        }

        public override void ExitState()
        {
            Animator.SetBool(_attack, false);
            CanAttack = false;
        }
    }
}