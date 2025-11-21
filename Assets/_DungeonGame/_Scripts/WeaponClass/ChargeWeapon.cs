using UnityEngine;
using UnityEngine.InputSystem;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 蓄力开火武器
    /// </summary>
    public abstract class ChargeWeapon : BaseWeapon
    {
        /// <summary>
        /// 按住攻击的时间
        /// </summary>
        public float pressTime;

        /// <summary>
        /// 是否按住攻击
        /// </summary>
        public bool isPressFireButton;

        protected override void OnEnable()
        {
            base.OnEnable();
            PlayerInputAction.HeroControl.MouseClickLeft.performed += HandleFireInput;
            PlayerInputAction.HeroControl.MouseClickLeft.canceled += HandleCancelFireInput;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PlayerInputAction.HeroControl.MouseClickLeft.performed -= HandleFireInput;
            PlayerInputAction.HeroControl.MouseClickLeft.canceled -= HandleCancelFireInput;
        }

        protected override void PauseUpdate()
        {
            base.PauseUpdate();
            ReadPressTime();
        }

        private void HandleCancelFireInput(InputAction.CallbackContext context)
        {
            Fire();
            isPressFireButton = false;
            pressTime = 0f;
        }

        private void HandleFireInput(InputAction.CallbackContext context)
        {
            isPressFireButton = true;
        }

        private void ReadPressTime()
        {
            if (isPressFireButton)
            {
                pressTime += Time.deltaTime;
            }
        }
    }
}