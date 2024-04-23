using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Core.Extend;
using ZiercCode.DungeonSmorgasbord.ScriptObject;
using ZiercCode.Old.Audio;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public abstract class Weapon : MonoBehaviour, IWeaponBase
    {
        [SerializeField] protected WeaponDataSo WeaponDataSo;
        [SerializeField] protected List<AudioName> WaveAudios;

        protected int FinalDamageAmount;
        protected float CriticalChance;

        protected Animator Animator;
        protected Dictionary<WeaponType, float> WeaponDamageRate;
        protected WeaponAnimationEventHandler AnimationEventHandler;

        protected virtual void Awake()
        {
            Animator = GetComponentInChildren<Animator>();
            AnimationEventHandler = GetComponent<WeaponAnimationEventHandler>();
        }

        protected void DamageCount()
        {
            FinalDamageAmount = (int)WeaponDamageRate[WeaponDataSo.WeaponType] * WeaponDataSo.Damage;
            bool isCriticalHit = MyMath.ChanceToBool(CriticalChance);
            if (isCriticalHit)
            {
                FinalDamageAmount *= 2;
            }
        }

        public void Initialize(Dictionary<WeaponType, float> weaponDamageRate, float criticalChance)
        {
            WeaponDamageRate = weaponDamageRate;
            CriticalChance = criticalChance;
        }

        public void Disable()
        {
            WeaponDamageRate = null;
            CriticalChance = 0f;
        }

        public abstract void OnLeftButtonPressStarted();
        public abstract void OnLeftButtonPressing();
        public abstract void OnLeftButtonPressCanceled();
        public abstract void OnRightButtonPressStarted();
        public abstract void OnRightButtonPressing();
        public abstract void OnRightButtonPressCanceled();

        public Transform GetWeaponTransform()
        {
            return transform;
        }
    }
}