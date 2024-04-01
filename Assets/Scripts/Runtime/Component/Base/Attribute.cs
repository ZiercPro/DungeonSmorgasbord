using UnityEngine;

namespace ZiercCode.Runtime.Component.Base
{
    public class Attribute : MonoBehaviour
    {
        public int maxHealth;
        public float moveSpeed;

        public virtual void Initialize() { }
    }
}