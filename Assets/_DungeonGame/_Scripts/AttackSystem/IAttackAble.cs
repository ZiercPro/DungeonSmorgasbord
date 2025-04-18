using UnityEngine;

namespace ZiercCode._DungeonGame._Scripts.AttackSystem
{
    public interface IAttackAble : IElementResistance
    {
        /// <summary>
        /// 基础生命值
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
        /// 物体变换组件
        /// </summary>
        public Transform Transform { get; }
    }
}