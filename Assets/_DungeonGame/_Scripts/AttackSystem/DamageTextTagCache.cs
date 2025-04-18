using System;
using System.Collections.Generic;
using System.Linq;

namespace ZiercCode._DungeonGame._Scripts.AttackSystem
{
    /// <summary>
    /// 伤害文本相关tag缓存
    /// </summary>
    public static class DamageTextTagCache
    {
        private static readonly Dictionary<DamageType, string> DamageTextIconNameCache; //伤害图标名缓存

        public static readonly string CriticalTagHead = "<size=115%><#FF0000>"; //暴击tag头
        public static readonly string CriticalTagEnd = "!!!</size></color>"; //暴击tag尾
        public static readonly string DamageNumTag = "{0}"; //伤害tag

        static DamageTextTagCache()
        {
            DamageType[] damageTypes = Enum.GetValues(typeof(DamageType)).Cast<DamageType>().ToArray();
            DamageTextIconNameCache = new Dictionary<DamageType, string>(damageTypes.Length);
            foreach (DamageType damageType in damageTypes)
            {
                DamageTextIconNameCache.Add(damageType, $"<sprite name={damageType.ToString().ToLower()}>");
            }
        }

        public static string GetSpriteTag(DamageType damageType)
        {
            return DamageTextIconNameCache[damageType];
        }
    }
}