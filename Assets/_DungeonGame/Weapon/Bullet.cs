using UnityEngine;
using ZiercCode._DungeonGame.HallScene;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.ObjectPool;
using Random = UnityEngine.Random;

namespace ZiercCode._DungeonGame.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float colliderRadius;
        private RangeDetect _rangeDetect = new RangeDetect(5);

        [SerializeField] private float stayTime;

        [SerializeField] private GameObject splitParticle; //子弹命中碎裂粒子
        [SerializeField] private Vector2Int splitParticleNum; //粒子数量范围
        [SerializeField] private Vector2 splitParticleDirectionRange; //粒子散射范围
        [SerializeField] private Vector2 splitParticleGroundVRange; //粒子水平范围
        [SerializeField] private Vector2 splitParticleVerticalVRange; //粒子垂直速度范围
        [SerializeField] private Vector2 splitParticleRotateVRange; //粒子旋转速度范围

        //[SerializeField] private AudioClip[] hitSfx; //命中音效

        private float _stayTimer;

        private void Awake()
        {
            PoolManager.Instance.Register("splitBullet", splitParticle);
        }

        private void OnEnable()
        {
            _stayTimer = stayTime;
        }

        private void FixedUpdate()
        {
            if (_rangeDetect.DetectInCircle(transform.position, colliderRadius))
            {
                Collider2D[] hit = _rangeDetect.GetColliders();
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i])
                    {
                        DamageInfo info = new DamageInfo(1, DamageType.Physics, transform);
                        if (hit[i].TryGetComponent(out IHitAble hitAble))
                            hitAble.GetHit(transform.right);
                        if (hit[i].TryGetComponent(out IDamageable damageable))
                            damageable.TakeDamage(info);
                    }
                }

                Split();
                //PlayHitSfx();

                PoolManager.Instance.Release("bullet", gameObject);
            }
        }

        private void Update()
        {
            AutoRelease();
        }

        // private void PlayHitSfx()
        // {
        //     AudioPlayer.Instance.PlaySfx(hitSfx[Random.Range(0, hitSfx.Length)], .5f);
        // }

        //命中分裂
        private void Split()
        {
            int num = Random.Range(splitParticleNum.x, splitParticleNum.y);
            for (int i = 0; i < num; i++)
            {
                GameObject particle = (GameObject)PoolManager.Instance.Get("splitBullet");
                particle.SetActive(true);
                particle.transform.position = transform.position;
                particle.transform.rotation = transform.rotation;

                float angle = Random.Range(splitParticleDirectionRange.x, splitParticleDirectionRange.y);
                particle.transform.Rotate(particle.transform.forward, angle);

                FakeHeight.FakeHeight fakeHeight = particle.GetComponent<FakeHeight.FakeHeight>();
                fakeHeight.Init(
                    -particle.transform.right *
                    Random.Range(splitParticleGroundVRange.x, splitParticleGroundVRange.y),
                    Random.Range(splitParticleVerticalVRange.x, splitParticleVerticalVRange.y), true,
                    Random.Range(splitParticleRotateVRange.x, splitParticleRotateVRange.y));
            }
        }

        private void AutoRelease()
        {
            if (_stayTimer > 0)
            {
                _stayTimer -= Time.deltaTime;
            }
            else
            {
                PoolManager.Instance.Release("bullet", gameObject);
            }
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawWireSphere(transform.position, colliderRadius);
        // }
    }
}