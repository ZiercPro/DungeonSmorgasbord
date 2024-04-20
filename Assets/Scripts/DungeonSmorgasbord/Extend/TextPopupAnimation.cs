using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Extend
{
    /// <summary>
    /// 文本跳动动画，用于实现例如伤害数字等的显示
    /// </summary>
    public class TextPopupAnimation : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f; //文本抖动持续时间
        [SerializeField] private float moveDuration = 0.2f; //文本移动持续时间
        private static int _sortingID = 0; //图层顺序
        private RectTransform _rect;
        private TextMeshPro _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshPro>();
            _rect = GetComponent<RectTransform>();
        }

        /// <summary>
        /// 弹出文本
        /// </summary>
        public void Popup()
        {
            _sortingID++;
            _text.sortingOrder = _sortingID;
            float xMin = -1f; //x移动区间最小
            float xMax = 1f; //x移动区间最大
            float yMin = 2f; //y移动区间最小
            float yMax = 3f; //y移动区间最大
            float xDistance = Random.Range(xMin, xMax);
            float yDistance = Random.Range(xDistance + yMin, xDistance + yMax);
            Vector2 startPos = _rect.anchoredPosition;
            Vector2 endPos = new Vector2(startPos.x + xDistance, startPos.y + yDistance);
            //动画
            _rect.DOAnchorPos(endPos, moveDuration).SetEase(Ease.InSine).OnComplete(() =>
                _rect.DOShakePosition(duration, 0.2f).OnComplete(() =>
                    _text.DOFade(0f, duration).From(_text.color).OnComplete(DestroySelf)));
        }

        /// <summary>
        /// 自我销毁的方法，后续可拓展到 对象池中
        /// </summary>
        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}