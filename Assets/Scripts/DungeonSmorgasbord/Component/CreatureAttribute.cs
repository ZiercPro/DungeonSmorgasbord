using NaughtyAttributes;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    /// <summary>
    /// 生物属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CreatureAttribute<T> : MonoBehaviour where T : CreatureAttributesSo
    {
        [field: SerializeField] public T creatureAttributesBaseSo { get; private set; }
        [Space] public float criticalChance;
        public float moveSpeed;
        public int maxHealth;

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