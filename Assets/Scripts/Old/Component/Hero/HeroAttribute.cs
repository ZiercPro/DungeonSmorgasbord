using System.Collections.Generic;
using ZiercCode.Old.ScriptObject;
using ZiercCode.Old.Weapon;

namespace ZiercCode.Old.Component.Hero
{
    public class HeroAttribute : Attribute<HeroAttributeSo>
    {
        public Dictionary<WeaponType, float> WeaponDamageRate;
        public float criticalChance;

        private void Awake()
        {
            if (_attributesBaseSo == null) return;
            moveSpeed = _attributesBaseSo.moveSpeed;
            maxHealth = _attributesBaseSo.maxHealth;
            criticalChance = _attributesBaseSo.criticalChance;
            WeaponDamageRate = _attributesBaseSo.WeaponDamageRate.ToDictionary();
        }
    }
}