using System.Collections.Generic;
using ZiercCode.Runtime.Component;
using ZiercCode.Runtime.Component.Hero;
using ZiercCode.Runtime.Weapon;

namespace ZiercCode.Runtime.Card
{
    /// <summary>
    /// 储存所有卡片的配置信息
    /// </summary>
    public static class Cards
    {
        /// <summary>
        /// 储存所有已经加载的卡片
        /// </summary>
        private static List<CardBase> cards;

        public static List<CardBase> GetCards()
        {
            if (cards == null)
            {
                cards = new List<CardBase>()
                {
                    card1,
                    card2,
                    card3,
                    card4,
                    card5,
                    card6
                };
            }

            return cards;
        }

        private static CardBase card1 = new CardBase(1000, CardType.Hero, "近战攻击伤害+6%", Hero =>
        {
            Hero.GetComponentInChildren<HeroAttribute>().WeaponDamageRate[WeaponType.Melee] *= 1.06f;
        });

        private static CardBase card2 = new CardBase(1001, CardType.Hero, "远程攻击伤害+6%", Hero =>
        {
            Hero.GetComponentInChildren<HeroAttribute>().WeaponDamageRate[WeaponType.Remotely] *= 1.06f;
        });

        private static CardBase card3 = new CardBase(1002, CardType.Hero, "移动速度+2%", Hero =>
        {
            Movement mc = Hero.GetComponentInChildren<Movement>();
            mc.ChangeSpeed(moveSpeed => moveSpeed * 0.02f);
        });

        private static CardBase card4 = new CardBase(1003, CardType.Hero, "最大生命值+2", Hero =>
        {
            Hero.GetComponentInChildren<Health>().SetMax(max =>
            {
                max += 2;
                return max;
            });
            Hero.GetComponentInChildren<Health>().SetCurrent(current =>
            {
                current += 2;
                return current;
            });
        });

        private static CardBase card5 = new CardBase(1004, CardType.Hero, "暴击几率+4%", Hero =>
        {
            Hero.GetComponentInChildren<HeroAttribute>().criticalChance += 0.04f;
        });

        private static CardBase card6 = new CardBase(1005, CardType.Hero, "魔法攻击伤害+6%", Hero =>
        {
            Hero.GetComponentInChildren<HeroAttribute>().WeaponDamageRate[WeaponType.Magic] *= 1.06f;
        });
    }
}