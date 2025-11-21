using NaughtyAttributes.Scripts.Core.MetaAttributes;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode._DungeonGame._Scripts.EntityClasses;
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
        /// 武器名
        /// </summary>
        [Header("基础配置")]
        public string weaponName;

        /// <summary>
        /// 所有伤害类型的伤害数值
        /// </summary>
        public EditableDictionary<DamageType, float> damage;

        /// <summary>
        /// 冲击力 击退效果强度
        /// </summary>
        [Min(0f)]
        public float hitForce;

        /// <summary>
        /// 暴击率
        /// </summary>
        [Min(0f)]
        public float criticalChance;

        /// <summary>
        /// 暴击倍率
        /// </summary>
        [Min(0f)]
        public float criticalDamageRate;

        /// <summary>
        /// 触发几率
        /// </summary>
        [Min(0f)]
        public float triggerChance;

        /// <summary>
        /// 距离伤害衰减
        /// </summary>
        [Range(0f, 1f)]
        public float damageReductionByDistance;

        /// <summary>
        /// 开火模式
        /// </summary>
        public FireMode fireMode;

        /// <summary>
        /// 射击速度
        /// </summary>
        [Min(0f)]
        public float shootSpeed;

        /// <summary>
        /// 射击距离
        /// </summary>
        [Min(0f)]
        public float shootDistance;

        /// <summary>
        /// 弹匣容量
        /// </summary>
        [Min(1)]
        public int magazineCapacity;

        /// <summary>
        /// 当前子弹数量
        /// </summary>
        [HideInInspector]
        public int currentMagazineCount;

        /// <summary>
        /// 换弹时间
        /// </summary>
        [Min(0f)]
        public float reloadTime;

        /// <summary>
        /// 射击精准度
        /// </summary>
        [Range(0f, 1f)]
        public float accuracy;

        /// <summary>
        /// 单次射击射弹数量
        /// </summary>
        [Min(0f)]
        public float projectileNumPerShoot;

        /// <summary>
        /// 射弹类型
        /// </summary>
        public ProjectileType projectileType;

        /// <summary>
        /// 射弹速度
        /// </summary>
        [HideIf("projectileType", ProjectileType.Ray)]
        [Min(0f)]
        public float projectileSpeed;

        /// <summary>
        /// 射弹大小
        /// </summary>
        [Min(0f)]
        public float projectileSize;

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

        /// <summary>
        /// 持有者
        /// </summary>
        [HideInInspector]
        public Entity myHolder;

        protected virtual void Awake()
        {
            PlayerInputAction = new PlayerInputAction();

            PoolManager.Instance.Register(projectilePoolName, projectilePrefab, projectilePoolMinSize,
                projectilePoolMaxSize);
        }

        protected virtual void OnEnable()
        {
            PlayerInputAction.HeroControl.Enable();

            PlayerInputAction.HeroControl.Reload.performed += HandleReloadInput;

            EventsGroup.AddListener<WeaponEvent.WeaponDataChanged>(CheckDataValidity);
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

        public virtual void Init(Entity holder)
        {
            myHolder = holder;
            currentMagazineCount = magazineCapacity;
        }

        /// <summary>
        /// 检验武器数据是否合理
        /// </summary>
        protected virtual void CheckDataValidity(IEventArgs args)
        {
            if (args is WeaponEvent.WeaponDataChanged weaponDataChanged)
            {
                if (weaponDataChanged.Weapon == this)
                {
                    //伤害数值
                    DamageType[] damageTypes = damage.ToDictionary.Keys.ToArray();
                    for (int i = 0; i < damageTypes.Length; i++)
                    {
                        damage.ToDictionary[damageTypes[i]] = damage.ToDictionary[damageTypes[i]] < 0f
                            ? 0f
                            : damage.ToDictionary[damageTypes[i]];
                    }

                    //击退
                    hitForce = hitForce < 0f ? 0f : hitForce;
                    //暴击
                    criticalChance = criticalChance < 0f ? 0f : criticalChance;
                    criticalDamageRate = criticalDamageRate < 0f ? 0f : criticalDamageRate;
                    //触发
                    triggerChance = triggerChance < 0f ? 0f : triggerChance;
                    //衰减
                    if (damageReductionByDistance < 0f) damageReductionByDistance = 0f;
                    else if (damageReductionByDistance > 1f) damageReductionByDistance = 1f;
                    //射速
                    shootSpeed = shootSpeed < 0f ? 0f : shootSpeed;
                    //射击距离
                    shootDistance = shootDistance < 0f ? 0f : shootDistance;
                    //弹匣
                    magazineCapacity = magazineCapacity < 1 ? 1 : magazineCapacity;
                    currentMagazineCount = currentMagazineCount > magazineCapacity
                        ? magazineCapacity
                        : currentMagazineCount;
                    //换弹时间
                    reloadTime = reloadTime < 0f ? 0f : reloadTime;
                    //精准度
                    if (accuracy < 0f) accuracy = 0f;
                    else if (accuracy > 1f) accuracy = 1f;
                    //射弹
                    projectileNumPerShoot = projectileNumPerShoot < 0f ? 0f : projectileNumPerShoot;
                    projectileSpeed = projectileSpeed < 0f ? 0f : projectileSpeed;
                    projectileSize = projectileSize < 0f ? 0f : projectileSize;
                }
            }
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
            if (isReloading || currentMagazineCount == magazineCapacity)
            {
                return;
            }

            ReloadTimer = reloadTime;
            isReloading = true;
            StartCoroutine(CountReloadCoroutine());
        }

        private void HandleReloadInput(InputAction.CallbackContext context)
        {
            Reload();
        }

        protected virtual IEnumerator CountReloadCoroutine()
        {
            EventBus.Invoke(new WeaponEvent.WeaponStartReload { Weapon = this });

            while (ReloadTimer > 0f)
            {
                ReloadTimer -= Time.deltaTime;
                yield return null;
            }

            currentMagazineCount = magazineCapacity;
            isReloading = false;

            EventBus.Invoke(new WeaponEvent.WeaponReloaded { Weapon = this });
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
        /// 获取本次射击的射弹数量
        /// </summary>
        protected virtual int GetProjectileNum()
        {
            int result = (int)projectileNumPerShoot;
            float plusChance = projectileNumPerShoot - result;
            result += MyMath.ChanceToInt(plusChance);
            return result;
        }

        /// <summary>
        /// 检查弹药量 是否需要重新装填
        /// </summary>
        protected virtual void CheckReload()
        {
            if (currentMagazineCount <= 0)
            {
                Reload();
            }
        }

        protected abstract void Fire();
    }
}