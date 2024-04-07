using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ZiercCode.Runtime.UI
{
    public class PopupText : MonoBehaviour
    {
        private static int sortingID = 0;
        private RectTransform rect;
        private TextMeshPro m_text;
        private float xdis;
        private float ydis;
        public float duration = 0.5f;
        public float moveduration = 0.2f;
        //public Vector2 punch = new Vector2(1f, 2f);

        private void Awake()
        {
            m_text = GetComponent<TextMeshPro>();
            rect = GetComponent<RectTransform>();
        }

        public void Popup()
        {
            sortingID++;
            m_text.sortingOrder = sortingID;
            xdis = Random.Range(0f, 1f);
            ydis = Random.Range(xdis + 1f, xdis + 2f);
            Vector2 startPos = rect.anchoredPosition;
            Vector2 endPos = new Vector2(startPos.x + xdis, startPos.y + ydis);
            rect.DOAnchorPos(endPos, moveduration).SetEase(Ease.InSine).OnComplete(() =>
            {
                rect.DOShakePosition(duration, 0.2f).OnComplete(() =>
                {
                    m_text.DOFade(0f, duration).From(m_text.color).OnComplete(() =>
                    {
                        Died();
                    });
                });
            });
        }

        private void Died()
        {
            Destroy(gameObject);
        }
    }
}