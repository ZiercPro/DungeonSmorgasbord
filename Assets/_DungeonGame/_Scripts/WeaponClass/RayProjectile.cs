using System.Collections;
using UnityEngine;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.EventBusSystem;
using ZiercCode.ObjectPool;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 射线类射弹
    /// </summary>
    public abstract class RayProjectile : BaseProjectile
    {
        [SerializeField]
        private LineRenderer lineRenderer;

        private Vector2 _currentCollisionBox;
        private RangeDetector _rangeDetector = new RangeDetector(15);

        public override void Init(BaseWeapon myWeapon)
        {
            base.Init(myWeapon);

            float duration = 1f / myWeapon.fireSpeed;

            StartCoroutine(LaserAnimation(duration));
        }

        protected override void SyncCollision()
        {
            _currentCollisionBox.x = myWeapon.projectileMaxDistance;
            _currentCollisionBox.y = myWeapon.projectileSize;
        }


        private IEnumerator LaserAnimation(float duration)
        {
            //初始化lineRenderer
            lineRenderer.startWidth = 0f;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + transform.right * myWeapon.projectileMaxDistance);

            float halfDuration = duration / 2f;
            float timer = 0f;
            float startWidth = myWeapon.projectileSize;

            //伤害检测
            //todo 是否使用raydetect
            if (_rangeDetector.DetectInBox(transform.position + transform.right * _currentCollisionBox.x / 2f,
                    _currentCollisionBox, transform.rotation.eulerAngles.z))
            {
                Collider2D[] hit = _rangeDetector.GetColliders();
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
            }

            //绘制激光
            while (timer < halfDuration)
            {
                timer += Time.deltaTime;
                lineRenderer.startWidth = Mathf.Lerp(0f, startWidth, timer / halfDuration);
                yield return null;
            }

            transform.localPosition = Vector2.zero;
            timer = halfDuration;
            while (timer > 0f)
            {
                timer -= Time.deltaTime;
                lineRenderer.startWidth = Mathf.Lerp(0f, startWidth, timer / halfDuration);
                yield return null;
            }

            lineRenderer.startWidth = 0f;
            lineRenderer.positionCount = 0;

            PoolManager.Instance.Release(myWeapon.projectilePoolName, gameObject);
        }
    }
}