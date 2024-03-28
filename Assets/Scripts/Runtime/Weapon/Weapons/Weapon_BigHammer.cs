using UnityEngine;

namespace Runtime.Weapon.Weapons
{
    using Base;
    using Damage;
    using Helper;
    using Player;
    using FeedBack;
    using WeaponComponent;

    public class Weapon_BigHammer : Base.Weapon
    {
        private CameraShakeFeedback _cameraShakeFeedback;
        private WeaponColliderCheck _colliderCheck;
        private bool _isAttackBlocked;

        protected override void Awake()
        {
            base.Awake();
            _cameraShakeFeedback = GetComponent<CameraShakeFeedback>();
            _colliderCheck = GetComponentInChildren<WeaponColliderCheck>();
        }

        private void Start()
        {
            AnimationEventHandler.AnimationTriggeredPerform += OnColliderCheckStart;
            AnimationEventHandler.AnimationTriggeredEnd += OnColliderCheckEnd;
            AnimationEventHandler.AnimationEnded += OnAttackEnd;
        }


        public override void Initialize(SerDictionary<WeaponType, float> weaponDamageRate, float criticalChance,
            InputManager inputManager)
        {
            base.Initialize(weaponDamageRate, criticalChance, inputManager);
            InputManager.MouseLeftClickPerformed += OnAttackStart;
        }

        public override void Disable()
        {
            InputManager.MouseLeftClickPerformed -= OnAttackStart;
            base.Disable();
        }

        private void OnAttackStart()
        {
            if (_isAttackBlocked) return;
            _isAttackBlocked = true;
            int attackID = Animator.StringToHash("attack");
            Animator.SetTrigger(attackID);
        }

        private void OnColliderCheckStart()
        {
            _colliderCheck.SetCheckActive(true);
            _colliderCheck.TriggerEntered += DoDamage;
        }

        private void OnColliderCheckEnd()
        {
            _colliderCheck.SetCheckActive(false);
            _colliderCheck.TriggerEntered -= DoDamage;
        }

        private void DoDamage(Collider2D c2d)
        {
            DamageCount();
            IDamageable damageable = c2d.GetComponent(typeof(IDamageable)) as IDamageable;
            if (damageable != null)
            {
                DamageInfo damageInfo = new DamageInfo(FinalDamageAmount, damageType, this.transform);
                damageable.TakeDamage(damageInfo);
                _cameraShakeFeedback.StartShake();
            }
        }

        private void OnAttackEnd()
        {
            _isAttackBlocked = false;
        }
    }
}