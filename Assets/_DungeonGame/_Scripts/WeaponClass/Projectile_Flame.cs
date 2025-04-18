using UnityEngine;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode._DungeonGame._Scripts.WeaponClass;
using ZiercCode.EventBusSystem;
using ZiercCode.ObjectPool;
using ZiercCode.Utilities;

namespace ZiercCode
{
    /// <summary>
    /// 喷火器射弹
    /// </summary>
    public class Projectile_Flame : BaseProjectile
    {
        private RangeDetector _rangeDetector = new RangeDetector(15);

        private Vector2 _currentCollisionBox;

        protected override void SyncCollision()
        {
            _currentCollisionBox.x = myWeapon.projectileMaxDistance;
            _currentCollisionBox.y = myWeapon.projectileSize;
        }

        public override void Init(BaseWeapon myWeapon)
        {
            base.Init(myWeapon);

            CheckHit();
        }

        private void CheckHit()
        {
            if (_rangeDetector.DetectInBox(transform.position + transform.right * _currentCollisionBox.x / 2f,
                    _currentCollisionBox, transform.rotation.eulerAngles.z))
            {
                Collider2D[] hits = _rangeDetector.GetColliders();
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i] && hits[i].TryGetComponent(out IAttackAble attackable))
                    {
                        EventBus.Invoke(new AttackEvent.WeaponAttack
                        {
                            Weapon = myWeapon, Projectile = this, Target = attackable
                        });
                    }
                }
            }

            PoolManager.Instance.Release(myWeapon.projectilePoolName, gameObject);
        }
    }
}