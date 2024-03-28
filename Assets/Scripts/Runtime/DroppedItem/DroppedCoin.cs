using UnityEngine;
using Runtime.Audio;

namespace Runtime.DroppedItem
{
    using Component.Hero;
    using Manager;
    using Hero;

    public class DroppedCoin : DroppedItem
    {
        private void GetItem(CoinPack pack)
        {
            pack.GetCoins(num);
            AudioPlayerManager.Instance.PlayAudio(AudioName.CoinCollected);
            base.GetItem();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GetItem(other.GetComponent<Hero>().CoinPack);
                TextPopupSpawner.Instance.InitPopupText(other.transform.position, Color.yellow, "+" + num);
                num = 0;
                Destroy(gameObject);
            }
        }
    }
}