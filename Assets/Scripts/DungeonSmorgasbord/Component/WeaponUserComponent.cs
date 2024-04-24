using System;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Weapon;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class WeaponUserComponent : MonoBehaviour
    {
        private WeaponRotateComponent _weaponRotateComponent;
        private IWeaponUserBase _weaponUserBase;
        private IWeaponBase _weaponBase;

        private void Awake()
        {
            _weaponRotateComponent = GetComponent<WeaponRotateComponent>();
            _weaponUserBase = GetComponent<IWeaponUserBase>();
        }

        public Action<Vector2> SetWeapon(SpriteRenderer weaponRenderer, Transform weaponTransform, WeaponBase weapon)
        {
            _weaponRotateComponent.SetWeapon(weaponTransform, weaponRenderer);
            _weaponBase = weapon;
            weapon.Init(_weaponUserBase);
            return viewPosition => _weaponRotateComponent.WeaponRotateTo(viewPosition);
        }

        public void OnLeftButtonPressStarted()
        {
            _weaponBase.OnLeftButtonPressStarted();
        }

        public void OnLeftButtonPressing()
        {
            _weaponBase.OnLeftButtonPressing();
        }

        public void OnLeftButtonPressCanceled()
        {
            _weaponBase.OnLeftButtonPressCanceled();
        }

        public void OnRightButtonPressStarted()
        {
            _weaponBase.OnRightButtonPressStarted();
        }

        public void OnRightButtonPressing()
        {
            _weaponBase.OnRightButtonPressing();
        }

        public void OnRightButtonPressCanceled()
        {
            _weaponBase.OnRightButtonPressCanceled();
        }
    }
}