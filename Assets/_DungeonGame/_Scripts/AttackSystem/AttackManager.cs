using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode._DungeonGame._Scripts.Text;
using ZiercCode._DungeonGame._Scripts.WeaponClass;
using ZiercCode.EventBusSystem;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.AttackSystem
{
    /// <summary>
    /// 伤害行为管理
    /// </summary>
    public class AttackManager : USingleton<AttackManager>
    {
        private EventsGroup _eventsGroup = new();

        private WeaponDamageHelper _weaponDamageHelper = new();

        private StringBuilder _damageText = new();

        private void OnEnable()
        {
            _eventsGroup.AddListener<AttackEvent.WeaponAttack>(HandleWeaponAttack);
        }

        private void OnDisable()
        {
            _eventsGroup.RemoveAllListener();
        }

        //处理武器伤害事件
        private void HandleWeaponAttack(IEventArgs args)
        {
            if (args is AttackEvent.WeaponAttack weaponAttack)
            {
                if (weaponAttack.Target == null)
                {
                    return;
                }

                //武器面板伤害（拷贝）
                Dictionary<DamageType, float> allDamage = new(weaponAttack.Weapon.damage.ToDictionary);

                //本次攻击是否能够暴击
                int critical = MyMath.ChanceToInt(weaponAttack.Weapon.criticalChance);

                //获取被攻击者的元素抗性 如果没有 则默认全部无抗性(0f)
                Dictionary<DamageType, float> elementResistanceTable = GetStandardElementResistanceTable();
                foreach (KeyValuePair<DamageType, float> kv in weaponAttack.Target.ElementResistanceTable.ToDictionary)
                {
                    elementResistanceTable[kv.Key] = kv.Value;
                }

                //分别计算每种元素伤害
                DamageType[] allTypes = allDamage.Keys.ToArray();
                for (int i = 0; i < allTypes.Length; i++)
                {
                    //暴击加成
                    allDamage[allTypes[i]] *= 1 + (critical * weaponAttack.Weapon.criticalDamageRate);

                    //射弹伤害衰减
                    if (weaponAttack.Projectile.myWeapon.projectileType == ProjectileType.Projectile)
                    {
                        allDamage[allTypes[i]] *= 1 - (((Projectile)weaponAttack.Projectile).currentMoveDistance /
                            weaponAttack.Weapon.shootDistance * weaponAttack.Weapon.damageReductionByDistance);
                    }

                    //直接伤害减免、护甲、元素伤害减免
                    allDamage[allTypes[i]] = HandleAttackAbleDamage(weaponAttack.Target, allDamage[allTypes[i]]) *
                                             (1f - elementResistanceTable[allTypes[i]]);
                }

                //计算总伤害用于实际造成伤害和显示
                float totalDamage = allDamage.Values.Sum();

                if (totalDamage < 0f)
                {
                    totalDamage = 0f;
                }

                weaponAttack.Target.CurrentHealth -= totalDamage;

                //获取触发的异常状态和层数
                Dictionary<DamageType, int> damageTypes = _weaponDamageHelper.GetTriggeredDeBuff(weaponAttack.Weapon);

                _damageText.Clear();
                if (critical == 1)
                {
                    _damageText.Append(DamageTextTagCache.CriticalTagHead);
                }

                foreach (KeyValuePair<DamageType, int> kv in damageTypes)
                {
                    _damageText.Append(DamageTextTagCache.GetSpriteTag(kv.Key));
                }

                _damageText.Append(string.Format(DamageTextTagCache.DamageNumTag, (int)totalDamage));

                if (critical == 1)
                {
                    _damageText.Append(DamageTextTagCache.CriticalTagEnd);
                }

                TextPopup.Instance.SpawnText(weaponAttack.Target.Transform.position, Color.white,
                    _damageText.ToString());
            }
        }

        /// <summary>
        /// 计算被攻击者受到的伤害大小
        /// </summary>
        /// <param name="attackAble"></param>
        /// <param name="startDamage"></param>
        /// <returns></returns>
        private float HandleAttackAbleDamage(in IAttackAble attackAble, in float startDamage)
        {
            return startDamage * (1f - attackAble.DamageReduction) *
                   (1f - (attackAble.Armor / (attackAble.Armor + 100f)));
        }


        //获取标准的元素抗性表
        private Dictionary<DamageType, float> GetStandardElementResistanceTable()
        {
            Dictionary<DamageType, float> allDamageTypes =
                Enum.GetValues(typeof(DamageType)).Cast<DamageType>().ToDictionary(k => k, k => 0f);
            return allDamageTypes;
        }
    }
}