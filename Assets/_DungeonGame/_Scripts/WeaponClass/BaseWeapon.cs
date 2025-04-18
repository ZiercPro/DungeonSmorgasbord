using NaughtyAttributes.Scripts.Core.MetaAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.EventBusSystem;
using ZiercCode.Management;
using ZiercCode.ObjectPool;
using ZiercCode.Utilities;
using Random = UnityEngine.Random;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 武器基类
    /// </summary>
    public abstract class BaseWeapon : PauseBehaviour
    {
        /// <summary>
        /// 所有伤害类型的伤害数值
        /// </summary>
        [Header("基础配置")]
        public EditableDictionary<DamageType, float> damage;

        /// <summary>
        /// 冲击力 击退效果强度
        /// </summary>
        public float hitForce;

        /// <summary>
        /// 暴击率
        /// </summary>
        public float criticalChance;

        /// <summary>
        /// 暴击倍率
        /// </summary>
        public float criticalDamageRate;

        /// <summary>
        /// 触发几率
        /// </summary>
        public float triggerChance;

        /// <summary>
        /// 距离伤害衰减
        /// </summary>
        public float damageReductionByDistance;

        //开火模式
        public FireMode fireMode;

        /// <summary>
        /// 射击速度
        /// </summary>
        public float fireSpeed;

        /// <summary>
        /// 射击距离
        /// </summary>
        public float projectileMaxDistance;

        /// <summary>
        /// 弹匣容量
        /// </summary>
        public int magazineCapacity;

        /// <summary>
        /// 当前子弹数量
        /// </summary>
        public int projectileCount;

        /// <summary>
        /// 换弹时间
        /// </summary>
        public float reloadTime;

        /// <summary>
        /// 射击精准度
        /// </summary>
        public float accuracy;

        /// <summary>
        /// 单次射击射弹数量
        /// </summary>
        public float projectileNumPerShoot;

        /// <summary>
        /// 射弹类型
        /// </summary>
        public ProjectileType projectileType;

        /// <summary>
        /// 射弹速度
        /// </summary>
        [HideIf("projectileType", ProjectileType.Ray)]
        public float projectileSpeed;

        /// <summary>
        /// 射弹大小
        /// </summary>
        public float projectileSize;

        // /// <summary>
        // /// 射弹存活时间
        // /// </summary>
        // public float projectileStayTime;

        /// <summary>
        ///射弹对象池名
        /// </summary>
        public string projectilePoolName;

        [SerializeField]
        protected int projectilePoolMinSize = 15;

        [SerializeField]
        protected int projectilePoolMaxSize = 150;

        [SerializeField]
        protected GameObject projectilePrefab; //射弹预制体

        [SerializeField]
        protected Transform firePoint; //子弹生成位置

        /// <summary>
        /// 是否正在换弹
        /// </summary>
        [HideInInspector]
        public bool isReloading;

        protected PlayerInputAction PlayerInputAction;

        protected readonly EventsGroup EventsGroup = new();

        /// <summary>
        /// 换弹计时器
        /// </summary>
        protected float ReloadTimer;

        //事件实例
        protected WeaponEvent.WeaponReloaded WeaponReloadedArgs;
        protected WeaponEvent.WeaponStartReload WeaponStartReloadArgs;
        protected WeaponEvent.WeaponFired WeaponFiredArgs;

        protected virtual void Awake()
        {
            PlayerInputAction = new PlayerInputAction();

            PoolManager.Instance.Register(projectilePoolName, projectilePrefab, projectilePoolMinSize,
                projectilePoolMaxSize);

            WeaponStartReloadArgs = new WeaponEvent.WeaponStartReload { Weapon = this };
            WeaponReloadedArgs = new WeaponEvent.WeaponReloaded { Weapon = this };
            WeaponFiredArgs = new WeaponEvent.WeaponFired { Weapon = this };
        }

        protected virtual void OnEnable()
        {
            PlayerInputAction.HeroControl.Enable();

            PlayerInputAction.HeroControl.Reload.performed += HandleReloadInput;

            projectileCount = magazineCapacity;
        }

        protected virtual void OnDisable()
        {
            PlayerInputAction.HeroControl.Reload.performed -= HandleReloadInput;

            PlayerInputAction.HeroControl.Disable();

            EventsGroup.RemoveAllListener();
        }

        protected void OnDestroy()
        {
            PoolManager.Instance.Dispose(projectilePoolName);
        }

        /// <summary>
        /// 根据精准度获取射击方向
        /// </summary>
        /// <param name="currentRotation">准确的射击方向</param>
        /// <returns></returns>
        protected virtual Quaternion GetShootRotation(Quaternion currentRotation)
        {
            //射击偏角
            float random = Random.Range(accuracy, 1f);
            float angle = Mathf.Acos(random) * Mathf.Rad2Deg;
            float shootAngle = Random.Range(-angle, angle);
            //当前偏角
            currentRotation.ToAngleAxis(out float currentAngle, out Vector3 currentAxis);

            currentAngle += shootAngle;
            Quaternion rotation = Quaternion.AngleAxis(currentAngle, currentAxis);
            return rotation;
        }

        /// <summary>
        /// 换弹 
        /// </summary>
        protected virtual void Reload()
        {
            if (isReloading || projectileCount == magazineCapacity) return;
            ReloadTimer = reloadTime;
            isReloading = true;
            StartCoroutine(ReloadCoroutine());
        }

        private void HandleReloadInput(InputAction.CallbackContext context)
        {
            Reload();
        }

        protected virtual IEnumerator ReloadCoroutine()
        {
            EventBus.Invoke(WeaponStartReloadArgs);
            
            while (ReloadTimer > 0f)
            {
                ReloadTimer -= Time.deltaTime;
                yield return null;
            }

            projectileCount = magazineCapacity;
            isReloading = false;

            EventBus.Invoke(WeaponReloadedArgs);
        }

        /// <summary>
        /// 激活击退效果
        /// </summary>
        protected void ActiveKickBack()
        {
            EventsGroup.RemoveListener<AttackEvent.WeaponAttack>(KickBack);
            EventsGroup.AddListener<AttackEvent.WeaponAttack>(KickBack);
        }

        /// <summary>
        /// 击退
        /// </summary>
        protected virtual void KickBack(IEventArgs args)
        {
            if (args is AttackEvent.WeaponAttack weaponAttack)
            {
                if (weaponAttack.Weapon == this)
                {
                    if (weaponAttack.Target.Transform.TryGetComponent(out KnockBackFeedBack knockBackFeedBack))
                    {
                        knockBackFeedBack.StartBackMove(weaponAttack.Projectile.transform.right, hitForce);
                    }
                }
            }
        }

        /// <summary>
        /// 检查弹药量
        /// </summary>
        protected virtual void CheckProjectileCount()
        {
            if (projectileCount <= 0)
            {
                Reload();
            }
        }

        protected abstract void Fire();
    }
}