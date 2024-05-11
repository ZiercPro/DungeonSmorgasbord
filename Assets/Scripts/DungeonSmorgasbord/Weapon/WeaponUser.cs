using NaughtyAttributes;
using NaughtyAttributes.Scripts.Core.DrawerAttributes_SpecialCase;
using System;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public class WeaponUser : MonoBehaviour
    {
        [SerializeField] private WeaponDataSo weaponDataSo;
        private WeaponRotate _weaponRotate;
        private IWeaponUserBase _weaponUserBase;
        private IWeaponBase _weaponBase;

        private GameObject _currentWeaponInstance;

        private void Awake()
        {
            _weaponRotate = GetComponent<WeaponRotate>();
            _weaponUserBase = GetComponent<IWeaponUserBase>();
        }

        /// <summary>
        /// 设置武器
        /// </summary>
        /// <returns>鼠标位置改变事件绑定武器旋转方法</returns>
#if UNITY_EDITOR
        [Button("设置武器")]
#endif
        public Action<Vector2> SetWeapon()
        {
            if (!weaponDataSo.useAble)
            {
                Debug.LogWarning($"weapon {weaponDataSo.myName} cant be equipped");
                return default;
            }

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
            if (!_weaponRotate) return null;
            _weaponRotate.SetWeapon(weaponTransform, weaponRenderer);
            return viewPosition => _weaponRotate.WeaponRotateTo(viewPosition);
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