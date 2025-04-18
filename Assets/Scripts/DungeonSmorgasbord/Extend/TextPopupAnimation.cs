using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZiercCode.DungeonSmorgasbord.Extend
{
    /// <summary>
    /// 文本跳动动画，用于实现例如伤害数字等的显示
    /// </summary>
    public class TextPopupAnimation : MonoBehaviour
    {
        [SerializeField]
        private float fadeDuration = 1f; //文本淡化时间

        [SerializeField]
        private float shakeDuration = 0.5f; //文本抖动持续时间

        [SerializeField]
        private float moveDuration = 0.2f; //文本移动持续时间

        [SerializeField]
        private RectTransform rectTransform;

        [SerializeField]
        private TextMeshProUGUI textMeshPro;

        [SerializeField]
        private Vector2 xMoveRange; //水平移动范围

        [SerializeField]
        private Vector2 yMoveRange; //垂直移动范围

        [SerializeField]
        private Vector2 rotateRange; //旋转范围

        //private static int _sortingID = 0; //图层顺序

        /// <summary>
        /// 弹出文本
        /// </summary>
        public void Popup(Action releaseFunc)
        {
            rectTransform.localScale = Vector3.zero;
            rectTransform.rotation = Quaternion.identity;
            rectTransform.Rotate(rectTransform.forward, Random.Range(rotateRange.x, rotateRange.y));
            float xDistance = Random.Range(xMoveRange.x, xMoveRange.y);
            float yDistance = Random.Range(yMoveRange.x, yMoveRange.y);
            Vector2 startPos = rectTransform.anchoredPosition;
            Vector2 endPos = new Vector2(startPos.x + xDistance, startPos.y + yDistance);

            rectTransform.DOAnchorPos(endPos, moveDuration).From(startPos)
                .SetEase(Ease.OutBounce).OnPlay(() =>
                    rectTransform.DOScale(1, moveDuration).SetEase(Ease.OutBounce))
                .OnComplete(() =>
                    rectTransform.DOShakePosition(shakeDuration, 3f).OnComplete(() =>
                        textMeshPro.DOFade(0f, fadeDuration)
                            .OnComplete(() => releaseFunc?.Invoke())));
        }
    }
}