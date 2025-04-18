using UnityEngine;
using ZiercCode.Management;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 射弹基类
    /// </summary>
    public abstract class BaseProjectile : PauseBehaviour
    {
        /// <summary>
        /// 属于武器实例
        /// </summary>
        [HideInInspector]
        public BaseWeapon myWeapon;

        // //存活计时器
        // protected float StayTimer;

        public virtual void Init(BaseWeapon myWeapon)
        {
            this.myWeapon = myWeapon;
            // StayTimer = myWeapon.projectileStayTime;
            SyncCollision();
        }

        // //存活一定时间后自动释放
        // protected virtual void TimeRelease()
        // {
        //     if (StayTimer > 0f)
        //     {
        //         StayTimer -= Time.deltaTime;
        //     }
        //     else
        //     {
        //         PoolManager.Instance.Release(myWeapon.projectilePoolName, gameObject);
        //     }
        // }

        /// <summary>
        /// 同步射弹的碰撞体大小
        /// </summary>
        protected abstract void SyncCollision();
    }
}