using UnityEngine;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode.Management;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.EntityClasses
{
    /// <summary>
    /// 实体基类
    /// 主要是游戏中的一些活体 如局内NPC 敌人 玩家等
    /// </summary>
    public abstract class Entity : PauseBehaviour, IAttackAble
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        public float moveSpeed;

        /// <summary>
        /// 行为速率
        /// </summary>
        public float motionSpeed = 1f;

        /// <summary>
        /// 敌对阵营
        /// </summary>
        public LayerMask targetFaction;

        /// <summary>
        /// 是否已经死亡
        /// </summary>
        public bool isDead;

        protected virtual void Start()
        {
            isDead = false;
            CurrentHealth = MaxHealth;
        }

        protected override void PauseUpdate()
        {
            base.PauseUpdate();
            DeathCheck();
        }

        [field: SerializeField]
        public float MaxHealth { get; set; }

        public float CurrentHealth { get; set; }

        [field: SerializeField]
        public float Armor { get; set; }

        [field: SerializeField]
        public float DamageReduction { get; set; }

        [field: SerializeField]
        public EditableDictionary<DamageType, float> ElementResistanceTable { get; set; }

        public LayerMask MyFaction => gameObject.layer;
        public Transform Transform => transform;

        //检查实体是否死亡 并执行死亡逻辑
        protected abstract void DeathCheck();
    }
}