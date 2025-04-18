using UnityEngine;
using UnityEngine.InputSystem;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 单发、半自动武器（都需要一直点按
    /// </summary>
    public abstract class SingleAndSemiAutoWeapon : BaseWeapon
    {
        /// <summary>
        /// 开火计时器 确认当前开火冷却
        /// </summary>
        protected float FireTimer;

        protected override void OnEnable()
        {
            base.OnEnable();
            PlayerInputAction.HeroControl.MouseClickLeft.performed += HandleFireInput; //不实用started事件是为了在游戏结束暂停不要立即接收输入
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PlayerInputAction.HeroControl.MouseClickLeft.performed -= HandleFireInput;
        }

        protected override void PauseUpdate()
        {
            base.PauseUpdate();
            if (FireTimer > 0)
            {
                FireTimer -= Time.deltaTime;
            }
        }

        private void HandleFireInput(InputAction.CallbackContext context)
        {
            if (FireTimer <= 0f)
            {
                Fire();
                FireTimer = 1f / fireSpeed;
            }
        }
    }
}