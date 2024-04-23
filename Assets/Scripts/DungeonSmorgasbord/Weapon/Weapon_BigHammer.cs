using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.FeedBack;

namespace ZiercCode.DungeonSmorgasbord.Weapon
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

        public override void OnLeftButtonPressStarted()
        {
            OnAttackStart();
        }

        public override void OnLeftButtonPressing()
        {
        }

        public override void OnLeftButtonPressCanceled()
        {
        }

        public override void OnRightButtonPressStarted()
        {
        }

        public override void OnRightButtonPressing()
        {
        }

        public override void OnRightButtonPressCanceled()
        {
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
                DamageInfo damageInfo = new(FinalDamageAmount, WeaponDataSo.DamageType, transform);
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