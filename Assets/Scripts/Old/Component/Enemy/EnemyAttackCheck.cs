using UnityEngine;

namespace ZiercCode.Old.Component.Enemy
{
    /// <summary>
    /// 检测玩家是否进入攻击范围的组件
    /// </summary>
    public class EnemyAttackCheck : MonoBehaviour
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