using NaughtyAttributes.Scripts.Core.MetaAttributes;
using UnityEngine;
using ZiercCode.Core.Utilities;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.DungeonSmorgasbord.Weapon;

namespace ZiercCode.DungeonSmorgasbord.ScriptObject
{
    [CreateAssetMenu(menuName = "ScriptableObject/Attributes/Enemy", fileName = "Enemy")]
    public class EnemyAttributeSo : CreatureAttributesSo
    {
        /// <summary>
        /// 难度
        /// </summary>
        public int difficulty;

        /// <summary>
        /// 是否使用武器
        /// </summary>
        public bool useWeapon;

        /// <summary>
        /// 伤害
        /// </summary>
        [HideIf("useWeapon")] public int damageAmount;

        /// <summary>
        /// 伤害类型
        /// </summary>
        [HideIf("useWeapon")] public DamageType damageType;

        /// <summary>
        /// 武器伤害率字典
        /// </summary>
        [ShowIf("useWeapon")] public EditableDictionary<WeaponType, float> weaponDamageRate;

        /// <summary>
        /// 攻击速度
        /// </summary>
        public float attackSpeed;

        /// <summary>
        /// 攻击距离
        /// </summary>
        public float attackRange;
    }
}