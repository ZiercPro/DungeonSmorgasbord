using UnityEngine;

namespace ZiercCode.Runtime.Component
{
    public class Attribute : MonoBehaviour
    {
        public int maxHealth;
        public float moveSpeed;

        public virtual void Initialize() { }
    }
}