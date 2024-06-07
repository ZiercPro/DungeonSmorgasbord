using System.Collections.Generic;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Weapon;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class RavenMageEnemy : Enemy, IWeaponUserBase
    {
        [SerializeField] private WeaponUserHelper weaponUserHelper;

        protected override void Start()
        {
            base.Start();
            
            //todo 重新设计敌人状态因为现在不能通过构造注入注入依赖
            
            StateMachine.Run<RavenMageEnemyState.Idle>();
        }

        public override void Init()
        {
            weaponUserHelper.SetWeapon();
            base.Init();
        }

        public override void OnAttack(Transform target)
        {
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