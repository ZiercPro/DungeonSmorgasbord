using UnityEngine;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.EventBusSystem;
using ZiercCode.ObjectPool;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 火焰喷射器
    /// </summary>
    public class Weapon_FlameThrower : AutoWeapon
    {
        [Space]
        [Header("火焰喷射器")]
        [SerializeField]
        private GameObject flameParticles;

        private ParticleSystem _flameParticles;
        private bool _isPlaying;

        private float _startFireDistance;
        private Vector3 _flameStartScale;

        protected override void Awake()
        {
            base.Awake();
            InitFlameParticles();
        }

        protected override void PauseUpdate()
        {
            base.PauseUpdate();

            ThrowFlame();
        }

        protected override void PauseFixedUpdate()
        {
            base.PauseFixedUpdate();

            SyncFlameParticles();
        }

        private void InitFlameParticles()
        {
            _flameParticles = Instantiate(flameParticles, transform).GetComponent<ParticleSystem>();
            _flameParticles.transform.position = firePoint.position;

            _startFireDistance = shootDistance;
            _flameStartScale = flameParticles.transform.localScale;
        }

        //同步火焰粒子的距离和宽度
        private void SyncFlameParticles()
        {
            _flameParticles.transform.localScale = new Vector3(
                _flameStartScale.x * shootDistance / _startFireDistance,
                _flameStartScale.y * projectileSize, _flameStartScale.z);
        }

        //喷射火焰粒子特效
        private void ThrowFlame()
        {
            if (!isReloading && PlayerInputAction.HeroControl.MouseClickLeft.ReadValue<float>() > 0f)
            {
                if (!_isPlaying)
                {
                    _flameParticles.Play();
                    _isPlaying = true;
                }
            }
            else if (_isPlaying)
            {
                _flameParticles.Stop();
                _isPlaying = false;
            }
        }

        protected override void Fire()
        {
            if (isReloading)
            {
                return;
            }

            for (int i = 0; i < GetProjectileNum(); i++)
            {
                GameObject flame = (GameObject)PoolManager.Instance.Get(projectilePoolName);

                flame.transform.position = firePoint.position;
                flame.transform.rotation = GetShootRotation(firePoint.rotation);

                BaseProjectile flameProjectile = flame.GetComponent<BaseProjectile>();
                flameProjectile.Init(this);
            }

            currentMagazineCount--;

            EventBus.Invoke(new WeaponEvent.WeaponFired { Weapon = this });

            CheckReload();
        }
    }
}