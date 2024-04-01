using UnityEngine;

namespace ZRuntime
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