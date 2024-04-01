using UnityEngine;
using ZiercCode.Runtime.Component.Base;
using ZiercCode.Runtime.Damage;
using ZiercCode.Runtime.ScriptObject;

namespace ZiercCode.Runtime.Component.Enemy
{

    public class EnemyAttribute : Attribute
    {
        [SerializeField] private EnemyAttributeSo attributeData;

        public LayerMask attackLayer;
        public DamageType damageType;
        public float attackSpeed;
        public float attackRange;
        public int damageAmount;

        public void OnValidate()
        {
            if (attributeData == null) return;
            moveSpeed = attributeData.moveSpeed;
            maxHealth = attributeData.maxHealth;
            damageType = attributeData.damageType;
            attackLayer = attributeData.attackLayer;
            attackSpeed = attributeData.attackSpeed;
            attackRange = attributeData.attackRange;
            damageAmount = attributeData.damageAmount;
        }
    }
}