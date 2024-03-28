using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.Helper
{
    public class GizmosDraw : MonoBehaviour
    {
        [Header("Static")] [Space] public float radius;
        public Color color;
        public Transform Pos;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = color;
            Vector3 position = Pos == null ? Vector3.zero : Pos.position;
            Gizmos.DrawWireSphere(Pos.position, radius);
        }
    }
}