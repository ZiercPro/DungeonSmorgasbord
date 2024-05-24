using NaughtyAttributes.Scripts.Core.DrawerAttributes_SpecialCase;
using System;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public class WeaponUserHelper : MonoBehaviour
    {
        [SerializeField] private WeaponDataSo weaponDataSo;
        [SerializeField] private WeaponRotate weaponRotate;
        private IWeaponUserBase _weaponUserBase;
        private IWeaponBase _weaponBase;

        private GameObject _currentWeaponInstance;

        private void Awake()
        {
            weaponRotate = GetComponent<WeaponRotate>();
            _weaponUserBase = GetComponent<IWeaponUserBase>();
        }

        /// <summary>
        /// 设置武器
        /// </summary>
        /// <returns></returns>
#if UNITY_EDITOR
        [Button("设置武器")]
#endif
        public void SetWeapon()
        {
            if (!weaponDataSo.useAble)
            {
                Debug.LogWarning($"weapon {weaponDataSo.myName} cant be equipped");
                return;
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
            if (weaponRotate)
                weaponRotate.SetWeapon(weaponTransform, weaponRenderer);
        }

        /// <summary>
        /// 设置武器旋转
        /// </summary>
        /// <returns>返回武器的旋转方法</returns>
        public Action<Vector2> SetWeaponRotate()
        {
            if (weaponRotate)
                return viewPosition => weaponRotate.WeaponRotateTo(viewPosition);
            else
            {
                Debug.LogWarning($"武器{weaponDataSo.name} 没有旋转");
                return default;
            }
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