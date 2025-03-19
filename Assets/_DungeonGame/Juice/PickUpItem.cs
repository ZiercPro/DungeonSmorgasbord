using DG.Tweening;
using UnityEngine;
using ZiercCode._DungeonGame.HallScene;
using ZiercCode.Audio;

namespace ZiercCode._DungeonGame.Juice
{
    public class PickUpItem : MonoBehaviour //可以拾取的物品
    {
        [SerializeField] private float pickUpRadius;
        [SerializeField] private AudioClip pickUpSound;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Material flashMaterial;

        private RangeDetect _rangeDetect = new RangeDetect(5);

        private bool _bePicked;

        private void FixedUpdate()
        {
            if (_rangeDetect.DetectByTagInCircle("Player", transform.position, pickUpRadius) && !_bePicked)
            {
                _bePicked = true;
                OnPickUp();
            }
        }

        public void OnPickUp()
        {
            AudioPlayer.Instance.PlaySfx(pickUpSound);

            spriteRenderer.material = flashMaterial;
            transform.DOShakePosition(0.15f, 0.4f).OnComplete(() =>
            {
                transform.DOScaleX(.5f, 0.05f).OnComplete(() => transform.DOScaleX(2f, 0.08f));
                transform.DOScaleY(1.25f, 0.05f).OnComplete(() =>
                    transform.DOScaleY(0f, 0.08f).OnComplete(() => gameObject.SetActive(false)));
            });
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, pickUpRadius);
        }
    }
}