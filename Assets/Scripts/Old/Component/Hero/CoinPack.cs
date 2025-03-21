using System;

namespace ZiercCode.Old.Component.Hero
{
    /// <summary>
    /// 储存硬币
    /// </summary>
    public class CoinPack
    {
        public event Action<int> CoinChanged;

        public CoinPack()
        {
            coinNum = 0;
        }

        public void Init(int amount)
        {
            GetCoins(amount);
        }

        public int coinNum { get; private set; }

        public void GetCoins(int num)
        {
            coinNum += num;
            CoinChanged?.Invoke(coinNum);
        }

        public void UseCoins(int num)
        {
            coinNum -= num;
            CoinChanged?.Invoke(coinNum);
        }
    }
}