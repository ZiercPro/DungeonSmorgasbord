using System;
using UnityEngine;
using ZiercCode.Core.UI;
using ZiercCode.Old.Component;
using ZiercCode.Old.Component.Hero;
using ZiercCode.Old.Damage;
using ZiercCode.Old.FeedBack;
using ZiercCode.Old.Manager;
using ZiercCode.Old.UI.Panel;
using ZiercCode.Old.Weapon;

namespace ZiercCode.Old.Hero
{
    public class Hero : MonoBehaviour, IDamageable
    {
        private HeroAnimationController _heroAnimationController;
        private CameraShakeFeedback _cameraShakeFeedback;
        private KnockBackFeedBack _knockBackFeedBack;
        private HeroWeaponHandler _heroWeaponHandler;
        private HeroInputManager _heroInputManager;
        private InteractHandler _interactHandler;
        private FlipController _flipController;
        private SpriteRenderer _spriteRenderer;
        private PanelManager _panelManager;
        private WeaponHolder _weaponHolder;
        private HeroAttribute _attribute;
        private Movement _movement;
        private HeroDash _heroDash;
        private Health _health;

        public CoinPack CoinPack { get; private set; }

        private void Awake()
        {
            CoinPack = new CoinPack();
            _panelManager = new PanelManager();
            _health = GetComponentInChildren<Health>();
            _movement = GetComponentInChildren<Movement>();
            _heroDash = GetComponentInChildren<HeroDash>();
            _attribute = GetComponentInChildren<HeroAttribute>();
            _weaponHolder = GetComponentInChildren<WeaponHolder>();
            _flipController = GetComponentInChildren<FlipController>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _interactHandler = GetComponentInChildren<InteractHandler>();
            _heroInputManager = GetComponentInChildren<HeroInputManager>();
            _heroWeaponHandler = GetComponentInChildren<HeroWeaponHandler>();
            _knockBackFeedBack = GetComponentInChildren<KnockBackFeedBack>();
            _cameraShakeFeedback = GetComponentInChildren<CameraShakeFeedback>();
            _heroAnimationController = GetComponentInChildren<HeroAnimationController>();
        }

        private void Start()
        {
            CoinPack.Init(0);
            _heroWeaponHandler.GetDefualtWeapon();
            _health.Initialize(_attribute.maxHealth);
            _movement.Initialize(_attribute.moveSpeed);
            _weaponHolder.Initialize(_spriteRenderer, _flipController);
            _heroInputManager.SetHeroControl(true);
            _heroInputManager.DashButtonPressedPerformed += _heroDash.StartDash;
            _heroInputManager.InteractButtonPressPerformed += _interactHandler.OnInteractive;
            _heroInputManager.MovementInputPerforming += moveDir => { _movement.MovePerform(moveDir); };
            _heroInputManager.MousePositionChanging += viewPos => { _flipController.FaceTo(viewPos); };
            _heroInputManager.MousePositionChanging += viewPos => { _weaponHolder.WeaponRotateTo(viewPos); };
            //动画
            _heroInputManager.MovementInputPerforming += moveDir =>
            {
                _heroAnimationController.MoveAnimation(moveDir);
            };
            OnTakeDamage += damageInfo => { _heroAnimationController.HitAnimation(); };
            _health.Dead += _heroAnimationController.DeadAnimation;
            _health.Dead += Dead;
        }

        public event Action<DamageInfo> OnTakeDamage;

        public void TakeDamage(DamageInfo info)
        {
            OnTakeDamage?.Invoke(info);
            _health.SetCurrent(current =>
            {
                current -= info.damageAmount;
                return current;
            });
            //听觉反馈

            //视觉反馈
            TextPopupSpawner.Instance.InitPopupText(transform.position, Color.red, "-" + info.damageAmount);
            _cameraShakeFeedback.StartShake();
            _knockBackFeedBack.StartBackMove(info);
        }


        public void Dead()
        {
            _knockBackFeedBack.enabled = false;
            _heroInputManager.SetHeroControl(false);
            _movement.StopMovePerform();
        }
    }
}