using System.Timers;
using UnityEngine;
using ZiercCode.Core.Pool;
using Timer = ZiercCode.Core.Utilities.Timer;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 武器射弹组件
    /// 用于实现射弹发射及之后的各种逻辑
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class WeaponProjectile : WeaponBase
    {
        /// <summary>
        /// 刚体
        /// </summary>
        [SerializeField] private Rigidbody2D rigidBody2D;

        /// <summary>
        /// 生命时长
        /// </summary>
        [SerializeField] private float lifeTime = 5f;

        /// <summary>
        /// 自我释放计时器
        /// </summary>
        private Timer _selfReleaseTimer;

        /// <summary>
        /// 发射方向
        /// </summary>
        private Vector3 _direction;

        /// <summary>
        /// 是否被发射
        /// </summary>
        private bool _isFired;

        /// <summary>
        /// 目标
        /// </summary>
        private Transform _target;

        public override void Init(IWeaponUserBase weaponUserBase)
        {
            base.Init(weaponUserBase);
            rigidBody2D.isKinematic = true;
        }

        private void Update()
        {
            OnFly();
        }

        /// <summary>
        /// 发射
        /// </summary>
        public void Fire(float speed)
        {
            rigidBody2D.isKinematic = false;
            Vector3 fireSpeed = transform.right.normalized * speed;
            rigidBody2D.velocity = fireSpeed;
            _isFired = true;
            _selfReleaseTimer = new Timer(lifeTime);
            _selfReleaseTimer.TimerTrigger += () =>
            {
                //PoolManager.Instance.ReleasePoolObject(GetWeaponDataSo().myName, gameObject);
            };
            _selfReleaseTimer.StartTimer();
        }

        /// <summary>
        /// 飞行中更新
        /// </summary>
        private void OnFly()
        {
            if (!_isFired) return;
            _selfReleaseTimer.Tick();
        }
    }
}