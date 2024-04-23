using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Damage;

namespace ZiercCode.DungeonSmorgasbord.ScriptObject
{
    [CreateAssetMenu(menuName = "ScriptObject/Attributes/Enemy", fileName = "Enemy")]
    public class EnemyAttributeSo : CreatureAttributesSo
    {
        /// <summary>
        /// 难度
        /// </summary>
        public int difficulty;

        /// <summary>
        /// 伤害
        /// </summary>
        public int damageAmount;

        /// <summary>
        /// 伤害类型
        /// </summary>
        public DamageType damageType;

        /// <summary>
        /// 攻击速度
        /// </summary>
        public float attackSpeed;

        /// <summary>
        /// 攻击距离
        /// </summary>
        public float attackRange;
    }
}