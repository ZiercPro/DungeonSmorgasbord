using System.Collections.Generic;
using UnityEngine;

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
        /// 获取武器的父物体
        /// </summary>
        /// <returns></returns>
        public Transform GetWeaponParent();
    }
}