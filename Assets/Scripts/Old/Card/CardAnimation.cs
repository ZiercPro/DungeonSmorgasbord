using DG.Tweening;
using UnityEngine;

namespace ZiercCode.Runtime.Card
{
    public class CardAnimation : MonoBehaviour
    {
        private float startY;

        private void Awake()
        {
            startY = transform.position.y;
        }

        public void CardEnter()
        {
            transform.DOScale(1.2f, 0.2f).SetUpdate(true).OnUpdate(() =>
            {
                transform.DOMoveY(startY + 100f, 0.2f).SetUpdate(true);
            });
        }

        public void CardExit()
        {
            transform.DOScale(1.0f, 0.2f).SetUpdate(true).OnUpdate(() =>
            {
                transform.DOMoveY(startY, 0.2f).SetUpdate(true);
            });
        }
    }
}