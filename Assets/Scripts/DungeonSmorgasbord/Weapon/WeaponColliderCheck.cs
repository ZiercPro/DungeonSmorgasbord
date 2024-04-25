using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public class WeaponColliderCheck : MonoBehaviour
    {
        [SerializeField] private Collider2D hitBox;


        public UnityEvent<Collider2D> triggerEntered;
        public UnityEvent<Collider2D> triggerExited;

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
    }
}