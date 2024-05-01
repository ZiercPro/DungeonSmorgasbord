using System;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.Old.Component
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private CreatureAttributesSo creatureAttributesSo;
        public bool IsDead { get; private set; }

        private int _currentHealth;
        private int _maxHealth;

        public class HealthChangeEventArgs : EventArgs
        {
            /// <summary>
            /// 最大生命值
            /// </summary>
            public int MaxHealth;

            /// <summary>
            /// 当前生命值
            /// </summary>
            public int CurrentHealth;

            /// <summary>
            ///生命值改变类型 
            /// </summary>
            public HealthChangeType HealthChangeType;
        }

        /// <summary>
        ///生命值改变类型 
        /// </summary>
        public enum HealthChangeType
        {
            /// <summary>
            /// 伤害
            /// </summary>
            Damage,

            /// <summary>
            /// 治疗
            /// </summary>
            Cure
        }

        /// <summary>
        /// 生命值改变事件
        /// </summary>
        public event Action<HealthChangeEventArgs> HealthChanged;

        /// <summary>
        /// 死亡事件
        /// </summary>
        public event Action Dead;

        private void Start()
        {
            this._maxHealth = creatureAttributesSo.maxHealth;
            _currentHealth = _maxHealth;
            if (_maxHealth <= 0 || _currentHealth <= 0)
            {
                IsDead = true;
                OnDead();
            }
        }

        /// <summary>
        /// 设置当前生命值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="healthChangeType">生命值改变的类型</param>
        public void SetCurrentHealth(int value, HealthChangeType healthChangeType)
        {
            _currentHealth = value;
            HealthChanged?.Invoke(new HealthChangeEventArgs
            {
                CurrentHealth = _currentHealth, MaxHealth = _maxHealth, HealthChangeType = healthChangeType
            });
            HealthCheck();
        }

        /// <summary>
        /// 获取当前生命值
        /// </summary>
        /// <returns></returns>
        public int GetCurrentHealth()
        {
            return _currentHealth;
        }

        /// <summary>
        /// 设置最大生命值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="healthChangeType">生命值改变类型</param>
        public void SetMaxHealth(int value, HealthChangeType healthChangeType)
        {
            _maxHealth = value;
            HealthChanged?.Invoke(new HealthChangeEventArgs
            {
                CurrentHealth = _currentHealth, MaxHealth = _maxHealth, HealthChangeType = healthChangeType
            });
            HealthCheck();
        }

        /// <summary>
        /// 获取最大生命值
        /// </summary>
        /// <returns></returns>
        public int GetMaxHealth()
        {
            return _maxHealth;
        }

        /// <summary>
        /// 检查生命值是否异常或者是否死亡
        /// </summary>
        private void HealthCheck()
        {
            if (IsDead) return;

            if (_maxHealth <= 0)
            {
                _maxHealth = 0;
                OnDead();
            }

            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
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