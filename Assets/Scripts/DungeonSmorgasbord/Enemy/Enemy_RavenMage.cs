using System.Collections.Generic;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Weapon;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class Enemy_RavenMage : Enemy, IWeaponUserBase
    {
        [SerializeField] private WeaponUserHelper weaponUserHelper;

        private EnemyIdleStateBase _enemyIdleState;
        private EnemyChaseStateBase _enemyChaseState;
        private EnemyAttackStateBase _enemyAttackState;

        protected override void Awake()
        {
            base.Awake();
            _enemyIdleState = new EnemyIdleNoMove(this, StateMachine, animator, moveComponent);
            _enemyChaseState =
                new EnemyChaseToTargetWithWeapon(this, StateMachine, animator, autoFlipComponent, moveComponent,
                    enemyAttackCheck, weaponUserHelper);
            _enemyAttackState = new EnemyAttackNoMoveWithWeapon(this, StateMachine, animator, enemyAttackCheck,
                attribute,
                weaponUserHelper, autoFlipComponent);
        }

        public override void Init()
        {
            weaponUserHelper.SetWeapon();
            base.Init();
        }

        public override void OnAttack(Transform target)
        {
        }

        public override EnemyIdleStateBase GetEnemyIdleState()
        {
            return _enemyIdleState;
        }

        public override EnemyChaseStateBase GetEnemyChaseState()
        {
            return _enemyChaseState;
        }

        public override EnemyAttackStateBase GetEnemyAttackState()
        {
            return _enemyAttackState;
        }

        public Dictionary<WeaponType, float> GetWeaponDamageRate()
        {
            return attribute.AttributesData.weaponDamageRate.ToDictionary();
        }

        public float GetCriticalChance()
        {
            return attribute.AttributesData.criticalChance;
        }

        public Transform GetWeaponUserTransform()
        {
            return transform;
        }
    }
}