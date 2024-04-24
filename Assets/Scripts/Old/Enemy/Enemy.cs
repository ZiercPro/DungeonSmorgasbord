using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Core.Extend;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.Component;
using ZiercCode.Old.Component.Enemy;
using ZiercCode.Old.Enemy.EnemyState;

namespace ZiercCode.Old.Enemy
{
    public abstract class Enemy : MonoBehaviour, IDamageable
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

        protected virtual void Start()
        {
            MoveComponent.SetMoveSpeed(Attribute.moveSpeed);
            health.Initialize(Attribute.maxHealth);
            health.Dead += () =>
            {
                Dead();
            };
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

        public virtual void Dead(bool dropItem = true)
        {
            Destroy(gameObject);
        }

        /// <summary>
        /// 获取当前所有存活的敌人
        /// </summary>
        /// <returns>敌人链表</returns>
        public static List<Enemy> GetEnemys()
        {
            return s_enemys;
        }

        /// <summary>
        /// 销毁当前全部敌人实例
        /// </summary>
        public static void EnemyClear()
        {
            if (s_enemys == null || s_enemys.Count <= 0) return;
            MyMath.ForeachChangeListAvailable(s_enemys, enemy =>
            {
                if (enemy != null && enemy.isActiveAndEnabled) enemy.Dead(false);
            });
        }

        public virtual DamageInfo Attack()
        {
            return null;
        }
    }
}