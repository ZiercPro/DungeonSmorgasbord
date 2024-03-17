using System;

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
