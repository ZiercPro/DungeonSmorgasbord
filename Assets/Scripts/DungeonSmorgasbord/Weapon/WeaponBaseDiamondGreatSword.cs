using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using ZiercCode.Old.Audio;
using ZiercCode.Old.FeedBack;
using ZiercCode.DungeonSmorgasbord.Damage;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public class WeaponBaseDiamondGreatSword : WeaponBase
    {
        [SerializeField] private CameraShakeFeedback cameraShakeFeedback;
        [SerializeField] private WeaponColliderCheck colliderCheck;
        private bool _isAttackBlocked;

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
            GetAnimator().SetTrigger(attackID);
            AudioPlayer.Instance.PlayAudiosRandomAsync(waveAudios);
        }

        private void OnColliderCheckStart()
        {
            colliderCheck.Enable();
            colliderCheck.TriggerEntered += DoDamage;
        }

        private void OnColliderCheckEnd()
        {
            colliderCheck.Disable();
            colliderCheck.TriggerEntered -= DoDamage;
        }

        private void DoDamage(Collider2D c2d)
        {
            if (c2d.transform == GetWeaponUser().GetWeaponUser()) return;
            if (c2d.GetComponent(typeof(IDamageable)) is IDamageable damageable)
            {
                DamageInfo newInfo = GetDamageInfo();
                damageable.TakeDamage(newInfo);
                cameraShakeFeedback.StartShake();
            }
        }

        private void OnAttackEnd()
        {
            _isAttackBlocked = false;
        }
    }
}