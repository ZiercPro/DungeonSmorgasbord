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
        private WeaponUserComponent _weaponUserComponent;
        private CameraShakeFeedback _cameraShakeFeedback;
        private KnockBackFeedBack _knockBackFeedBack;
        private AutoFlipComponent _autoFlipComponent;
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
            _weaponUserComponent = GetComponent<WeaponUserComponent>();
            _moveComponent = GetComponent<MoveComponent>();
            _dashComponent = GetComponent<DashComponent>();
            _attribute = GetComponent<HeroAttribute>();
            _autoFlipComponent = GetComponent<AutoFlipComponent>();
            _interactHandler = GetComponent<InteractHandler>();
            _heroInputManager = GetComponent<HeroInputManager>();
            _knockBackFeedBack = GetComponent<KnockBackFeedBack>();
            _cameraShakeFeedback = GetComponent<CameraShakeFeedback>();
            _heroAnimationController = GetComponent<HeroAnimationController>();
        }

        private void Start()
        {
            CoinPack.Init(0);
            _health.Initialize(_attribute.maxHealth);
            _moveComponent.SetMoveSpeed(_attribute.moveSpeed);
            _heroInputManager.SetHeroControl(true);
            InitWeapon();
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

        private void InitWeapon()
        {
            _heroInputManager.MousePositionChanging +=
                _weaponUserComponent.SetWeapon();
            _heroInputManager.MouseLeftClickStarted += _weaponUserComponent.OnLeftButtonPressStarted;
            _heroInputManager.MouseLeftClickCanceled += _weaponUserComponent.OnLeftButtonPressCanceled;
            _heroInputManager.MouseLeftClickPerformed += _weaponUserComponent.OnLeftButtonPressed;
            _heroInputManager.MouseRightClickStarted += _weaponUserComponent.OnRightButtonPressStarted;
            _heroInputManager.MouseRightClickPerformed += _weaponUserComponent.OnRightButtonPressed;
            _heroInputManager.MouseRightClickCanceled += _weaponUserComponent.OnRightButtonPressCanceled;
            _heroInputManager.MousePositionChanging += _weaponUserComponent.OnViewPositionChange;
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

        public Dictionary<WeaponType, float> GetWeaponDamageRate()
        {
            return _attribute.weaponDamageRate.ToDictionary();
        }

        public float GetCriticalChance()
        {
            return _attribute.criticalChance;
        }

        public Transform GetWeaponUserTransform()
        {
            return transform;
        }
    }
}