using DG.Tweening;
using UnityEngine;

namespace ZiercCode._DungeonGame.Weapon.Weapon_LuGerGun
{
    public class ShootFlash : MonoBehaviour
    {
        [SerializeField] private Transform flash;
        [SerializeField] private float flashDuration;
        [SerializeField] private Vector2 scaleRange;
        [SerializeField] private Vector2 rotateRange;

        public void DoFlash()
        {
            flash.gameObject.SetActive(true);
            flash.Rotate(flash.forward, Random.Range(rotateRange.x, rotateRange.y));
            flash.DOScale(Random.Range(scaleRange.x, scaleRange.y), flashDuration).SetEase(Ease.Flash)
                .OnComplete(() =>
                    flash.DOScale(0f, flashDuration).SetEase(Ease.Flash)
                        .OnComplete(() => flash.gameObject.SetActive(false)));
        }
    }
}