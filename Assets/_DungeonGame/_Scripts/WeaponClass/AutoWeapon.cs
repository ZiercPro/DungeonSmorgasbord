using UnityEngine;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 全自动武器
    /// </summary>
    public abstract class AutoWeapon : BaseWeapon
    {
        protected float FireTimer;

        protected override void PauseUpdate()
        {
            base.PauseUpdate();

            HandleFireInput();
        }

        private void HandleFireInput()
        {
            if (FireTimer > 0f)
            {
                FireTimer -= Time.deltaTime;
            }
            else
            {
                if (PlayerInputAction.HeroControl.MouseClickLeft.ReadValue<float>() > 0f)
                {
                    Fire();
                    FireTimer = 1f / shootSpeed;
                }
            }
        }
    }
}