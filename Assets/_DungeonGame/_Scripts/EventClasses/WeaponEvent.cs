using ZiercCode._DungeonGame._Scripts.WeaponClass;
using ZiercCode.EventBusSystem;

namespace ZiercCode._DungeonGame._Scripts.EventClasses
{
    public class WeaponEvent
    {
        /// <summary>
        /// 武器开火逻辑结束后调用
        /// </summary>
        public class WeaponFired : IEventArgs
        {
            public BaseWeapon Weapon;
        }

        /// <summary>
        /// 换弹逻辑开始前调用
        /// </summary>
        public class WeaponStartReload : IEventArgs
        {
            public BaseWeapon Weapon;
        }

        /// <summary>
        /// 武器换弹逻辑结束后调用
        /// </summary>
        public class WeaponReloaded : IEventArgs
        {
            public BaseWeapon Weapon;
        }

        /// <summary>
        /// 武器数据发生变化
        /// 每次修改武器数据时需要主动广播该类型时间
        /// </summary>
        public class WeaponDataChanged : IEventArgs
        {
            public BaseWeapon Weapon;
        }
    }
}