using UnityEngine;

namespace Runtime.Component.Enemy
{
    public class AttackCheck : MonoBehaviour
    {
        private CircleCollider2D _c2d;
        public bool isEnter { get; private set; }
        public Collider2D collier { get; private set; }

        private void Awake()
        {
            _c2d = GetComponent<CircleCollider2D>();
        }

        public void SetRadius(float radius)
        {
            _c2d.radius = radius;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isEnter = true;
                collier = collision;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                isEnter = false;
                collier = collision;
            }
        }
    }
}