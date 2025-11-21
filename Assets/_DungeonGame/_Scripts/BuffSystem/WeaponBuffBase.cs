using ZiercCode._DungeonGame._Scripts.WeaponClass;

namespace ZiercCode._DungeonGame._Scripts.BuffSystem
{
    public abstract class WeaponBuffBase : BuffBase
    {
        protected BaseWeapon MyWeapon => (BaseWeapon)Holder;
    }
}