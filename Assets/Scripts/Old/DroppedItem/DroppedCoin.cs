using UnityEngine;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Component.Hero;
using ZiercCode.Old.Manager;

namespace ZiercCode.Old.DroppedItem
{

    public class DroppedCoin : DroppedItem
    {
        private void GetItem(CoinPack pack)
        {
            pack.GetCoins(num);
            AudioPlayer.Instance.PlayAudioAsync(AudioName.CoinCollected);
            base.GetItem();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GetItem(other.GetComponent<Hero.Hero>().CoinPack);
                TextPopupSpawner.Instance.InitPopupText(other.transform.position, Color.yellow, "+" + num);
                num = 0;
                Destroy(gameObject);
            }
        }
    }
}