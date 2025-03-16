using UnityEngine;
using ZiercCode._DungeonGame.HallScene;
using ZiercCode.Audio;
using ZiercCode.ObjectPool;
using Random = UnityEngine.Random;

namespace ZiercCode._DungeonGame.Weapon.Weapon_LuGerGun
{
    public class BulletShell : MonoBehaviour
    {
        [SerializeField] private AudioClip[] shellDrop; //弹壳掉落的音效
        [SerializeField] private FakeHeight.FakeHeight fakeHeight;
        [SerializeField] private Vector2 colliderSize;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float stayTime; //存在时间
        [SerializeField] private float fadeTime; //淡去时间


        private float _stayTimer;
        private float _fadeTimer;

        private RangeDetect _rangeDetect = new RangeDetect(1);

        private void OnEnable()
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,
                spriteRenderer.color.b, 1f);
            _stayTimer = stayTime;
            _fadeTimer = fadeTime;
        }

        private void FixedUpdate()
        {
            CheckCollisions();
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

        //碰到碰撞体后停止移动
        private void CheckCollisions()
        {
            if (_rangeDetect.DetectInBox(transform.position, colliderSize, transform.rotation.eulerAngles.z))
            {
                fakeHeight.StopGroundMove();
            }
        }

        public void PlayDropSfx()
        {
            AudioPlayer.Instance.PlaySfx(shellDrop[Random.Range(0, shellDrop.Length)]);
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawWireCube(transform.position, colliderSize);
        // }
    }
}