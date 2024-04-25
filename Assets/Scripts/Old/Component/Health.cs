using System;
using UnityEngine;

namespace ZiercCode.Old.Component
{
    public class Health : MonoBehaviour
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public bool IsDead { get; private set; }

        public event Action<int> CurrentHealthChanged;
        public event Action<int> MaxHealthChanged;
        public event Action InitializeEnded;
        public event Action Dead;

        public void Initialize(int maxHealth)
        {
            this.MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            if (maxHealth <= 0 || CurrentHealth <= 0)
            {
                IsDead = true;
                OnDead();
            }

            InitializeEnded?.Invoke();
        }

        public void SetCurrent(Func<int, int> set)
        {
            CurrentHealth = set.Invoke(CurrentHealth);
            CurrentHealthChanged?.Invoke(CurrentHealth);
            HealthCheck();
        }

        public void SetMax(Func<int, int> set)
        {
            MaxHealth = set.Invoke(MaxHealth);
            MaxHealthChanged?.Invoke(MaxHealth);
            HealthCheck();
        }

        private void HealthCheck()
        {
            if (IsDead) return;

            if (MaxHealth <= 0)
            {
                MaxHealth = 0;
                OnDead();
            }

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnDead();
            }
        }

        private void OnDead()
        {
            IsDead = true;
            Dead?.Invoke();
        }
    }
}