using DG.Tweening;
using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class ScaleShakeFeedBack : MonoBehaviour
    {
        [SerializeField] private Transform animatorTransform;
        [SerializeField] private float duration = 0.8f;
        [SerializeField] private float strength = 0.6f;


        private Tweener _shakeTweener;
        private bool _canShakeScale = true;

        public void StartShake()
        {
            if (!_canShakeScale) return;
            _canShakeScale = false;
            _shakeTweener = animatorTransform.DOShakeScale(duration, strength).SetEase(Ease.Flash)
                .OnComplete(() => { _canShakeScale = true; });
            _shakeTweener.SetAutoKill(true);
        }
    }
}