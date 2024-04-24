using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Old.Audio;
using ZiercCode.Old.FeedBack;
using ZiercCode.DungeonSmorgasbord.Damage;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public class Weapon_DiamondGreatSword : Weapon
    {
        private CameraShakeFeedback _cameraShakeFeedback;
        private WeaponColliderCheck _colliderCheck;
        private bool _isAttackBlocked;


        protected override void Awake()
        {
            base.Awake();
            waveAudios = new List<AudioName>();
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
            AudioPlayer.Instance.PlayAudiosRandomAsync(waveAudios);
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
            if (c2d.GetComponent(typeof(IDamageable)) is IDamageable damageable)
            {
                DamageInfo newInfo = GetDamageInfo();
                damageable.TakeDamage(newInfo);
                _cameraShakeFeedback.StartShake();
            }
        }

        private void OnAttackEnd()
        {
            _isAttackBlocked = false;
        }
    }
}