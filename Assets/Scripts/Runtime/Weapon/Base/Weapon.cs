using UnityEngine;

namespace Runtime.Weapon.Base
{
    public class Weapon : MonoBehaviour
    {
        public DamageType damageType;
        public WeaponType weaponType;
        public int damageAmount;

        protected int FinalDamageAmount;
        protected float CriticalChance;

        protected Animator Animator;
        protected InputManager InputManager;
        protected SerDictionary<WeaponType, float> WeaponDamageRate;
        protected WeaponAnimationEventHandler AnimationEventHandler;

        protected virtual void Awake()
        {
            Animator = GetComponentInChildren<Animator>();
            AnimationEventHandler = GetComponentInChildren<WeaponAnimationEventHandler>();
        }

        protected virtual void DamageCount()
        {
            FinalDamageAmount = (int)WeaponDamageRate[weaponType] * damageAmount;
            bool isCriticalHit = MyMath.IsEffected(CriticalChance);
            if (isCriticalHit)
            {
                FinalDamageAmount *= 2;
            }
        }

        public virtual void Initialize(SerDictionary<WeaponType, float> weaponDamageRate, float criticalChance,
            InputManager inputManager)
        {
            WeaponDamageRate = weaponDamageRate;
            CriticalChance = criticalChance;
            InputManager = inputManager;
        }

        public virtual void Disable()
        {
            WeaponDamageRate = null;
            CriticalChance = 0f;
            InputManager = null;
        }
    }
}