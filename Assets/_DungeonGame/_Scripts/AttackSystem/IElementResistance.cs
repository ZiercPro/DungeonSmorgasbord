using System.Collections.Generic;

namespace ZiercCode._DungeonGame._Scripts.AttackSystem
{
    /// <summary>
    /// 元素抗性 具有元素抗性的物体需要继承
    /// </summary>
    public interface IElementResistance
    {
        /// <summary>
        /// 元素抗性表
        /// </summary>
        public Dictionary<DamageType, float> ElementResistanceTable { get;}
    }
}