using UnityEngine;
using ZiercCode.Audio;
using ZiercCode.Old.Manager;
using ZiercCode.Old.Component.Hero;

namespace ZiercCode.Old.DroppedItem
{
    public class DroppedCoin : DroppedItem
    {
        private void GetItem(CoinPack pack)
        {
            pack.GetCoins(num);
            AudioPlayer.Instance.PlaySfx("CoinCollected", transform.position);
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