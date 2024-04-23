using NaughtyAttributes;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class EnemyAttribute : CreatureAttribute<EnemyAttributeSo>
    {
        [ReadOnly] public DamageType damageType;
        [ReadOnly] public float attackSpeed;
        [ReadOnly] public float attackRange;
        [ReadOnly] public int damageAmount;

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