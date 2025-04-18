using UnityEngine;
using ZiercCode.EventBusSystem;
using ZiercCode.FakeHeight;
using ZiercCode.ObjectPool;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 鲁格手枪
    /// </summary>
    public class Weapon_LuGerPistol : SingleAndSemiAutoWeapon
    {
        protected override void Awake()
        {
            base.Awake();

            ActiveKickBack();
        }

        protected override void Fire()
        {
            if (isReloading)
            {
                return;
            }

            for (int i = 0; i < projectileNumPerShoot; i++)
            {
                Quaternion rotation = GetShootRotation(firePoint.rotation);

                GameObject newBullet = (GameObject)PoolManager.Instance.Get(projectilePoolName);

                newBullet.transform.position = firePoint.position;
                newBullet.transform.rotation = rotation;

                BaseProjectile projectileWeaponLuGerPistol = newBullet.GetComponent<BaseProjectile>();
                projectileWeaponLuGerPistol.Init(this);

                FakeHeightTransform fakeHeight = newBullet.GetComponent<FakeHeightTransform>();
                fakeHeight.Init(newBullet.transform.right * projectileSpeed, 0f, true, 0f, .5f);
            }

            projectileCount--;

            EventBus.Invoke(WeaponFiredArgs);

            CheckProjectileCount();
        }
    }
}