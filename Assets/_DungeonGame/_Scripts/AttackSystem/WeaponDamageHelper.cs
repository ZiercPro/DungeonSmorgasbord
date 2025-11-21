using System.Collections.Generic;
using ZiercCode._DungeonGame._Scripts.WeaponClass;
using ZiercCode.Utilities;
using Random = UnityEngine.Random;

namespace ZiercCode._DungeonGame._Scripts.AttackSystem
{
    /// <summary>
    /// 用于武器的伤害计算
    /// </summary>
    public class WeaponDamageHelper
    {
        /// <summary>
        /// 计算面板总伤害
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public float GetWeaponTotalDamage(BaseWeapon weapon)
        {
            float damage = 0;

            foreach (float damageValue in weapon.damage.ToDictionary.Values)
            {
                damage += damageValue;
            }

            return damage;
        }

        /// <summary>
        /// 获取触发的debuff
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public Dictionary<DamageType, int> GetTriggeredDeBuff(BaseWeapon weapon)
        {
            Dictionary<DamageType, int> result = new();
            //触发数量 取触发小数点前自然数
            int triggerNum = (int)weapon.triggerChance;

            //剩余触发几率 按概率算 是否多加一个触发
            float chance = weapon.triggerChance - triggerNum;
            triggerNum += MyMath.ChanceToInt(chance);

            float totalDamage = GetWeaponTotalDamage(weapon);

            for (int i = 0; i < triggerNum; i++)
            {
                float random = Random.Range(0f, totalDamage);

                foreach (KeyValuePair<DamageType, float> kv in weapon.damage.ToDictionary)
                {
                    if (kv.Value <= 0f)
                    {
                        continue;
                    }

                    random -= kv.Value;
                    if (random <= 0f) //刚好落在对应区间
                    {
                        if (!result.TryAdd(kv.Key, 1))
                        {
                            result[kv.Key]++;
                            break;
                        }
                    }
                }
            }

            return result;
        }
    }
}