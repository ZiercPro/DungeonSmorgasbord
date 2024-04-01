using UnityEngine;

namespace ZRuntime
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D _rb;
        public bool canMove;
        public float moveSpeed { get; private set; }

        private void Awake()
        {
            _rb = GetComponentInChildren<Rigidbody2D>();
        }

        private void OnDisable()
        {
            StopMovePerform();
        }

        public void Initialize(float speed)
        {
            moveSpeed = speed;
            canMove = true;
        }

        public void MovePerform(Vector2 moveDir)
        {
            if (!canMove) return;
            _rb.velocity = moveDir * moveSpeed;
        }

        public void StopMovePerform()
        {
            _rb.velocity = Vector2.zero;
        }

        public void AddSpeed(float amount)
        {
            moveSpeed += amount;
        }

        public void ReduceSpeed(float amount)
        {
            moveSpeed -= amount;
        }
    }
}