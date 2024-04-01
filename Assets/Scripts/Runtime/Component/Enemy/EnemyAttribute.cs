using UnityEngine;

namespace ZRuntime
{

    public class EnemyAttribute : Attribute
    {
        [SerializeField] private EnemyAttributeSO attributeData;

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