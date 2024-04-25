using System;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Weapon;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class WeaponUserComponent : MonoBehaviour
    {
        [SerializeField] private WeaponDataSo weaponDataSo;
        private WeaponRotateComponent _weaponRotateComponent;
        private IWeaponUserBase _weaponUserBase;
        private IWeaponBase _weaponBase;

        private void Awake()
        {
            _weaponRotateComponent = GetComponent<WeaponRotateComponent>();
            _weaponUserBase = GetComponent<IWeaponUserBase>();
        }

        /// <summary>
        /// 设置武器
        /// </summary>
        /// <returns>鼠标位置改变事件绑定</returns>
        public Action<Vector2> SetWeapon()
        {
            GameObject weaponInstance = Instantiate(weaponDataSo.prefab);
            Transform weaponTransform = weaponInstance.transform;
            SpriteRenderer weaponRenderer = weaponInstance.GetComponentInChildren<SpriteRenderer>();
            WeaponBase weaponBase = weaponInstance.GetComponent<WeaponBase>();
            _weaponBase = weaponBase;
            weaponBase.Init(_weaponUserBase);
            if (!_weaponRotateComponent) return null;
            _weaponRotateComponent.SetWeapon(weaponTransform, weaponRenderer);
            return viewPosition => _weaponRotateComponent.WeaponRotateTo(viewPosition);
        }

        public void OnLeftButtonPressStarted()
        {
            _weaponBase.OnLeftButtonPressStarted();
        }

        public void OnLeftButtonPressed()
        {
            _weaponBase.OnLeftButtonPressed();
        }

        public void OnLeftButtonPressCanceled()
        {
            _weaponBase.OnLeftButtonPressCanceled();
        }

        public void OnRightButtonPressStarted()
        {
            _weaponBase.OnRightButtonPressStarted();
        }

        public void OnRightButtonPressed()
        {
            _weaponBase.OnRightButtonPressed();
        }

        public void OnRightButtonPressCanceled()
        {
            _weaponBase.OnRightButtonPressCanceled();
        }
    }
}