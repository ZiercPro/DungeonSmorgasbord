using System;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Weapon;
using ZiercCode.Old.Component.Enemy;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    /// <summary>
    /// 敌人正常追击
    /// </summary>
    public class EnemyChaseToTargetWithWeapon : EnemyChaseStateBase
    {
        private Vector2 _moveDir;

        private Action<Vector2> _weaponUserHelper;

        public EnemyChaseToTargetWithWeapon(Enemy enemyBase, EnemyStateMachine stateMachine, Animator animator,
            AutoFlipComponent autoFlipComponent, MoveComponent moveComponent, EnemyAttackCheck enemyAttackCheck,
            WeaponUserHelper weaponUserHelper) : base(enemyBase, stateMachine, animator, autoFlipComponent,
            moveComponent, enemyAttackCheck)
        {
            _weaponUserHelper = weaponUserHelper.SetWeaponRotate();
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            _weaponUserHelper(EnemyBase.GetTarget().position);
        }
    }
}