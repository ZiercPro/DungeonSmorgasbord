using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Weapon.Weapons
{
    public class Weapon_DiamondGreatSword : global::Runtime.Weapon.Base.Weapon
    {
        private CameraShakeFeedback _cameraShakeFeedback;
        private WeaponColliderCheck _colliderCheck;
        private bool _isAttackBlocked;
        private List<AudioBase> _waveAudios;

        protected override void Awake()
        {
            base.Awake();
            _waveAudios = new List<AudioBase>();
            _cameraShakeFeedback = GetComponent<CameraShakeFeedback>();
            _colliderCheck = GetComponentInChildren<WeaponColliderCheck>();
        }

        private void Start()
        {
            AnimationEventHandler.AnimationTriggeredPerform += OnColliderCheckStart;
            AnimationEventHandler.AnimationTriggeredEnd += OnColliderCheckEnd;
            AnimationEventHandler.AnimationEnded += OnAttackEnd;
            _waveAudios.Add(GameRoot.Instance.AudioList.weaponWave_3);
            _waveAudios.Add(GameRoot.Instance.AudioList.weaponWave_4);
            _waveAudios.Add(GameRoot.Instance.AudioList.weaponWave_5);
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
            AudioPlayerManager.Instance.PlayAudiosRandom(_waveAudios);
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