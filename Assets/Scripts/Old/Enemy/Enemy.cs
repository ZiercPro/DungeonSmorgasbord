using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.Core.Utilities;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.Component;
using ZiercCode.Old.Component.Enemy;
using ZiercCode.Old.Enemy.EnemyState;

namespace ZiercCode.Old.Enemy
{
    public abstract class Enemy : MonoBehaviour, IDamageable, IPoolObject
    {
        public AutoFlipComponent AutoFlipComponent { get; private set; }
        public EnemyStateMachine stateMachine { get; private set; }
        public EnemyAttribute Attribute { get; private set; }
        public AttackCheck attackCheck { get; private set; }
        public MoveComponent MoveComponent { get; private set; }
        public Animator animator { get; private set; }
        public Health health { get; private set; }
        public Health attackTarget { get; private set; }

        private static List<Enemy> s_enemys;

        private SpawnHandle _spawnHandle;

        public abstract void TakeDamage(DamageInfo info);

        protected void OnEnable()
        {
            if (s_enemys == null) s_enemys = new List<Enemy>();
            s_enemys.Add(this);
        }

        protected void OnDisable()
        {
            s_enemys.Remove(this);
        }

        protected virtual void Awake()
        {
            Attribute = GetComponentInChildren<EnemyAttribute>();
            attackCheck = GetComponentInChildren<AttackCheck>();
            AutoFlipComponent = GetComponent<AutoFlipComponent>();
            MoveComponent = GetComponentInChildren<MoveComponent>();
            animator = GetComponentInChildren<Animator>();
            health = GetComponentInChildren<Health>();
            stateMachine = new EnemyStateMachine();
            SetTarget(GameObject.FindGameObjectWithTag("Player").GetComponent<Health>());
        }

        protected void Start()
        {
            MoveComponent.SetMoveSpeed(Attribute.moveSpeed);
            attackCheck.SetRadius(Attribute.attackRange);
        }

        public virtual void Init()
        {
        }

        protected virtual void Update()
        {
            stateMachine.currentState.FrameUpdate();
            AutoFlipComponent.FaceTo(attackTarget.transform.position);
        }

        protected virtual void FixedUpdate()
        {
            stateMachine.currentState.PhysicsUpdate();
        }

        public void SetTarget(Health target)
        {
            attackTarget = target;
        }

        public virtual void Dead()
        {
            _spawnHandle.Release();
        }

        public virtual DamageInfo Attack()
        {
            return null;
        }

        public void SetSpawnHandle(SpawnHandle handle)
        {
            _spawnHandle = handle;
        }

        public void OnGet()
        {
            Init();
            health.Init();
            health.Dead += Dead;
        }

        public void OnRelease()
        {
            health.Dead -= Dead;
        }
    }
}