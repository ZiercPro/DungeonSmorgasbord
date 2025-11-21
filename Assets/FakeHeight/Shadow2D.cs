using Unity.VisualScripting;
using UnityEngine;
using ZiercCode.Management;

namespace ZiercCode.FakeHeight
{
    /// <summary>
    /// 自动绘制2D阴影
    /// </summary>
    public class Shadow2D : PauseBehaviour
    {
        [SerializeField]
        private Material shadowMaterial; //用于使阴影为纯色

        [SerializeField]
        private Color shadowColor;

        [SerializeField]
        private Vector3 shadowVisualOffset = new(0.05f, -.05f, 0f); //阴影与物体的位置视觉偏差 不影响触地判断

        [SerializeField]
        private float shadowSizeChangeRate = .02f; //影子大小随本体距离变化率

        [SerializeField]
        private Transform casterTransform; //本体

        [SerializeField]
        private SpriteRenderer casterSpriteRenderer;

        [SerializeField]
        private bool updateShadowSize = true;

        [SerializeField]
        private bool updateShadowSprite = true;

        [SerializeField]
        private bool updateShadowRotation = true;

        [SerializeField]
        private bool updateShadowAlpha = true;

        [SerializeField]
        private bool updateShadowRelativePosition = true;


        private SpriteRenderer _shadowRenderer;
        private Transform _shadowObjectTransform;

        public Transform ShadowObjectTransform => _shadowObjectTransform;
        public Vector3 ShadowVisualOffset => shadowVisualOffset;

        private void Awake()
        {
            //初始化创建影子实例
            _shadowObjectTransform = new GameObject("shadow").transform;
            _shadowObjectTransform.parent = transform;
            _shadowObjectTransform.localPosition = shadowVisualOffset;
            _shadowObjectTransform.localRotation = casterTransform.localRotation;
            _shadowObjectTransform.localScale = casterTransform.localScale;

            _shadowRenderer = _shadowObjectTransform.AddComponent<SpriteRenderer>();
            _shadowRenderer.sprite = casterSpriteRenderer.sprite;
            _shadowRenderer.material = shadowMaterial;
            _shadowRenderer.color = shadowColor;
            _shadowRenderer.sortingLayerName = casterSpriteRenderer.sortingLayerName;
            _shadowRenderer.sortingOrder = casterSpriteRenderer.sortingOrder - 1;
        }

        protected override void PauseLateUpdate()
        {
            UpdateShadowSize();
            UpdateShadowRotation();
            UpdateShadowSprite();
            UpdateShadowAlpha();
            UpdateShadowRelativePosition();
        }

        //根据影子和实例之前的距离更新影子大小
        private void UpdateShadowSize()
        {
            if (!updateShadowSize)
            {
                return;
            }

            float disMu = casterTransform.position.y - transform.position.y;
            _shadowObjectTransform.localScale =
                casterTransform.localScale * Mathf.Lerp(1f, 0f, disMu * shadowSizeChangeRate);
        }

        private void UpdateShadowSprite()
        {
            if (!updateShadowSprite)
            {
                return;
            }

            _shadowRenderer.sprite = casterSpriteRenderer.sprite;
            _shadowRenderer.flipX = casterSpriteRenderer.flipX;
            _shadowRenderer.flipY = casterSpriteRenderer.flipY;
        }

        private void UpdateShadowRotation()
        {
            if (!updateShadowRotation)
            {
                return;
            }

            _shadowObjectTransform.localRotation = casterTransform.localRotation;
        }

        private void UpdateShadowAlpha()
        {
            if (!updateShadowAlpha)
            {
                return;
            }

            _shadowRenderer.color =
                new Color(shadowColor.r, shadowColor.g, shadowColor.b, casterSpriteRenderer.color.a);
        }

        private void UpdateShadowRelativePosition()
        {
            if (!updateShadowRelativePosition)
            {
                return;
            }

            _shadowObjectTransform.position = transform.position + shadowVisualOffset;
        }
    }
}