using NaughtyAttributes.Scripts.Core.DrawerAttributes_SpecialCase;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;

namespace ZiercCode.Test
{
    public class CreatureAttributeTest : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private HeroAttribute attribute;

        [Button("测试")]
        public void Add()
        {
            attribute.AttributesData.criticalChance += 0.1f;
        }
#endif
    }
}