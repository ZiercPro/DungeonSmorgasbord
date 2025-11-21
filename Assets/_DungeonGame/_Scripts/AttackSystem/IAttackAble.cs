using UnityEngine;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.AttackSystem
{
    public interface IAttackAble
    {
        /// <summary>
        /// 最大生命值
        /// </summary>
        public float MaxHealth { get; set; }

        /// <summary>
        /// 当前生命值
        /// </summary>
        public float CurrentHealth { get; set; }

        /// <summary>
        /// 护甲值
        /// </summary>
        public float Armor { get; set; }

        /// <summary>
        /// 伤害减免
        /// </summary>
        public float DamageReduction { get; set; }

        /// <summary>
        /// 元素抗性表
        /// </summary>
        public EditableDictionary<DamageType, float> ElementResistanceTable { get; set; }

        /// <summary>
        /// 阵营
        /// </summary>
        public LayerMask MyFaction { get; }

        /// <summary>
        /// 变化组件
        /// </summary>
        public Transform Transform { get; }
    }
}