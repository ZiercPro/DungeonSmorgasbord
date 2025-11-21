using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode._DungeonGame._Scripts.WeaponClass;
using ZiercCode.EventBusSystem;

namespace ZiercCode._DungeonGame._Scripts.EventClasses
{
    public class AttackEvent
    {
        // public abstract class BaseAttack : IEventArgs
        // {
        //     /// <summary>
        //     /// 本次攻击是否需要检查双方阵营
        //     /// 默认为true
        //     /// </summary>
        //     public bool CheckFaction = true;
        // }

        //武器攻击
        public class WeaponAttack : IEventArgs
        {
            /// <summary>
            /// 武器本体
            /// </summary>
            public BaseWeapon Weapon;

            /// <summary>
            /// 射弹
            /// </summary>
            public BaseProjectile Projectile;

            /// <summary>
            /// 攻击目标
            /// </summary>
            public IAttackAble Target;
        }
    }
}