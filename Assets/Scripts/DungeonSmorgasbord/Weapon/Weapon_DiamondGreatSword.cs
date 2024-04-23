using System.Collections.Generic;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.Audio;
using ZiercCode.Old.FeedBack;

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
            WaveAudios = new List<AudioName>();
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
            throw new System.NotImplementedException();
        }

        public override void OnLeftButtonPressCanceled()
        {
            throw new System.NotImplementedException();
        }

        public override void OnRightButtonPressStarted()
        {
            throw new System.NotImplementedException();
        }

        public override void OnRightButtonPressing()
        {
            throw new System.NotImplementedException();
        }

        public override void OnRightButtonPressCanceled()
        {
            throw new System.NotImplementedException();
        }

        private void OnAttackStart()
        {
            if (_isAttackBlocked) return;
            _isAttackBlocked = true;
            int attackID = Animator.StringToHash("attack");
            Animator.SetTrigger(attackID);
            AudioPlayer.Instance.PlayAudiosRandomAsync(WaveAudios);
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
            //伤害信息
            IDamageable damageable = c2d.GetComponent(typeof(IDamageable)) as IDamageable;
            if (damageable != null)
            {
                DamageInfo newInfo = new DamageInfo(FinalDamageAmount, WeaponDataSo.DamageType, transform);
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