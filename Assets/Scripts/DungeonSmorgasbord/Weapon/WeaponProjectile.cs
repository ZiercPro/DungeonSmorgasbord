using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 武器射弹组件，用于实现射弹的各种逻辑
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class WeaponProjectile : WeaponBase
    {
        /// <summary>
        /// 刚体
        /// </summary>
        [SerializeField] private Rigidbody2D rigidBody2D;

        /// <summary>
        /// 飞行速度
        /// </summary>
        [SerializeField] private float speed;

        /// <summary>
        /// 发射方向
        /// </summary>
        private Vector3 _direction;

        private void Awake()
        {
            rigidBody2D.isKinematic = true;
        }

        /// <summary>
        /// 发射
        /// </summary>
        public void Fire(Vector3 direction)
        {
            rigidBody2D.isKinematic = false;
            Vector3 fireSpeed = direction * speed;
            rigidBody2D.velocity = fireSpeed;
        }
    }
}