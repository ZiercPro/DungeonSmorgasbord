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
        private Rigidbody2D _rb;

        /// <summary>
        /// 移动速度
        /// </summary>
        private float _moveSpeed;

        /// <summary>
        /// 是否可以移动
        /// </summary>
        private bool _canMove;

        /// <summary>
        /// 设置移动速度
        /// </summary>
        /// <param name="moveSpeed">移动速度</param>
        public void SetMoveSpeed(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        /// <summary>
        /// 获取移动速度
        /// </summary>
        /// <returns></returns>
        public float GetMoveSpeed()
        {
            return _moveSpeed;
        }

        private void OnEnable()
        {
            Enable();
        }

        private void Awake()
        {
            _rb = GetComponentInChildren<Rigidbody2D>();
        }

        private void OnDisable()
        {
            Stop();
            Disable();
        }

        public void Move(Vector2 moveDir)
        {
            if (!_canMove) return;
            _rb.velocity = moveDir * _moveSpeed;
        }

        public void Stop()
        {
            _rb.velocity = Vector2.zero;
        }

        /// <summary>
        /// 修改速度
        /// </summary>
        /// <param name="operation">修改速度的方法</param>
        public void ChangeSpeed(Func<float, float> operation)
        {
            _moveSpeed = operation.Invoke(_moveSpeed);
        }

        /// <summary>
        /// 允许移动
        /// </summary>
        public void Enable()
        {
            _canMove = true;
        }

        /// <summary>
        /// 禁止移动
        /// </summary>
        public void Disable()
        {
            _canMove = false;
        }

        /// <summary>
        /// 获取是否可以移动
        /// </summary>
        /// <returns></returns>
        public bool CanMove()
        {
            return _canMove;
        }
    }
}