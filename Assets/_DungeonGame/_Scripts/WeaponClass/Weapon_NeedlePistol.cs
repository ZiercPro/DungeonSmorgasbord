using UnityEngine;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.EventBusSystem;
using ZiercCode.FakeHeight;
using ZiercCode.ObjectPool;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    public class Weapon_NeedlePistol : SingleAndSemiAutoWeapon
    {
        [Header("针头")]
        [SerializeField]
        private float maxHeight = 5f; //针头最高飞行高度

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

            int projectileNum = GetProjectileNum();

            for (int i = 0; i < projectileNum; i++)
            {
                GameObject newProjectile = (GameObject)PoolManager.Instance.Get(projectilePoolName);

                newProjectile.transform.position = firePoint.position;
                newProjectile.transform.rotation = GetShootRotation(firePoint.rotation);

                newProjectile.GetComponent<BaseProjectile>().Init(this);

                FakeHeightTransform fakeHeight = newProjectile.GetComponent<FakeHeightTransform>();

                //计算垂直速度
                float totalTime = shootDistance / projectileSpeed;
                fakeHeight.virtualGravity = maxHeight * 2 / Mathf.Pow(totalTime / 2f, 2);
                float verticalSpeed = totalTime / 2f * fakeHeight.virtualGravity;

                fakeHeight.Init(newProjectile.transform.right * projectileSpeed, verticalSpeed, true);
            }

            currentMagazineCount--;

            CheckReload();

            EventBus.Invoke(new WeaponEvent.WeaponFired { Weapon = this });
        }
    }
}