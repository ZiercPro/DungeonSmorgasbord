using ZiercCode._DungeonGame._Scripts.WeaponClass;

namespace ZiercCode._DungeonGame._Scripts.WeaponComponent
{
    /// <summary>
    /// 充能武器抖动效果组件
    /// todo
    /// </summary>
    public class WeaponChargeShakeComponent : BaseWeaponComponent
    {
        private ChargeWeapon _weapon;

        protected override void Awake()
        {
            base.Awake();

            _weapon = (ChargeWeapon)MyWeapon;
        }
        
        
    }
}