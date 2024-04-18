using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Old.Damage;
using ZiercCode.Old.Helper;
using ZiercCode.Old.Hero;

namespace ZiercCode.Old.Weapon
{

    public class Weapon : MonoBehaviour
    {
        public DamageType damageType;
        public WeaponType weaponType;
        public int damageAmount;

        protected int FinalDamageAmount;
        protected float CriticalChance;

        protected Animator Animator;
        protected HeroInputManager HeroInputManager;
        protected Dictionary<WeaponType, float> WeaponDamageRate;
        protected WeaponAnimationEventHandler AnimationEventHandler;

        protected virtual void Awake()
        {
            Animator = GetComponentInChildren<Animator>();
            AnimationEventHandler = GetComponentInChildren<WeaponAnimationEventHandler>();
        }

        protected virtual void DamageCount()
        {
            FinalDamageAmount = (int)WeaponDamageRate[weaponType] * damageAmount;
            bool isCriticalHit = MyMath.ChanceToBool(CriticalChance);
            if (isCriticalHit)
            {
                FinalDamageAmount *= 2;
            }
        }

        public virtual void Initialize(Dictionary<WeaponType, float> weaponDamageRate, float criticalChance,
            HeroInputManager heroInputManager)
        {
            WeaponDamageRate = weaponDamageRate;
            CriticalChance = criticalChance;
            HeroInputManager = heroInputManager;
        }

        public virtual void Disable()
        {
            WeaponDamageRate = null;
            CriticalChance = 0f;
            HeroInputManager = null;
        }
    }
}