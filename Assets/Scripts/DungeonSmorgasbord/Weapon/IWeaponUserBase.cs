using System.Collections.Generic;
using ZiercCode.DungeonSmorgasbord.Damage;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 武器使用者接口，可使用武器类需要实现该接口
    /// </summary>
    public interface IWeaponUserBase
    {
        /// <summary>
        /// 不同武器伤害效率
        /// </summary>
        public Dictionary<WeaponType, float> WeaponDamageRate { get; }

        /// <summary>
        /// 伤害信息
        /// </summary>
        public DamageInfo DamageInfo { get; }
    }
}