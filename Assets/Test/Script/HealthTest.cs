using UnityEngine;
using ZiercCode.Old.Component;

namespace ZiercCode.Test.Script
{
    public class HealthTest : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void Start()
        {
        }
    }
}