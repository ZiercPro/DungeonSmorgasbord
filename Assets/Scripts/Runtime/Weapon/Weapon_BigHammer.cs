using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Runtime.Damage;
using ZiercCode.Runtime.FeedBack;
using ZiercCode.Runtime.Helper;
using ZiercCode.Runtime.Player;

namespace ZiercCode.Runtime.Weapon
{
    public class Weapon_BigHammer : Weapon
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


        public override void Initialize(Dictionary<WeaponType, float> weaponDamageRate, float criticalChance,
            HeroInputManager heroInputManager)
        {
            base.Initialize(weaponDamageRate, criticalChance, heroInputManager);
            HeroInputManager.MouseLeftClickPerformed += OnAttackStart;
        }

        public override void Disable()
        {
            HeroInputManager.MouseLeftClickPerformed -= OnAttackStart;
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