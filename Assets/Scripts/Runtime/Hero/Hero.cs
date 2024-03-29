using Runtime.FeedBack;
using Runtime.Weapon.Base;
using System;
using UnityEngine;


namespace Runtime.Hero
{
    using Damage;
    using Player;
    using Manager;
    using Component.Hero;
    using Component.Base;

    public class Hero : MonoBehaviour, IDamageable
    {
        private HeroAnimationController _heroAnimationController;
        private CameraShakeFeedback _cameraShakeFeedback;
        private KnockBackFeedBack _knockBackFeedBack;
        private HeroWeaponHandler _heroWeaponHandler;
        private InteractHandler _interactHandler;
        private FlipController _flipController;
        private SpriteRenderer _spriteRenderer;
        private InputManager _inputManager;
        private WeaponHolder _weaponHolder;
        private HeroAttribute _attribute;
        private Movement _movement;
        private HeroDash _heroDash;
        private Health _health;

        public CoinPack CoinPack { get; private set; }

        protected virtual void Awake()
        {
            CoinPack = new CoinPack();
            _health = GetComponentInChildren<Health>();
            _movement = GetComponentInChildren<Movement>();
            _heroDash = GetComponentInChildren<HeroDash>();
            _attribute = GetComponentInChildren<HeroAttribute>();
            _inputManager = GetComponentInChildren<InputManager>();
            _weaponHolder = GetComponentInChildren<WeaponHolder>();
            _flipController = GetComponentInChildren<FlipController>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _interactHandler = GetComponentInChildren<InteractHandler>();
            _heroWeaponHandler = GetComponentInChildren<HeroWeaponHandler>();
            _knockBackFeedBack = GetComponentInChildren<KnockBackFeedBack>();
            _cameraShakeFeedback = GetComponentInChildren<CameraShakeFeedback>();
            _heroAnimationController = GetComponentInChildren<HeroAnimationController>();
        }

        protected virtual void Start()
        {
            //让gamepanel显示金币数量
            CoinPack.GetCoins(0);

            _attribute.Initialize();
            _heroWeaponHandler.GetDefualtWeapon();
            _health.Initialize(_attribute.maxHealth);
            _movement.Initialize(_attribute.moveSpeed);
            _weaponHolder.Initialize(_spriteRenderer, _flipController);

            _inputManager.DashButtonPressedPerformed += _heroDash.StartDash;
            _inputManager.InteractButtonPressPerformed += _interactHandler.OnInteractive;
            _inputManager.MovementInputPeforming += moveDir => { _movement.MovePerform(moveDir); };
            _inputManager.MousePositionChanging += viewPos => { _flipController.FaceTo(viewPos); };
            _inputManager.MousePositionChanging += viewPos => { _weaponHolder.WeaponRotateTo(viewPos); };
            //动画
            _inputManager.MovementInputPeforming += moveDir => { _heroAnimationController.MoveAnimation(moveDir); };
            OnTakeDamage += damageInfo => { _heroAnimationController.HitAnimation(); };
            _health.Dead += _heroAnimationController.DeadAnimation;
            _health.Dead += Dead;
        }

        public HeroAttribute GetAttribute()
        {
            return _attribute;
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
            _inputManager.enabled = false;
            _movement.StopMovePerform();
            // enabled = false;
        }
    }
}