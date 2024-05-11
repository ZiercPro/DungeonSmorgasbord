using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.Core.Utilities;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.DungeonSmorgasbord.Item;
using ZiercCode.Old.Component;
using ZiercCode.Old.Component.Enemy;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public abstract class Enemy : MonoBehaviour, IDamageable, IPoolObject
    {
        [Header("组件")] [SerializeField] protected ScaleShakeFeedBack scaleShakeFeedBack;
        [SerializeField] protected AutoFlipComponent autoFlipComponent;
        [SerializeField] protected PoolObjectSpawner poolObjectSpawner;
        [SerializeField] protected EnemyAttackCheck enemyAttackCheck;
        [SerializeField] protected MoveComponent moveComponent;
        [SerializeField] protected FlashFeedBack flashFeedBack;
        [SerializeField] protected CanDropItems canDropItems;
        [SerializeField] protected EnemyAttribute attribute;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Health health;

        protected EnemyStateMachine StateMachine;
        protected Transform FollowTarget;
        protected Health TargetHealth;

        private SpawnHandle _spawnHandle;
        private Timer _attackTimer;

        /// <summary>
        /// 是否可以攻击
        /// 因为虚方法在子类只是调用，无法阻止子类覆写的方法继续执行
        /// </summary>
        protected bool CanAttack = true;

        public virtual void TakeDamage(DamageInfo info)
        {
            int healthValue = health.GetCurrentHealth() - info.damageAmount;
            health.SetCurrentHealth(healthValue, Health.HealthChangeType.Damage);
            //大小变化
            scaleShakeFeedBack.StartShake();
            //闪白
            flashFeedBack.Flash();
        }

        protected virtual void Awake()
        {
            StateMachine = new EnemyStateMachine();
        }

        protected virtual void Start()
        {
            moveComponent.SetMoveSpeed(attribute.AttributesData.moveSpeed);
            enemyAttackCheck.SetRadius(attribute.AttributesData.attackRange);

            _attackTimer = new Timer(1 / attribute.AttributesData.attackSpeed);
            _attackTimer.TimerTrigger += () => CanAttack = true;

            SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
        }

        public virtual void Init()
        {
            health.Init();
            health.Dead += Dead;
        }

        public virtual void Attack(Transform target)
        {
            CanAttack = false;
            _attackTimer.StartTimer();
            IDamageable damageable = target.GetComponent<IDamageable>();
            DamageInfo damageInfo = new DamageInfo(attribute.AttributesData.damageAmount,
                attribute.AttributesData.damageType, transform);
            damageable.TakeDamage(damageInfo);
        }

        protected virtual void Update()
        {
            StateMachine.currentState.FrameUpdate();
            _attackTimer.Tick();
        }

        protected virtual void FixedUpdate()
        {
            StateMachine.currentState.PhysicsUpdate();
        }

        public void SetTarget(Transform target)
        {
            FollowTarget = target;
            if (target.TryGetComponent(out Health h))
            {
                TargetHealth = h;
            }
        }

        public Transform GetTarget()
        {
            return FollowTarget;
        }

        /// <summary>
        /// 检查目标是否存活
        /// </summary>
        /// <returns>如果有health组件则检查生命值，如果没有则默认存活</returns>
        public bool TargetIsAlive()
        {
            if (!TargetHealth || (TargetHealth && !TargetHealth.IsDead)) return true;
            return false;
        }

        public abstract EnemyIdleStateBase GetEnemyIdleState();
        public abstract EnemyChaseStateBase GetEnemyChaseState();
        public abstract EnemyAttackStateBase GetEnemyAttackState();


        public virtual void Dead()
        {
            canDropItems.DropItems();
            _spawnHandle.Release();
        }

        #region PoolFunc

        public void SetSpawnHandle(SpawnHandle handle)
        {
            _spawnHandle = handle;
        }


        public void OnGet()
        {
            Init();
        }

        public void OnRelease()
        {
            health.Dead -= Dead;
        }

        #endregion
    }
}