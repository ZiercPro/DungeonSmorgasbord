using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZiercCode
{
    /// <summary>
    /// 射弹组件
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileComponent : MonoBehaviour
    {
        /// <summary>
        /// 刚体
        /// </summary>
        [SerializeField] private Rigidbody2D rigidBody2D;

        /// <summary>
        /// 发射方向
        /// </summary>
        private Vector3 _direction;

        /// <summary>
        /// 飞行速度
        /// </summary>
        private float _speed;

        /// <summary>
        /// 发射
        /// </summary>
        public void Fire(Vector3 direction, float speed)
        {
            Debug.Log(direction);
            Vector3 fireSpeed = direction * speed;
            rigidBody2D.velocity = fireSpeed;
        }
    }
}