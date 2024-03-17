using UnityEngine;

public class DroppedCoin : DroppedItem
{
    public void GetItem(CoinPack pack)
    {
        pack.GetCoins(num);
        AudioPlayerManager.Instance.PlayAudio(Audios.coinCollected);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetItem(other.GetComponent<Hero>().CoinPack);
            num = 0;
            Destroy(gameObject);
        }
    }
}