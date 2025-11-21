using DG.Tweening;
using NaughtyAttributes.Scripts.Core.MetaAttributes;
using UnityEngine;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.EventBusSystem;
using ZiercCode.ObjectPool;
using Random = UnityEngine.Random;

namespace ZiercCode._DungeonGame._Scripts.WeaponComponent
{
    /// <summary>
    /// 枪类武器的开火枪口火焰效果
    /// </summary>
    public class GunFireFlashComponent : BaseWeaponComponent
    {
        [SerializeField]
        private Color flashColor = Color.white;

        [SerializeField]
        private GameObject flashTemplate;

        [SerializeField]
        private string poolName;

        [SerializeField]
        private int poolMin;

        [SerializeField]
        private int poolMax;

        [SerializeField]
        private Transform shootPoint;

        [SerializeField]
        private Vector2 scaleRange;

        [SerializeField]
        private bool flashByShootSpeed; //由射击速度决定闪烁速度

        [SerializeField]
        [HideIf("flashByShootSpeed")]
        private float fixedDuration;

        [SerializeField]
        private Vector2 rotateRange = new(0f, 90f);

        private float _flashDuration;

        protected void OnEnable()
        {
            EventsGroup.AddListener<WeaponEvent.WeaponFired>(OnFire);
        }

        protected void OnDisable()
        {
            EventsGroup.RemoveAllListener();
        }

        protected void OnDestroy()
        {
            PoolManager.Instance.Dispose(poolName);
        }

        protected override void Awake()
        {
            base.Awake();
            PoolManager.Instance.Register(poolName, flashTemplate, poolMin, poolMax);
        }

        public void OnFire(IEventArgs args)
        {
            if (args is WeaponEvent.WeaponFired weaponFire)
            {
                if (weaponFire.Weapon == MyWeapon)
                {
                    DoFlash();
                }
            }
        }

        public void DoFlash()
        {
            //没法优化 因为要读取新的参数
            GameObject flash = (GameObject)PoolManager.Instance.Get(poolName);
            flash.transform.position = shootPoint.position;
            flash.transform.rotation = Quaternion.identity;
            flash.transform.localScale = Vector3.zero;
            SpriteRenderer flashSpriteRenderer = flash.GetComponent<SpriteRenderer>();
            flashSpriteRenderer.color = flashColor;
            if (flashByShootSpeed)
            {
                _flashDuration = 1f / MyWeapon.shootSpeed;
            }
            else
            {
                _flashDuration = fixedDuration;
            }

            flash.transform.Rotate(flash.transform.forward, Random.Range(rotateRange.x, rotateRange.y));
            flash.transform.DOScale(Random.Range(scaleRange.x, scaleRange.y), _flashDuration / 2f).SetEase(Ease.Flash)
                .OnComplete(() =>
                    flash.transform.DOScale(0f, _flashDuration / 2f).SetEase(Ease.Flash)
                        .OnComplete(() => PoolManager.Instance.Release(poolName, flash)));
        }
    }
}