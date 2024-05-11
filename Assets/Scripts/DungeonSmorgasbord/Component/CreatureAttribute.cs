using NaughtyAttributes;
using NaughtyAttributes.Scripts.Core.DrawerAttributes;
using NaughtyAttributes.Scripts.Core.MetaAttributes;
using System;
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
        [SerializeField, Expandable] protected T creatureAttributesBaseSo;

        [NonSerialized] public T AttributesData;

        private void Awake()
        {
            AttributesData = Instantiate(creatureAttributesBaseSo);
            
        }
    }
}