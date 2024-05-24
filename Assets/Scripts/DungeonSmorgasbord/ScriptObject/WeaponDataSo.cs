using NaughtyAttributes.Scripts.Core.MetaAttributes;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.DungeonSmorgasbord.Weapon;

namespace ZiercCode.DungeonSmorgasbord.ScriptObject
{
    /// <summary>
    /// 武器数据
    /// </summary>
    [CreateAssetMenu(fileName = "weaponDataSo", menuName = "ScriptableObject/weaponDataSo")]
    public class WeaponDataSo : ScriptableObject
    {
        /// <summary>
        /// 武器名称
        /// </summary>
        public string myName;

        /// <summary>
        /// 可以造成伤害
        /// </summary>
        public bool canDoDamage;

        /// <summary>
        /// 伤害数值
        /// </summary>
        [ShowIf("canDoDamage")] public int Damage;

        /// <summary>
        /// 伤害类型
        /// </summary>
        [ShowIf("canDoDamage")] public DamageType DamageType;

        /// <summary>
        /// 武器类型
        /// </summary>
        public WeaponType WeaponType;

        /// <summary>
        /// 武器预制件
        /// </summary>
        public GameObject prefab;

        /// <summary>
        /// 是否可以使用
        /// </summary>
        public bool useAble;
    }
}