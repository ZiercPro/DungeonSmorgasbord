using Runtime.Weapon.Base;
using UnityEngine;

namespace Runtime.Component.Hero
{
    using Player;
    using Weapon.Base;

    public class HeroWeaponHandler : MonoBehaviour
    {
        [SerializeField] private GameObject defaultWeapon;

        private GameObject _currentWeaponInstance;
        private WeaponHolder _weaponHolder;
        private InputManager _inputManager;
        private HeroAttribute _attribute;

        private void Awake()
        {
            _weaponHolder = GetComponentInChildren<WeaponHolder>();
            _inputManager = GetComponentInChildren<InputManager>();
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
                lastWeapon.GetComponent<Weapon>().Disable();
            SpriteRenderer weaponRenderer = weaponIns.GetComponentInChildren<SpriteRenderer>();
            Weapon weapon = weaponIns.GetComponentInChildren<Weapon>();
            weapon.Initialize(_attribute.weaponDamageRate, _attribute.criticalChance, _inputManager);
            _weaponHolder.ChangeWeapon(weaponRenderer, weaponIns.transform);
            return lastWeapon;
        }
    }
}