using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Damage;
using ZiercCode.Old.FeedBack;
using ZiercCode.Old.Hero;

namespace ZiercCode.Old.Weapon
{
    public class Weapon_DiamondGreatSword : Weapon
    {
        private CameraShakeFeedback _cameraShakeFeedback;
        private WeaponColliderCheck _colliderCheck;
        private bool _isAttackBlocked;
        private List<AudioName> _waveAudios;

        protected override void Awake()
        {
            base.Awake();
            _waveAudios = new List<AudioName>();
            _cameraShakeFeedback = GetComponent<CameraShakeFeedback>();
            _colliderCheck = GetComponentInChildren<WeaponColliderCheck>();
        }

        private void Start()
        {
            AnimationEventHandler.AnimationTriggeredPerform += OnColliderCheckStart;
            AnimationEventHandler.AnimationTriggeredEnd += OnColliderCheckEnd;
            AnimationEventHandler.AnimationEnded += OnAttackEnd;
            _waveAudios.Add(AudioName.WeaponWave3);
            _waveAudios.Add(AudioName.WeaponWave4);
            _waveAudios.Add(AudioName.WeaponWave5);
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
            AudioPlayer.Instance.PlayAudiosRandomAsync(_waveAudios);
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
                DamageInfo newInfo = new DamageInfo(FinalDamageAmount, damageType, this.transform);
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