using UnityEngine;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.EventBusSystem;
using ZiercCode.FakeHeight;
using ZiercCode.ObjectPool;
using ZiercCode.Utilities;
using Random = UnityEngine.Random;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    public class Projectile_YellowBullet : Projectile
    {
        [SerializeField]
        private Vector2 collisionBox;

        [SerializeField]
        private GameObject splitParticle; //子弹命中碎裂粒子

        [SerializeField]
        private Vector2Int splitParticleNum; //粒子数量范围

        [SerializeField]
        private Vector2 splitParticleDirectionRange; //粒子散射范围

        [SerializeField]
        private Vector2 splitParticleGroundVRange; //粒子水平范围

        [SerializeField]
        private Vector2 splitParticleVerticalVRange; //粒子垂直速度范围

        [SerializeField]
        private Vector2 splitParticleRotateVRange; //粒子旋转速度范围

        [SerializeField]
        private string splitParticlePoolName;

        [SerializeField]
        private int splitParticlePoolMinSize;

        [SerializeField]
        private int splitParticlePoolMaxSize;


        private Vector2 currentCollisionBox;

        private RangeDetector _rangeDetect = new(5);


        protected void OnDestroy()
        {
            PoolManager.Instance.Dispose(splitParticlePoolName);
        }

        protected override void Awake()
        {
            base.Awake();
            PoolManager.Instance.Register(splitParticlePoolName, splitParticle, splitParticlePoolMinSize,
                splitParticlePoolMaxSize);
        }


        protected override void PauseUpdate()
        {
            base.PauseUpdate();
            CheckHit();
        }

        protected override void SyncCollision()
        {
            currentCollisionBox = collisionBox * myWeapon.projectileSize;
        }

        //检测碰撞
        private void CheckHit()
        {
            if (_rangeDetect.DetectInBox(transform.position, currentCollisionBox, transform.rotation.eulerAngles.z))
            {
                Collider2D[] hit = _rangeDetect.GetColliders();
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i] && hit[i].TryGetComponent(out IAttackAble attackAble))
                    {
                        EventBus.Invoke(new AttackEvent.WeaponAttack
                        {
                            Weapon = myWeapon, Projectile = this, Target = attackAble
                        });
                    }
                }

                Split();

                PoolManager.Instance.Release(myWeapon.projectilePoolName, gameObject);
            }
        }

        //命中分裂
        private void Split()
        {
            int num = Random.Range(splitParticleNum.x, splitParticleNum.y);
            for (int i = 0; i < num; i++)
            {
                GameObject particle = (GameObject)PoolManager.Instance.Get(splitParticlePoolName);
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
                    0f, .5f);
            }
        }


        // //追踪效果
        // [SerializeField]
        // private Shadow2D shadow;
        //
        // [SerializeField]
        // private FakeHeightTransform fakeHeightTransform;
        //
        // [SerializeField]
        // private float velocityChangeRate;
        //
        // private Transform _target;
        //
        // private void TrackTarget()
        // {
        //     if (!_target)
        //     {
        //         _target = FindObjectOfType<TestStake>().transform;
        //     }
        //
        //
        //     Vector2 targetDir = (_target.position - transform.position).normalized;
        //     Vector2 currentDir = fakeHeightTransform.groundVelocity.normalized;
        //     //直接lerp归一化的方向Vector2 再乘模长
        //     fakeHeightTransform.groundVelocity =
        //         Vector2.Lerp(currentDir, targetDir, velocityChangeRate * Time.deltaTime).normalized *
        //         fakeHeightTransform.groundVelocity.magnitude;
        //
        //     //通过旋转矩阵进行旋转
        //     // float targetAngel = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        //     // float currentAngel = Mathf.Atan2(currentDir.y, currentDir.x) * Mathf.Rad2Deg;
        //     //
        //     // float offset = targetAngel - currentAngel;
        //     //
        //     // offset = Mathf.LerpAngle(0, offset, velocityChangeRate * Time.deltaTime) * Mathf.Deg2Rad;
        //     //
        //     // float cos = Mathf.Cos(offset);
        //     // float sin = Mathf.Sin(offset);
        //     //
        //     // _rotateM.x = cos * fakeHeightTransform.groundVelocity.x - sin * fakeHeightTransform.groundVelocity.y;
        //     // _rotateM.y = sin * fakeHeightTransform.groundVelocity.x + cos * fakeHeightTransform.groundVelocity.y;
        //     // fakeHeightTransform.groundVelocity = _rotateM;
        //
        //
        //     float angle = Mathf.Atan2(fakeHeightTransform.groundVelocity.y, fakeHeightTransform.groundVelocity.x) *
        //                   Mathf.Rad2Deg;
        //     Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //     shadow.CasterTransform.rotation = rotation;
        // }
//无法旋转绘制 有误导性
// #if UNITY_EDITOR
//         private void OnDrawGizmos()
//         {
//             Gizmos.color = Color.yellow;
//             Gizmos.DrawWireCube(transform.position, currentCollisionBox);
//         }
// #endif
    }
}