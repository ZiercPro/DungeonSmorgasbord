using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using ZiercCode.ObjectPool;

namespace ZiercCode._DungeonGame.Weapon
{
    public class ShootBullet : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint; //开火位置

        [SerializeField] private float bulletOriginalSpeed; //子弹初始速度

        [SerializeField] private float fireInterval; //开火间隔

        private PlayerInputAction _playerInputAction;

        public UnityEvent OnShoot;

        private float fireTimer;
        private bool _canFire;

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            PoolManager.Instance.Register("bullet", bulletPrefab);
        }

        private void OnEnable()
        {
            _playerInputAction.HeroControl.Enable();
            //_playerInputAction.HeroControl.MouseClickLeft.started += Shoot;
            //_playerInputAction.HeroControl.MouseClickLeft.performed += Shoot;
        }

        private void OnDisable()
        {
            //_playerInputAction.HeroControl.MouseClickLeft.started -= Shoot;
            //_playerInputAction.HeroControl.MouseClickLeft.performed -= Shoot;
            _playerInputAction.HeroControl.Disable();
        }

        private void Update()
        {
            FireCoolDown();
            if (_playerInputAction.HeroControl.MouseClickLeft.ReadValue<float>() > 0f)
            {
                //Debug.Log("按住");
                if (!_canFire) return;

                GameObject newBullet = (GameObject)PoolManager.Instance.Get("bullet");

                newBullet.SetActive(true);
                newBullet.transform.position = firePoint.position;
                newBullet.transform.rotation = firePoint.rotation;

                FakeHeight.FakeHeightTransform fakeheight = newBullet.GetComponent<FakeHeight.FakeHeightTransform>();
                fakeheight.Init(firePoint.right * bulletOriginalSpeed, 0f, true, 0f, .5f);
                OnShoot.Invoke();

                fireTimer = fireInterval;
                _canFire = false;
            }
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            if (context.interaction is HoldInteraction)
            {
                //Debug.Log("按住");
                if (!_canFire) return;

                GameObject newBullet = (GameObject)PoolManager.Instance.Get("bullet");

                newBullet.SetActive(true);
                newBullet.transform.position = firePoint.position;
                newBullet.transform.rotation = firePoint.rotation;

                FakeHeight.FakeHeightTransform fakeheight = newBullet.GetComponent<FakeHeight.FakeHeightTransform>();
                fakeheight.Init(firePoint.right * bulletOriginalSpeed, 0f, true, 0f, .5f);
                OnShoot.Invoke();

                fireTimer = fireInterval;
                _canFire = false;
            }
        }

        private void FireCoolDown()
        {
            if (fireTimer > 0)
            {
                fireTimer -= Time.deltaTime;
            }
            else if (!_canFire) _canFire = true;
        }
    }
}