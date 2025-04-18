using UnityEngine;
using ZiercCode.ObjectPool;

namespace ZiercCode._DungeonGame._Scripts.Juice
{
    public class BulletShell : MonoBehaviour //弹壳
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private float stayTime; //存在时间

        [SerializeField]
        private float fadeTime; //淡去时间


        private float _stayTimer;
        private float _fadeTimer;

        private void OnEnable()
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                spriteRenderer.color.b, 1f);
            _stayTimer = stayTime;
            _fadeTimer = fadeTime;
        }

        private void Update()
        {
            AutoRelease();
        }

        private void AutoRelease()
        {
            if (_stayTimer > 0)
            {
                _stayTimer -= Time.deltaTime;
            }
            else
            {
                if (_fadeTimer > 0)
                {
                    _fadeTimer -= Time.deltaTime;
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                        spriteRenderer.color.b, _fadeTimer / fadeTime);
                }
                else
                {
                    PoolManager.Instance.Release("shellCase", gameObject);
                }
            }
        }
    }
}