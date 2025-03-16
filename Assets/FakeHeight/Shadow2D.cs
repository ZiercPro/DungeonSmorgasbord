using Unity.VisualScripting;
using UnityEngine;

namespace ZiercCode.FakeHeight
{
    public class Shadow2D : MonoBehaviour //自动绘制2D阴影
    {
        [SerializeField] private Material shadowMaterial; //用于使阴影为纯色
        [SerializeField] private Color shadowColor;
        [SerializeField] private Vector3 shadowOffset = new Vector3(0.1f, -.1f, 0f); //阴影与物体的位置偏差
        [SerializeField] private float shadowSizeChangeRate = .02f; //影子大小变化速率

        [SerializeField] private Transform casterTransform; //本体
        [SerializeField] private SpriteRenderer casterSpriteRenderer;

        private SpriteRenderer _shadowRenderer;
        private Transform _shadowObjectTransform;

        public Transform ShadowObjectTransform => _shadowObjectTransform;
        public Transform CasterTransform => casterTransform;
        public Vector3 ShadowOffset => shadowOffset;

        private void Awake()
        {
            //初始化创建影子实例
            _shadowObjectTransform = new GameObject("shadow").transform;
            _shadowObjectTransform.parent = transform;
            _shadowObjectTransform.localPosition = Vector3.zero + shadowOffset;
            _shadowObjectTransform.localRotation = casterTransform.localRotation;
            _shadowObjectTransform.localScale = casterTransform.localScale;

            _shadowRenderer = _shadowObjectTransform.AddComponent<SpriteRenderer>();
            _shadowRenderer.sprite = casterSpriteRenderer.sprite;
            _shadowRenderer.material = shadowMaterial;
            _shadowRenderer.color = shadowColor;
            _shadowRenderer.sortingLayerName = casterSpriteRenderer.sortingLayerName;
            _shadowRenderer.sortingOrder = casterSpriteRenderer.sortingOrder - 1;
        }


        private void LateUpdate()
        {
            UpdateShadowSize();
            UpdateShadowRotation();
            UpdateShadowSprite();
            UpdateShadowColorAlpha();
        }

        //根据影子和实例之前的距离更新影子大小
        private void UpdateShadowSize()
        {
            float disMu =
                Vector3.SqrMagnitude(casterTransform.position -
                                     _shadowObjectTransform.position); //计算影子和实体之间的距离 为了节省性能 不开方
            _shadowObjectTransform.localScale =
                casterTransform.localScale * Mathf.Lerp(1f, 0f, disMu * shadowSizeChangeRate);
        }

        private void UpdateShadowSprite()
        {
            _shadowRenderer.sprite = casterSpriteRenderer.sprite;
            _shadowRenderer.flipX = casterSpriteRenderer.flipX;
        }

        private void UpdateShadowRotation()
        {
            _shadowObjectTransform.localRotation = casterTransform.localRotation;
        }

        private void UpdateShadowColorAlpha()
        {
            _shadowRenderer.color =
                new Color(shadowColor.r, shadowColor.g, shadowColor.b, casterSpriteRenderer.color.a);
        }
    }
}