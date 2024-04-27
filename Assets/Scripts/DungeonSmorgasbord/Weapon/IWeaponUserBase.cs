using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public interface IWeaponUserBase
    {
        /// <summary>
        /// 获取使用不同武器对应伤害效率
        /// </summary>
        /// <returns></returns>
        public Dictionary<WeaponType, float> GetWeaponDamageRate();

        /// <summary>
        /// 获取暴击率
        /// </summary>
        /// <returns></returns>
        public float GetCriticalChance();

        /// <summary>
        /// 获取武器使用者
        /// </summary>
        /// <returns></returns>
        public Transform GetWeaponUserTransform();
    }
}