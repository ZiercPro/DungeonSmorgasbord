using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class EnemyAttribute : CreatureAttribute<EnemyAttributeSo>
    {
        public DamageType damageType;
        public float attackSpeed;
        public float attackRange;
        public int damageAmount;

        public override void ResetData()
        {
            if (creatureAttributesBaseSo == null) return;
            moveSpeed = creatureAttributesBaseSo.moveSpeed;
            maxHealth = creatureAttributesBaseSo.maxHealth;
            damageType = creatureAttributesBaseSo.damageType;
            attackSpeed = creatureAttributesBaseSo.attackSpeed;
            attackRange = creatureAttributesBaseSo.attackRange;
            damageAmount = creatureAttributesBaseSo.damageAmount;
        }
    }
}