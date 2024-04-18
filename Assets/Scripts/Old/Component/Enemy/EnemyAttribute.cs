using UnityEngine;
using ZiercCode.Runtime.Damage;
using ZiercCode.Runtime.ScriptObject;

namespace ZiercCode.Runtime.Component.Enemy
{
    public class EnemyAttribute : Attribute<EnemyAttributeSo>
    {
        public DamageType damageType;
        public float attackSpeed;
        public float attackRange;
        public int damageAmount;

        public void OnValidate()
        {
            if (_attributesBaseSo == null) return;
            moveSpeed = _attributesBaseSo.moveSpeed;
            maxHealth = _attributesBaseSo.maxHealth;
            damageType = _attributesBaseSo.damageType;
            attackSpeed = _attributesBaseSo.attackSpeed;
            attackRange = _attributesBaseSo.attackRange;
            damageAmount = _attributesBaseSo.damageAmount;
        }
    }
}