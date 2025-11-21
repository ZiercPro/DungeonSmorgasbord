using System.Collections;
using UnityEngine;
using ZiercCode.FakeHeight;
using ZiercCode.ObjectPool;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 抛射类射弹
    /// 有飞行轨迹的，且需要伪高度效果的射弹
    /// </summary>
    public abstract class VerticalProjectile : Projectile
    {
        [SerializeField]
        protected float stayTimeAfterGrounded; //完全着地后存在的时长

        protected FakeHeightCollision FakeHeightCollision;
        protected Vector2 _startCollisionBox;

        protected override void Awake()
        {
            base.Awake();
            FakeHeightCollision = GetComponent<FakeHeightCollision>();
            _startCollisionBox = FakeHeightCollision.colliderBox;
        }

        public override void Init(BaseWeapon myWeapon)
        {
            base.Init(myWeapon);
            myTransform.localScale = StartSize * myWeapon.projectileSize;
        }

        /// <summary>
        /// 完全触地后调用
        /// </summary>
        public void StartReleaseCounter()
        {
            StartCoroutine(ReleaseCountCoroutine());
        }

        protected override void SyncCollision()
        {
            FakeHeightCollision.colliderBox = _startCollisionBox * myWeapon.projectileSize;
        }

        /// <summary>
        /// 如果是触地时检测碰撞伤害 则通过fakeheighttransform调用这个
        /// </summary>
        public void HitOnGrounded()
        {
            CheckHit();
        }

        private IEnumerator ReleaseCountCoroutine()
        {
            yield return new WaitForSeconds(stayTimeAfterGrounded);
            PoolManager.Instance.Release(myWeapon.projectilePoolName, gameObject);
        }
    }
}