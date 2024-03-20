using System;
using UnityEngine;

public class DroppedCoin : DroppedItem
{
    private void GetItem(CoinPack pack)
    {
        pack.GetCoins(num);
        AudioPlayerManager.Instance.PlayAudio(GameRoot.Instance.AudioList.coinCollected);
        base.GetItem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetItem(other.GetComponent<Hero>().CoinPack);
            TextPopupSpawner.Instance.InitPopupText(other.transform, Color.yellow, "+" + num);
            num = 0;
            Destroy(gameObject);
        }
    }
}