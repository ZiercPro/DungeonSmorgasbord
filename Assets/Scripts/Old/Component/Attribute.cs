using UnityEngine;
using ZiercCode.Old.ScriptObject;

namespace ZiercCode.Old.Component
{
    public class Attribute<T> : MonoBehaviour where T : AttributesBaseSo
    {
        [SerializeField] protected T _attributesBaseSo;
        public float moveSpeed;
        public int maxHealth;
    }
}