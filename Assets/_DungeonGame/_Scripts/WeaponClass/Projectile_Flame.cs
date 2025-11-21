using UnityEngine;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode.ObjectPool;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 喷火器射弹
    /// </summary>
    public class Projectile_Flame : BaseProjectile
    {
        private RangeDetector _rangeDetector = new(15);

        private Vector2 _currentCollisionBox;

        protected override void SyncCollision()
        {
            _currentCollisionBox.x = myWeapon.shootDistance;
            _currentCollisionBox.y = myWeapon.projectileSize;
        }

        public override void Init(BaseWeapon myWeapon)
        {
            base.Init(myWeapon);

            CheckHit();
        }

        protected override void CheckHit()
        {
            if (_rangeDetector.DetectInBoxByLayer(myWeapon.myHolder.targetFaction,
                    myTransform.position + (myTransform.right * _currentCollisionBox.x / 2f),
                    _currentCollisionBox, myTransform.rotation.eulerAngles.z))
            {
                Collider2D[] hits = _rangeDetector.GetColliders();
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i] && hits[i].TryGetComponent(out IAttackAble attackable))
                    {
                        DoAttack(attackable);
                    }
                }
            }

            PoolManager.Instance.Release(myWeapon.projectilePoolName, gameObject);
        }
    }
}