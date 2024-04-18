using Unity.VisualScripting;
using UnityEngine;
using ZiercCode.Runtime.ScriptObject;

namespace ZiercCode.Runtime.Component
{
    public class Attribute<T> : MonoBehaviour where T : AttributesBaseSo
    {
        [SerializeField] protected T _attributesBaseSo;
        public float moveSpeed;
        public int maxHealth;
    }
}