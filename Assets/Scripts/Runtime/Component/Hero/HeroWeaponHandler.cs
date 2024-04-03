using UnityEngine;
using ZiercCode.Runtime.Hero;
using ZiercCode.Runtime.Player;
using ZiercCode.Runtime.Weapon;

namespace ZiercCode.Runtime.Component.Hero
{
    public class HeroWeaponHandler : MonoBehaviour
    {
        [SerializeField] private GameObject defaultWeapon;

        private GameObject _currentWeaponInstance;
        private WeaponHolder _weaponHolder;
        private HeroInputManager _heroInputManager;
        private HeroAttribute _attribute;

        private void Awake()
        {
            _weaponHolder = GetComponentInChildren<WeaponHolder>();
            _heroInputManager = GetComponentInChildren<HeroInputManager>();
            _attribute = GetComponentInChildren<HeroAttribute>();
            _currentWeaponInstance = Instantiate(defaultWeapon);
        }

        public void GetDefualtWeapon()
        {
            if (defaultWeapon == null) return;
            EquipWeapon(_currentWeaponInstance);
        }

        public GameObject EquipWeapon(GameObject weaponIns)
        {
            GameObject lastWeapon = _currentWeaponInstance;
            _currentWeaponInstance = weaponIns;
            if (_currentWeaponInstance != lastWeapon)
                lastWeapon.GetComponent<Weapon.Weapon>().Disable();
            SpriteRenderer weaponRenderer = weaponIns.GetComponentInChildren<SpriteRenderer>();
            Weapon.Weapon weapon = weaponIns.GetComponentInChildren<Weapon.Weapon>();
            weapon.Initialize(_attribute.WeaponDamageRate, _attribute.criticalChance, _heroInputManager);
            _weaponHolder.ChangeWeapon(weaponRenderer, weaponIns.transform);
            return lastWeapon;
        }
    }
}