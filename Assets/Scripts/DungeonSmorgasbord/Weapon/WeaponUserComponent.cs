using NaughtyAttributes;
using System;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public class WeaponUserComponent : MonoBehaviour
    {
        [SerializeField] private WeaponDataSo weaponDataSo;
        private WeaponRotateComponent _weaponRotateComponent;
        private IWeaponUserBase _weaponUserBase;
        private IWeaponBase _weaponBase;

        private GameObject _currentWeaponInstance;

        private void Awake()
        {
            _weaponRotateComponent = GetComponent<WeaponRotateComponent>();
            _weaponUserBase = GetComponent<IWeaponUserBase>();
        }

        /// <summary>
        /// 设置武器
        /// </summary>
        /// <returns>鼠标位置改变事件绑定</returns>
#if UNITY_EDITOR
        [Button("设置武器")]
#endif
        public Action<Vector2> SetWeapon()
        {
            if (_currentWeaponInstance)
            {
                //去除当前武器
                Destroy(_currentWeaponInstance);
                _currentWeaponInstance = null;
            }

            GameObject weaponInstance = Instantiate(weaponDataSo.prefab);
            _currentWeaponInstance = weaponInstance;
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

        public void OnViewPositionChange(Vector2 viewPosition)
        {
            _weaponBase.OnViewPositionChange(viewPosition);
        }
    }
}