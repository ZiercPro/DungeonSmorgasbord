using System;
using UnityEngine;
using ZiercCode.Runtime.Helper;
using ZiercCode.Runtime.Damage;
using System.Collections.Generic;
using ZiercCode.Runtime.Component;
using ZiercCode.Runtime.Component.Enemy;
using ZiercCode.Runtime.Enemy.EnemyState;

namespace ZiercCode.Runtime.Enemy
{
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        public EnemyStateMachine stateMachine { get; private set; }
        public EnemyAttribute attribute { get; private set; }
        public AttackCheck attackCheck { get; private set; }
        public Movement movement { get; private set; }
        public Animator animator { get; private set; }
        public Health health { get; private set; }
        public Health attackTarget { get; private set; }

        private static List<Enemy> s_enemys;
        public event Action<DamageInfo> OnTakeDamage;

        public virtual void TakeDamage(DamageInfo info)
        {
            OnTakeDamage?.Invoke(info);
        }

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
            attribute = GetComponentInChildren<EnemyAttribute>();
            attackCheck = GetComponentInChildren<AttackCheck>();
            movement = GetComponentInChildren<Movement>();
            animator = GetComponentInChildren<Animator>();
            health = GetComponentInChildren<Health>();
            stateMachine = new EnemyStateMachine();
            attackTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        protected virtual void Start()
        {
            movement.Initialize(attribute.moveSpeed);
            health.Initialize(attribute.maxHealth);
            health.Dead += () =>
            {
                Dead();
            };
        }

        protected virtual void Update()
        {
            stateMachine.currentState.FrameUpdate();
        }

        protected virtual void FixedUpdate()
        {
            stateMachine.currentState.PhysicsUpdate();
        }

        public void ChangeTarget(Health target)
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