using System;
using UnityEngine;

namespace ZiercCode.Runtime.Component
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        public bool canMove;
        public float MoveSpeed { get; private set; }

        public void Initialize(float moveSpeed)
        {
            MoveSpeed = moveSpeed;
        }
        private void Awake()
        {
            _rb = GetComponentInChildren<Rigidbody2D>();
        }

        private void OnDisable()
        {
            StopMovePerform();
        }

        public void MovePerform(Vector2 moveDir)
        {
            if (!canMove) return;
            _rb.velocity = moveDir * MoveSpeed;
        }

        public void StopMovePerform()
        {
            _rb.velocity = Vector2.zero;
        }

        /// <summary>
        /// 修改速度
        /// </summary>
        /// <param name="operation">修改速度的方法</param>
        public void ChangeSpeed(Func<float, float> operation)
        {
            MoveSpeed = operation.Invoke(MoveSpeed);
        }
    }
}