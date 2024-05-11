using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Core.Extend;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.DungeonSmorgasbord.Extend;
using ZiercCode.DungeonSmorgasbord.Weapon;
using ZiercCode.Old.Component;
using ZiercCode.Old.Component.Hero;
using ZiercCode.Old.FeedBack;

namespace ZiercCode.Old.Hero
{
    public class Hero : MonoBehaviour, IDamageable, IWeaponUserBase
    {
        private WeaponUser _weaponUser;
        private CameraShakeFeedback _cameraShakeFeedback;
        private KnockBackFeedBack _knockBackFeedBack;
        private AutoFlipComponent _autoFlipComponent;
        private AnimationHandler _animationHandler;
        private HeroInputManager _heroInputManager;
        private InteractHandler _interactHandler;
        private HeroAttribute _attribute;
        private MoveComponent _moveComponent;
        private DashComponent _dashComponent;
        private Health _health;

        public CoinPack CoinPack { get; private set; }

        private void Awake()
        {
            CoinPack = new CoinPack();
            _health = GetComponent<Health>();
            _weaponUser = GetComponent<WeaponUser>();
            _moveComponent = GetComponent<MoveComponent>();
            _dashComponent = GetComponent<DashComponent>();
            _attribute = GetComponent<HeroAttribute>();
            _autoFlipComponent = GetComponent<AutoFlipComponent>();
            _interactHandler = GetComponent<InteractHandler>();
            _heroInputManager = GetComponent<HeroInputManager>();
            _knockBackFeedBack = GetComponent<KnockBackFeedBack>();
            _cameraShakeFeedback = GetComponent<CameraShakeFeedback>();
            _animationHandler = GetComponent<AnimationHandler>();
        }

        private void Start()
        {
            CoinPack.Init(0);
            _moveComponent.SetMoveSpeed(_attribute.AttributesData.moveSpeed);
            _heroInputManager.SetHeroControl(true);
            InitWeapon();
            _heroInputManager.DashButtonPressedPerformed += _dashComponent.Dash;
            _heroInputManager.InteractButtonPressPerformed += _interactHandler.OnInteractive;
            _heroInputManager.MovementInputPerforming += moveDir => { _moveComponent.Move(moveDir); };
            _heroInputManager.MousePositionChanging += viewPos => { _autoFlipComponent.FaceTo(viewPos); };
            //动画
            _heroInputManager.MovementInputPerforming += moveDir =>
            {
                if (moveDir.sqrMagnitude > 0)
                    _animationHandler.ActiveAnimationFunc(3);
                else
                    _animationHandler.ActiveAnimationFunc(2);
            };
            _health.Dead += Dead;
        }

        private void InitWeapon()
        {
            _heroInputManager.MousePositionChanging += _weaponUser.SetWeapon();
            _heroInputManager.MouseLeftClickStarted += _weaponUser.OnLeftButtonPressStarted;
            _heroInputManager.MouseLeftClickCanceled += _weaponUser.OnLeftButtonPressCanceled;
            _heroInputManager.MouseLeftClickPerformed += _weaponUser.OnLeftButtonPressed;
            _heroInputManager.MouseRightClickStarted += _weaponUser.OnRightButtonPressStarted;
            _heroInputManager.MouseRightClickPerformed += _weaponUser.OnRightButtonPressed;
            _heroInputManager.MouseRightClickCanceled += _weaponUser.OnRightButtonPressCanceled;
            _heroInputManager.MousePositionChanging += _weaponUser.OnViewPositionChange;
        }

        public void TakeDamage(DamageInfo info)
        {
            if (_health.IsDead) return;
            int healthValue = _health.GetCurrentHealth() - info.damageAmount;
            _health.SetCurrentHealth(healthValue, Health.HealthChangeType.Damage);
            //听觉反馈

            //视觉反馈
            _cameraShakeFeedback.StartShake();
            _animationHandler.ActiveAnimationFunc(1);
        }


        public void Dead()
        {
            _heroInputManager.SetHeroControl(false);
            _knockBackFeedBack.enabled = false;
            _moveComponent.Stop();
            
            _animationHandler.ActiveAnimationFunc(0);
        }

        public Dictionary<WeaponType, float> GetWeaponDamageRate()
        {
            return _attribute.AttributesData.weaponDamageRate.ToDictionary();
        }

        public float GetCriticalChance()
        {
            return _attribute.AttributesData.criticalChance;
        }

        public Transform GetWeaponUserTransform()
        {
            return transform;
        }
    }
}