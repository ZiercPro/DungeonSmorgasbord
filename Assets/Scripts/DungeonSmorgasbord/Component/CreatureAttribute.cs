using NaughtyAttributes;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class CreatureAttribute<T> : MonoBehaviour where T : CreatureAttributesSo
    {
        [SerializeField] protected T creatureAttributesBaseSo;
        [Space] [ReadOnly] public float criticalChance;
        [ReadOnly] public float moveSpeed;
        [ReadOnly] public int maxHealth;

        public virtual void ResetData()
        {
            moveSpeed = creatureAttributesBaseSo.moveSpeed;
            maxHealth = creatureAttributesBaseSo.maxHealth;
            criticalChance = creatureAttributesBaseSo.criticalChance;
        }

        protected void OnValidate()
        {
            ResetData();
        }
    }
}