using System.Collections.Generic;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Weapon;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    /// <summary>
    /// 武器使用组件
    /// </summary>
    public class WeaponUserComponent : MonoBehaviour,IWeaponUserBase
    {
        [SerializeField] private WeaponRotateComponent weaponRotateComponent;

        public Dictionary<WeaponType, float> WeaponDamageRate { get; }

        public void GetDefaultWeapon()
        {
            //  if (defaultWeapon == null) return;
            // EquipWeapon(_currentWeaponInstance);
        }

        public GameObject EquipWeapon(GameObject weaponIns)
        {
            // GameObject lastWeapon = _currentWeaponInstance;
            // _currentWeaponInstance = weaponIns;
            // if (_currentWeaponInstance != lastWeapon)
            //     lastWeapon.GetComponent<Weapon.Weapon>().Disable();
            // SpriteRenderer weaponRenderer = weaponIns.GetComponentInChildren<SpriteRenderer>();
            // Weapon.Weapon weapon = weaponIns.GetComponentInChildren<Weapon.Weapon>();
            // weapon.SetMoveSpeed(_attribute.weaponDamageRate, _attribute.criticalChance);
            // // _weaponHolder.SetWeaponParent(weaponRenderer, weaponIns.transform);
            // return lastWeapon;
            return null;
        }

        public Transform GetWeaponParent()
        {
            throw new System.NotImplementedException();
        }
    }
}