using System;
using UnityEngine;

namespace Runtime.Component.Base
{
    public class Health : MonoBehaviour
    {
        public int currentHealth { get; private set; }
        public int maxHealth { get; private set; }
        public bool isDead { get; private set; }

        public event Action<int> CurrentHealthChanged;
        public event Action<int> MaxHealthChanged;
        public event Action InitializeEnded;
        public event Action Dead;

        public void Initialize(int maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
            if (maxHealth <= 0 || currentHealth <= 0)
            {
                isDead = true;
                OnDead();
            }

            InitializeEnded?.Invoke();
        }

        public void SetCurrent(Func<int, int> set)
        {
            currentHealth = set.Invoke(currentHealth);
            CurrentHealthChanged?.Invoke(currentHealth);
            HealthCheck();
        }

        public void SetMax(Func<int, int> set)
        {
            maxHealth = set.Invoke(maxHealth);
            MaxHealthChanged?.Invoke(maxHealth);
            HealthCheck();
        }

        private void HealthCheck()
        {
            if (isDead) return;

            if (maxHealth <= 0)
            {
                maxHealth = 0;
                OnDead();
            }

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnDead();
            }
        }

        private void OnDead()
        {
            isDead = true;
            Dead?.Invoke();
        }
    }
}