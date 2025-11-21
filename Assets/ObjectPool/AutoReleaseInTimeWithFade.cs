using UnityEngine;

namespace ZiercCode.ObjectPool
{
    public class AutoReleaseInTimeWithFade : AutoReleaseInTime
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private float fadeTime; //淡去时间

        private float _fadeTimer;
        private float _solidTimer;

        protected override void OnEnable()
        {
            base.OnEnable();
            _fadeTimer = fadeTime;
            _solidTimer = stayTime - fadeTime;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                spriteRenderer.color.b, 1f);
        }

        protected override void Update()
        {
            base.Update();
            Fade();
        }

        private void Fade()
        {
            if (_solidTimer > 0f)
            {
                _solidTimer -= Time.deltaTime;
            }
            else
            {
                if (_fadeTimer > 0f)
                {
                    _fadeTimer -= Time.deltaTime;
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                        spriteRenderer.color.b, _fadeTimer / fadeTime);
                }
            }
        }
    }
}