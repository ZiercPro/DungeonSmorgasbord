using UnityEngine;

namespace ZiercCode.Old.Component.Enemy
{
    /// <summary>
    /// 检测玩家是否进入攻击范围的组件
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public class EnemyAttackCheck : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D c2d;
        public bool isEnter { get; private set; }
        public Collider2D collier { get; private set; }

        public void SetRadius(float radius)
        {
            c2d.radius = radius;
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