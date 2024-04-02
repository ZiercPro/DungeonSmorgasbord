using System;

namespace ZiercCode.Runtime.Component.Hero
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
            //让gamepanel显示金币数量
            GetCoins(0);
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