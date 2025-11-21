using ZiercCode._DungeonGame._Scripts.WeaponClass;
using ZiercCode.EventBusSystem;

namespace ZiercCode._DungeonGame._Scripts.CrystalClasses
{
    /// <summary>
    /// 晶体基类
    /// 维护若干buff
    /// </summary>
    public abstract class CrystalBase
    {
        /// <summary>
        /// 晶石作用武器
        /// </summary>
        protected BaseWeapon MyWeapon;

        /// <summary>
        /// 事件集
        /// </summary>
        protected EventsGroup EventsGroup;

        /// <summary>
        /// 晶石是否启用
        /// </summary>
        protected bool Enabled;

        public CrystalBase()
        {
            EventsGroup = new EventsGroup();
        }

        /// <summary>
        /// 将晶石绑定到实体上
        /// </summary>
        /// <param name="target"></param>
        public virtual void AttachToWeapon(BaseWeapon target)
        {
            MyWeapon = target;
            Enabled = true;
        }

        public abstract void Remove();

        public abstract void Update();

        public bool IsActive()
        {
            return Enabled;
        }
    }
}