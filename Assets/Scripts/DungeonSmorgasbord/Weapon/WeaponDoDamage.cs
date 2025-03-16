using NaughtyAttributes.Scripts.Core.MetaAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.Manager;
using ZiercCode.Utilities;

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
        /// 伤害数字颜色
        /// </summary>
        [SerializeField] private Color damageTextColor;

        /// <summary>
        /// 只造成一次伤害
        /// </summary>
        [SerializeField] private bool doSingleDamage;

        /// <summary>
        ///是否同一目只造成一次伤害
        /// </summary>
        [SerializeField, HideIf("doSingleDamage")]
        private bool doSingleDamageToSameTarget;

        /// <summary>
        /// 是否能伤害到武器持有者
        /// </summary>
        [SerializeField] private bool canHurtSelf;

        /// <summary>
        /// 是否可以造成伤害
        /// </summary>
        private bool _canDoDamage;

        /// <summary>
        /// 已经造成过伤害的对象
        /// </summary>
        private List<Collider2D> _haveDamagedTarget;

        private void Awake()
        {
            _canDoDamage = true;
            _haveDamagedTarget = new List<Collider2D>();
        }

        /// <summary>
        /// 造成伤害
        /// </summary>
        /// <param name="c2d">目标碰撞体</param>
        public void DoDamage(Collider2D c2d)
        {
            if (!_canDoDamage) return;

            if (doSingleDamageToSameTarget)
                if (_haveDamagedTarget.Any(target => target == c2d))
                    return;

            if (c2d.TryGetComponent(out IDamageable damageable))
            {
                if (!canHurtSelf && c2d.transform == weapon.GetWeaponUserBase().GetWeaponUserTransform())
                    return;

                damageable.TakeDamage(GetDamageInfo());
                _haveDamagedTarget.Add(c2d);
                TextPopupSpawner.Instance.InitPopupText(c2d.transform.position, damageTextColor,
                    GetDamageInfo().damageAmount);

                if (doSingleDamage)
                    _canDoDamage = false;
            }
        }

        /// <summary>
        /// 获取最终伤害信息
        /// </summary>
        /// <returns></returns>
        private DamageInfo GetDamageInfo()
        {
            int damageAmount = (int)(weapon.GetWeaponDataSo().Damage *
                                     weapon.GetWeaponUserBase().GetWeaponDamageRate()[
                                         weapon.GetWeaponDataSo().WeaponType]);
            if (MyMath.ChanceToBool(weapon.GetWeaponUserBase().GetCriticalChance()))
                damageAmount *= 2;

            DamageInfo damageInfo = new(damageAmount, weapon.GetWeaponDataSo().DamageType,
                weapon.GetWeaponUserBase().GetWeaponUserTransform());
            return damageInfo;
        }

        private void OnDisable()
        {
            _haveDamagedTarget.Clear();
            _canDoDamage = true;
        }
    }
}