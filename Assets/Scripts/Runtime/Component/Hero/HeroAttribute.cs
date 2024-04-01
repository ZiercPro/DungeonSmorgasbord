using UnityEngine;
using ZiercCode.Runtime.Component.Base;
using ZiercCode.Runtime.Helper;
using ZiercCode.Runtime.ScriptObject;
using ZiercCode.Runtime.Weapon;

namespace ZiercCode.Runtime.Component.Hero
{

    public class HeroAttribute : Attribute
    {
        [SerializeField] private HeroAttributeSO attributeData;

        public SerDictionary<WeaponType, float> weaponDamageRate;
        public float criticalChance;

        private void Awake()
        {
            if (attributeData == null) return;
            moveSpeed = attributeData.moveSpeed;
            maxHealth = attributeData.maxHealth;
            criticalChance = attributeData.criticalChance;
            weaponDamageRate = attributeData.weaponDamageRate;
        }
    }
}