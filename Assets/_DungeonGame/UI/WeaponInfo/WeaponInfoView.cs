using DG.Tweening;
using RMC.Mini;
using RMC.Mini.View;
using TMPro;
using UnityEngine;
using ZiercCode._DungeonGame._Scripts;

namespace ZiercCode._DungeonGame.UI.WeaponInfo
{
    public class WeaponInfoView : MonoBehaviour, IView
    {
        [SerializeField]
        private float projectileCountTextScaleMagnifyDuration = .1f; //放大时间

        [SerializeField]
        private float projectileCountTextScaleShrinkDuration = .2f; //缩小时间

        [SerializeField]
        private float projectileCountTextScaleShakeRate = 1.2f; //抖动倍率

        /// <summary>
        /// 子弹数量信息
        /// </summary>
        [field: SerializeField]
        public TextMeshProUGUI CurrentMagazineCountText { get; private set; }

        [field: SerializeField]
        public CanvasGroupUser CanvasGroupUser { get; private set; }

        [field: SerializeField]
        public ReloadAnimation ReloadAnimation { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI FireDamageText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI WoodDamageText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI IceDamageText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI VoiceDamageText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ElectricDamageText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI PoisonDamageText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI WindDamageText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI VoidDamageText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI HitForceText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI CriticalChanceText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI CriticalDamageRateText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI TriggerChanceText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI DamageReductionByDistanceText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ShootSpeedText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ShootDistanceText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI MagazineCapacityText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ReloadTimeText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI AccuracyText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ProjectileNumPerShootText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ProjectileSpeedText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI ProjectileSizeText { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI WeaponName { get; private set; }

        private bool _isInitialized;
        private IContext _context;

        private float _projectileCountTextScale;

        public void Initialize(IContext context)
        {
            if (!_isInitialized)
            {
                _context = context;
                _isInitialized = true;

                _projectileCountTextScale = CurrentMagazineCountText.rectTransform.localScale.x;
            }
        }

        public void RequireIsInitialized()
        {
        }

        /// <summary>
        /// 子弹数量ui弹性效果
        /// </summary>
        public void OnProjectileChanged()
        {
            CurrentMagazineCountText.rectTransform.DOScale(
                _projectileCountTextScale * projectileCountTextScaleShakeRate,
                projectileCountTextScaleMagnifyDuration).SetEase(Ease.OutBounce).OnComplete(() =>
                CurrentMagazineCountText
                    .rectTransform
                    .DOScale(_projectileCountTextScale,
                        projectileCountTextScaleShrinkDuration).SetEase(Ease.InFlash)
            );
        }

        public bool IsInitialized => _isInitialized;
        public IContext Context => _context;

        public void Dispose()
        {
        }
    }
}