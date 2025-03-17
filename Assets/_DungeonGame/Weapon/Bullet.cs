using UnityEngine;
using ZiercCode._DungeonGame.HallScene;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.FakeHeight;
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

        //track
        [SerializeField] private Shadow2D shadow;
        [SerializeField] private FakeHeightTransform fakeHeightTransform;

        [SerializeField] private float velocityChangeRate;

        //private Vector2 _rotateM = new Vector2(0f, 0f);
        private Transform _target;

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

            // TrackTarget();
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

                FakeHeightTransform fakeHeightTransform = particle.GetComponent<FakeHeightTransform>();
                fakeHeightTransform.Init(
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

        private void TrackTarget()
        {
            if (!_target)
                _target = FindObjectOfType<TestStake>().transform;


            Vector2 targetDir = (_target.position - transform.position).normalized;
            Vector2 currentDir = fakeHeightTransform.groundVelocity.normalized;
            //直接lerp归一化的方向Vector2 再乘模长
            fakeHeightTransform.groundVelocity =
                Vector2.Lerp(currentDir, targetDir, velocityChangeRate * Time.deltaTime).normalized *
                fakeHeightTransform.groundVelocity.magnitude;

            //通过旋转矩阵进行旋转
            // float targetAngel = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            // float currentAngel = Mathf.Atan2(currentDir.y, currentDir.x) * Mathf.Rad2Deg;
            //
            // float offset = targetAngel - currentAngel;
            //
            // offset = Mathf.LerpAngle(0, offset, velocityChangeRate * Time.deltaTime) * Mathf.Deg2Rad;
            //
            // float cos = Mathf.Cos(offset);
            // float sin = Mathf.Sin(offset);
            //
            // _rotateM.x = cos * fakeHeightTransform.groundVelocity.x - sin * fakeHeightTransform.groundVelocity.y;
            // _rotateM.y = sin * fakeHeightTransform.groundVelocity.x + cos * fakeHeightTransform.groundVelocity.y;
            // fakeHeightTransform.groundVelocity = _rotateM;


            float angle = Mathf.Atan2(fakeHeightTransform.groundVelocity.y, fakeHeightTransform.groundVelocity.x) *
                          Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            shadow.CasterTransform.rotation = rotation;
        }

        // private void OnDrawGizmos()
        // {
        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawWireSphere(transform.position, colliderRadius);
        // }
    }
}