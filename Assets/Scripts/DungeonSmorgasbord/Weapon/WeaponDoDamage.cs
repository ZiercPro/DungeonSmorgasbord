using UnityEngine;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public class WeaponDoDamage : MonoBehaviour
    {
        /// <summary>
        /// 武器数据
        /// </summary>
        [SerializeField] private WeaponDataSo _weaponDataSo;
        
        // /// <summary>
        // /// 获取最终伤害信息
        // /// </summary>
        // /// <returns></returns>
        // public DamageInfo GetDamageInfo()
        // {
        //     DamageInfo damageInfo;
        //     int damageAmount = weaponDataSo.Damage;
        //     if (MyMath.ChanceToBool(_weaponUserBase.GetCriticalChance()))
        //         damageAmount *= 2;
        //
        //     damageInfo = new(damageAmount, weaponDataSo.DamageType,
        //         _weaponUserBase.GetWeaponUser());
        //     return damageInfo;
        // }

    }
}