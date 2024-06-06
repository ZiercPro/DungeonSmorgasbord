using NaughtyAttributes.Scripts.Core.DrawerAttributes;
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

        private T _attributesSo;

        public T AttributesData
        {
            get
            {
                if (_attributesSo == null)
                    _attributesSo = Instantiate(creatureAttributesBaseSo);

                return _attributesSo;
            }
        }
    }
}