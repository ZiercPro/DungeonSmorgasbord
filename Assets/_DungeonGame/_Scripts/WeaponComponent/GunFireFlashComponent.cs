using DG.Tweening;
using UnityEngine;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.EventBusSystem;
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
        private Transform shootPoint;

        [SerializeField]
        private Vector2 scaleRange;

        [SerializeField]
        private Vector2 rotateRange = new Vector2(0f, 90f);

        private GameObject _flash;

        private float _flashDuration;

        protected void OnEnable()
        {
            EventsGroup.AddListener<WeaponEvent.WeaponFired>(OnFire);
        }

        protected void OnDisable()
        {
            EventsGroup.RemoveAllListener();
        }

        protected override void Awake()
        {
            base.Awake();
            _flash = Instantiate(flashTemplate, transform);
            _flash.transform.position = shootPoint.position;
            _flash.transform.rotation = Quaternion.identity;
            SpriteRenderer flashSpriteRenderer = _flash.GetComponent<SpriteRenderer>();
            flashSpriteRenderer.color = flashColor;
            _flash.SetActive(false);
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
            _flash.SetActive(true);
            _flashDuration = 1f / MyWeapon.fireSpeed;
            _flash.transform.Rotate(_flash.transform.forward, Random.Range(rotateRange.x, rotateRange.y));
            _flash.transform.DOScale(Random.Range(scaleRange.x, scaleRange.y), _flashDuration / 2f).SetEase(Ease.Flash)
                .OnComplete(() =>
                    _flash.transform.DOScale(0f, _flashDuration / 2f).SetEase(Ease.Flash)
                        .OnComplete(() => _flash.gameObject.SetActive(false)));
        }
    }
}