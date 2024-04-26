using System;
using UnityEngine;
using ZiercCode.Core.Extend;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 武器伤害组件，用于造成伤害
    /// </summary>
    public class WeaponDoDamage : MonoBehaviour
    {
        /// <summary>
        /// 武器数据
        /// </summary>
        [SerializeField] private WeaponBase weapon;

        /// <summary>
        /// 是否能伤害到武器持有者
        /// </summary>
        [SerializeField] private bool canHurtSelf;

        /// <summary>
        /// 武器类
        /// </summary>
        private WeaponBase _weaponBase;

        private void Awake()
        {
            _weaponBase = GetComponent<WeaponBase>();
        }

        public void DoDamage(Collider2D c2d)
        {
            if (c2d.TryGetComponent(out IDamageable damageable))
            {
                if (!canHurtSelf && c2d.transform == _weaponBase.transform)
                    return;

                damageable.TakeDamage(GetDamageInfo());
            }
        }

        /// <summary>
        /// 获取最终伤害信息
        /// </summary>
        /// <returns></returns>
        private DamageInfo GetDamageInfo()
        {
            int damageAmount = weapon.GetWeaponDataSo().Damage;
            if (MyMath.ChanceToBool(_weaponBase.GetWeaponUser().GetCriticalChance()))
                damageAmount *= 2;

            DamageInfo damageInfo = new(damageAmount, weapon.GetWeaponDataSo().DamageType,
                _weaponBase.GetWeaponUser().GetWeaponUser());
            return damageInfo;
        }
    }
}