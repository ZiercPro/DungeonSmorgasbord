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
        public TextMeshProUGUI ProjectileCountText { get; private set; }

        [field: SerializeField]
        public CanvasGroupUser CanvasGroupUser { get; private set; }

        [field: SerializeField]
        public ReloadAnimation ReloadAnimation { get; private set; }

        private bool _isInitialized;
        private IContext _context;

        private float _projectileCountTextScale;

        public void Initialize(IContext context)
        {
            if (!_isInitialized)
            {
                _context = context;
                _isInitialized = true;

                _projectileCountTextScale = ProjectileCountText.rectTransform.localScale.x;
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
            ProjectileCountText.rectTransform.DOScale(_projectileCountTextScale * projectileCountTextScaleShakeRate,
                projectileCountTextScaleMagnifyDuration).SetEase(Ease.OutBounce).OnComplete(() => ProjectileCountText
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