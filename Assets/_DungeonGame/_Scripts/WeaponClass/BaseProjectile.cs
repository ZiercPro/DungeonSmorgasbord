using UnityEngine;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.EventBusSystem;
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

        /// <summary>
        /// 射弹本体
        /// </summary>
        [SerializeField]
        protected Transform myTransform;

        /// <summary>
        /// 初始大小
        /// </summary>
        protected Vector3 StartSize;

        protected virtual void Awake()
        {
            StartSize = myTransform.localScale;
        }

        public virtual void Init(BaseWeapon myWeapon)
        {
            this.myWeapon = myWeapon;
            SyncCollision();
        }

        /// <summary>
        /// 同步射弹的碰撞体大小
        /// 初始化时调用
        /// </summary>
        protected abstract void SyncCollision();


        /// <summary>
        /// 碰撞检测
        /// 处理攻击检测的攻击效果逻辑
        /// 调用时机不确定
        /// </summary>
        protected abstract void CheckHit();

        /// <summary>
        /// 子弹攻击逻辑
        ///  检测阵营、广播事件 
        /// </summary>
        protected virtual void DoAttack(IAttackAble attackAble)
        {
            EventBus.Invoke(new AttackEvent.WeaponAttack { Weapon = myWeapon, Projectile = this, Target = attackAble });
        }
    }
}