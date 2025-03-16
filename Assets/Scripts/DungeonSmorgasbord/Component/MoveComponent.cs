using System;
using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveComponent : MonoBehaviour
    {
        /// <summary>
        /// 刚体组件
        /// </summary>
        [SerializeField] private Rigidbody2D rb2D;

        /// <summary>
        /// 移动速度
        /// </summary>
        private float _moveSpeed;

        /// <summary>
        /// 是否可以移动
        /// </summary>
        private bool _canMove;

        /// <summary>
        /// 移动速度属性
        /// </summary>
        public float MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }
        public Vector2 CurrentVelocity => rb2D.velocity;

        public void Move(Vector2 moveDir)
        {
            rb2D.velocity = moveDir * _moveSpeed;
        }

        public void Stop()
        {
            rb2D.velocity = Vector2.zero;
        }

        /// <summary>
        /// 修改速度
        /// </summary>
        /// <param name="operation">修改速度的方法</param>
        public void ChangeSpeed(Func<float, float> operation)
        {
            _moveSpeed = operation.Invoke(_moveSpeed);
        }
    }
}