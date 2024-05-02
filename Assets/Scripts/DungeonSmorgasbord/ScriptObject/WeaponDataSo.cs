using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.DungeonSmorgasbord.Weapon;

namespace ZiercCode.DungeonSmorgasbord.ScriptObject
{
    /// <summary>
    /// 武器数据
    /// </summary>
    [CreateAssetMenu(fileName = "weaponDataSo", menuName = "ScriptObject/weaponDataSo")]
    public class WeaponDataSo : ScriptableObject
    {
        /// <summary>
        /// 武器名称
        /// </summary>
        public string myName;

        /// <summary>
        /// 伤害数值
        /// </summary>
        public int Damage;

        /// <summary>
        /// 伤害类型
        /// </summary>
        public DamageType DamageType;

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