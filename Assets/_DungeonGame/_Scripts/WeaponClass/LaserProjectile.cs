using System.Collections;
using UnityEngine;
using ZiercCode.ObjectPool;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 激光类射弹
    /// 碰撞默认为矩形碰撞
    /// </summary>
    public abstract class LaserProjectile : BaseProjectile
    {
        [SerializeField]
        protected LineRenderer lineRenderer;

        protected Vector2 CurrentCollisionBox;

        protected RangeDetector RangeDetector = new(15);

        public override void Init(BaseWeapon myWeapon)
        {
            base.Init(myWeapon);

            float duration = 1f / myWeapon.shootSpeed;

            StartCoroutine(LaserAnimation(duration));
        }

        protected override void SyncCollision()
        {
            CurrentCollisionBox.x = myWeapon.shootDistance;
            CurrentCollisionBox.y = myWeapon.projectileSize;
        }


        private IEnumerator LaserAnimation(float duration)
        {
            //初始化lineRenderer
            lineRenderer.startWidth = 0f;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, myTransform.position);
            lineRenderer.SetPosition(1, myTransform.position + (myTransform.right * myWeapon.shootDistance));

            float halfDuration = duration / 2f;
            float timer = 0f;
            float startWidth = myWeapon.projectileSize;

            //伤害检测
            CheckHit();

            //绘制激光
            while (timer < halfDuration)
            {
                timer += Time.deltaTime;
                lineRenderer.startWidth = Mathf.Lerp(0f, startWidth, timer / halfDuration);
                yield return null;
            }

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