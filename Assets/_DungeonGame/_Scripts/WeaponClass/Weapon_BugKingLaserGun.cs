using UnityEngine;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.EventBusSystem;
using ZiercCode.ObjectPool;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 母虫激光枪
    /// </summary>
    public class Weapon_BugKingLaserGun : AutoWeapon
    {
        protected override void Fire()
        {
            if (isReloading)
            {
                return;
            }

            for (int i = 0; i < GetProjectileNum(); i++)
            {
                GameObject newLaser = (GameObject)PoolManager.Instance.Get(projectilePoolName);
                Quaternion shootRotation = GetShootRotation(firePoint.rotation);

                newLaser.transform.position = firePoint.position;
                newLaser.transform.rotation = shootRotation;

                LaserProjectile laserProjectile = newLaser.GetComponent<LaserProjectile>();
                laserProjectile.Init(this);
            }

            currentMagazineCount--;

            EventBus.Invoke(new WeaponEvent.WeaponFired { Weapon = this });

            CheckReload();
        }
    }
}