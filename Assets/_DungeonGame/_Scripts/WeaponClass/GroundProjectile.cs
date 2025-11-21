using ZiercCode.ObjectPool;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 水平飞行类射弹
    /// 如子弹，法球等有飞行轨迹，且不需要伪高度效果的射弹
    /// </summary>
    public abstract class GroundProjectile : Projectile
    {
        protected override void PauseUpdate()
        {
            base.PauseUpdate();
            DistanceRelease();
            CheckHit();
        }

        public override void Init(BaseWeapon myWeapon)
        {
            base.Init(myWeapon);
            myTransform.localScale = StartSize * myWeapon.projectileSize;
        }

        protected virtual void DistanceRelease()
        {
            if (currentMoveDistance >= myWeapon.shootDistance)
            {
                PoolManager.Instance.Release(myWeapon.projectilePoolName, gameObject);
            }
        }
    }
}