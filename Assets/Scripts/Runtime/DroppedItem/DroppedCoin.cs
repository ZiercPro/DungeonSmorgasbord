using UnityEngine;
using ZiercCode.Runtime.Audio;
using ZiercCode.Runtime.Component.Hero;
using ZiercCode.Runtime.Manager;

namespace ZiercCode.Runtime.DroppedItem
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