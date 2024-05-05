using NaughtyAttributes;
using ZiercCode.Core.Extend;
using ZiercCode.Core.Utilities;
using ZiercCode.DungeonSmorgasbord.ScriptObject;
using ZiercCode.DungeonSmorgasbord.Weapon;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class HeroAttribute : CreatureAttribute<HeroAttributeSo>
    {
        public EditableDictionary<WeaponType, float> weaponDamageRate;

        public override void ResetData()
        {
            if (creatureAttributesBaseSo == null) return;
            moveSpeed = creatureAttributesBaseSo.moveSpeed;
            maxHealth = creatureAttributesBaseSo.maxHealth;
            criticalChance = creatureAttributesBaseSo.criticalChance;
            weaponDamageRate = creatureAttributesBaseSo.weaponDamageRate;
        }
    }
}