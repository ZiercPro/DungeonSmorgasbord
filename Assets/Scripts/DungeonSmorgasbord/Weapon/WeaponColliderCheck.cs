using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 武器碰撞检测组件
    /// </summary>
    public class WeaponColliderCheck : MonoBehaviour
    {
        [SerializeField] private Collider2D hitBox;

        public UnityEvent<Collider2D> triggerEntered;
        public UnityEvent<Collider2D> triggerExited;
        public UnityEvent<Collider2D> triggerStay;

        private void Awake()
        {
            hitBox.enabled = false;
        }

        public void Enable()
        {
            hitBox.enabled = true;
        }

        public void Disable()
        {
            hitBox.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            triggerEntered?.Invoke(other);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            triggerStay?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            triggerExited?.Invoke(other);
        }
    }
}