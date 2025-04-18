using UnityEngine;
using ZiercCode.ObjectPool;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 投射类射弹
    /// </summary>
    public abstract class Projectile : BaseProjectile
    {
        /// <summary>
        /// 当前移动的距离
        /// </summary>
        [HideInInspector]
        public float currentMoveDistance;

        /// <summary>
        /// 初始化时的位置
        /// </summary>
        protected Vector2 StartPosition;

        /// <summary>
        /// 初始大小
        /// </summary>
        protected Vector3 StartSize;

        protected virtual void Awake()
        {
            StartSize = transform.localScale;
        }

        protected override void PauseUpdate()
        {
            base.PauseUpdate();
            DistanceRelease();
        }

        protected override void PauseFixedUpdate()
        {
            base.PauseFixedUpdate();
            currentMoveDistance = Vector2.Distance(StartPosition, transform.position);
        }

        public override void Init(BaseWeapon myWeapon)
        {
            base.Init(myWeapon);
            currentMoveDistance = 0f;
            StartPosition = transform.position;
            transform.localScale = StartSize * myWeapon.projectileSize;
        }


        protected virtual void DistanceRelease()
        {
            if (!MyMath.CompareDistanceWithRange(StartPosition, transform.position, myWeapon.projectileMaxDistance))
            {
                PoolManager.Instance.Release(myWeapon.projectilePoolName, gameObject);
            }
        }
    }
}