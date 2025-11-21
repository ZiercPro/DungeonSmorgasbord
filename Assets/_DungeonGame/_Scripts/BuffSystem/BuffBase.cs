using UnityEngine;

namespace ZiercCode._DungeonGame._Scripts.BuffSystem
{
    /// <summary>
    /// buff基类
    /// </summary>
    public abstract class BuffBase
    {
        protected Object Holder;

        protected bool Enabled;

        protected float BuffTime;

        protected float BuffTimer;

        public virtual void BindHolder(Object holder)
        {
            Holder = holder;
        }

        /// <summary>
        /// 获取buff修改数值类型
        /// </summary>
        /// <returns></returns>
        public abstract BuffTypeEnum GetBuffType();

        /// <summary>
        /// 应用buff效果
        /// </summary>
        public abstract void ApplyBuff();

        /// <summary>
        /// 移除buff效果
        /// </summary>
        public abstract void RemoveBuff();

        /// <summary>
        /// 重复添加buff时调用
        /// </summary>
        public abstract void ReAddBuff();

        /// <summary>
        /// 更新buff
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// 重置buff时间
        /// </summary>
        public void ResetTimer()
        {
            BuffTimer = BuffTime;
        }

        /// <summary>
        /// 是否正在应用buff效果
        /// </summary>
        /// <returns></returns>
        public bool IsActive()
        {
            return Enabled;
        }
    }
}