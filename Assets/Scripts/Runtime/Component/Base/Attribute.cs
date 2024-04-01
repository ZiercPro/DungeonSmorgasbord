using UnityEngine;

namespace ZRuntime
{
    public class Attribute : MonoBehaviour
    {
        public int maxHealth;
        public float moveSpeed;

        public virtual void Initialize() { }
    }
}