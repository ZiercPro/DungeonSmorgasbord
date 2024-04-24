using System.Collections.Generic;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.DungeonSmorgasbord.Weapon;
using ZiercCode.Old.Component;
using ZiercCode.Old.Component.Hero;
using ZiercCode.Old.FeedBack;
using ZiercCode.Old.Manager;

namespace ZiercCode.Old.Hero
{
    public class Hero : MonoBehaviour, IDamageable, IWeaponUserBase
    {
        private HeroAnimationController _heroAnimationController;
        private WeaponRotateComponent _weaponRotateComponent;
        private CameraShakeFeedback _cameraShakeFeedback;
        private KnockBackFeedBack _knockBackFeedBack;
        private HeroInputManager _heroInputManager;
        private InteractHandler _interactHandler;
        private AutoFlipComponent _autoFlipComponent;
        private HeroAttribute _attribute;
        private MoveComponent _moveComponent;
        private DashComponent _dashComponent;
        private Health _health;

        public CoinPack CoinPack { get; private set; }

        private void Awake()
        {
            CoinPack = new CoinPack();
            _health = GetComponentInChildren<Health>();
            _moveComponent = GetComponentInChildren<MoveComponent>();
            _dashComponent = GetComponentInChildren<DashComponent>();
            _attribute = GetComponentInChildren<HeroAttribute>();
            _weaponRotateComponent = GetComponentInChildren<WeaponRotateComponent>();
            _autoFlipComponent = GetComponentInChildren<AutoFlipComponent>();
            _interactHandler = GetComponentInChildren<InteractHandler>();
            _heroInputManager = GetComponentInChildren<HeroInputManager>();
            _knockBackFeedBack = GetComponentInChildren<KnockBackFeedBack>();
            _cameraShakeFeedback = GetComponentInChildren<CameraShakeFeedback>();
            _heroAnimationController = GetComponentInChildren<HeroAnimationController>();
        }

        private void Start()
        {
            CoinPack.Init(0);
            _health.Initialize(_attribute.maxHealth);
            _moveComponent.SetMoveSpeed(_attribute.moveSpeed);
            _heroInputManager.SetHeroControl(true);
            _heroInputManager.DashButtonPressedPerformed += _dashComponent.Dash;
            _heroInputManager.InteractButtonPressPerformed += _interactHandler.OnInteractive;
            _heroInputManager.MovementInputPerforming += moveDir => { _moveComponent.Move(moveDir); };
            _heroInputManager.MousePositionChanging += viewPos => { _autoFlipComponent.FaceTo(viewPos); };
            //动画
            _heroInputManager.MovementInputPerforming += moveDir =>
            {
                _heroAnimationController.MoveAnimation(moveDir);
            };
            _health.Dead += _heroAnimationController.DeadAnimation;
            _health.Dead += Dead;
        }


        public void TakeDamage(DamageInfo info)
        {
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
            _moveComponent.Stop();
        }

        public Dictionary<WeaponType, float> WeaponDamageRate { get; }
        public DamageInfo DamageInfo { get; }
    }
}