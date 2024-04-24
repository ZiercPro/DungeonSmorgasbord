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
            animationEventHandler.AnimationTriggeredPerform += OnColliderCheckStart;
            animationEventHandler.AnimationTriggeredEnd += OnColliderCheckEnd;
            animationEventHandler.AnimationEnded += OnAttackEnd;
        }

        public override void OnLeftButtonPressStarted()
        {
            OnAttackStart();
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
            _colliderCheck.Enable();
            _colliderCheck.TriggerEntered += DoDamage;
        }

        private void OnColliderCheckEnd()
        {
            _colliderCheck.Disable();
            _colliderCheck.TriggerEntered -= DoDamage;
        }

        private void DoDamage(Collider2D c2d)
        {
            IDamageable damageable = c2d.GetComponent(typeof(IDamageable)) as IDamageable;
            if (damageable != null)
            {
                DamageInfo damageInfo = GetDamageInfo();
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