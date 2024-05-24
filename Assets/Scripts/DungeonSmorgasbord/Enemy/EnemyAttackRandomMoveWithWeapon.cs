using System;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Weapon;
using ZiercCode.Old.Component.Enemy;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class EnemyAttackRandomMoveWithWeapon : EnemyAttackStateBase
    {
        private AutoFlipComponent _autoFlipComponent;
        private Action<Vector2> _weaponRotateAction;
        private WeaponUserHelper _weaponUserHelper;
        private MoveComponent _moveComponent;

        public EnemyAttackRandomMoveWithWeapon(Enemy enemyBase, EnemyStateMachine stateMachine, Animator animator,
            EnemyAttackCheck enemyAttackCheck, EnemyAttribute enemyAttribute, WeaponUserHelper weaponUserHelper,
            AutoFlipComponent autoFlipComponent, MoveComponent moveComponent
        ) : base(
            enemyBase, stateMachine, animator, enemyAttackCheck, enemyAttribute)
        {
            _moveComponent = moveComponent;
            _weaponUserHelper = weaponUserHelper;
            _autoFlipComponent = autoFlipComponent;
            _weaponRotateAction = _weaponUserHelper.SetWeaponRotate();
        }


        public override void FrameUpdate()
        {
            AttackCoolDownTimer.Tick();

            if (EnemyBase.GetTarget() && EnemyBase.TargetIsAlive() && EnemyAttackCheck.isEnter)
            {
                //瞄准敌人
                _autoFlipComponent.FaceTo(EnemyBase.GetTarget().transform.position);
                _weaponRotateAction(EnemyBase.GetTarget().transform.position);
                //如果在范围内 则判断是否可以攻击
                if (CanAttack)
                {
                    //可以攻击 则直接攻击
                    CanAttack = false;
                    _weaponUserHelper.OnLeftButtonPressStarted();
                    AttackCoolDownTimer.StartTimer();
                }
            }
            else if (EnemyBase.GetTarget() && EnemyBase.TargetIsAlive())
            {
                //如果不在范围内则追击
                StateMachine.ChangeState(EnemyBase.GetEnemyChaseState());
            }
            else
            {
                //如果目标不存活 则回到静息状态
                StateMachine.ChangeState(EnemyBase.GetEnemyIdleState());
            }
        }
    }
}