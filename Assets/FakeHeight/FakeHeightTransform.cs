using UnityEngine;
using UnityEngine.Events;

namespace ZiercCode.FakeHeight
{
    [RequireComponent(typeof(Shadow2D))]
    public class FakeHeightTransform : MonoBehaviour
    {
        [SerializeField] private Shadow2D shadow2D;

        [SerializeField] private float virtualGravity; //模拟重力加速度
        [SerializeField] private bool updateGravity; //更新重力

        public UnityEvent onGrounded; //当物体接触地面时调用
        public UnityEvent onFirstGrounded; //第一次接触地面时调用

        public Vector2 groundVelocity;
        private float _verticalVelocity;
        private float _rotationVelocity;

        private float _lastVerticalVelocity; //记录上一次初始化时的垂直速度

        private bool _isGrounded;
        private bool _isFirstGrounded; //是否是初次接触地面

        public void Init(Vector2 gV, float vV, bool isFirstGround, float rV = 0f, float startHeight = 0f)
        {
            _isGrounded = false;
            _isFirstGrounded = isFirstGround;
            groundVelocity = gV;
            _verticalVelocity = vV;
            _lastVerticalVelocity = vV;
            _rotationVelocity = rV;

            //设置初始高度
            shadow2D.ShadowObjectTransform.position =
                shadow2D.CasterTransform.position + shadow2D.ShadowOffset + new Vector3(0f, -startHeight, 0f);
        }

        private void UpdateGroundPosition() //更新水平位置
        {
            if (groundVelocity == Vector2.zero) return; //如果速度减为零了 就不再更新
            transform.position += (Vector3)groundVelocity * Time.deltaTime; //本体和阴影一起运动
        }

        private void UpdateVerticalPosition() //更新垂直位置
        {
            if (!_isGrounded && updateGravity) //更新本体y轴位置
            {
                _verticalVelocity -= virtualGravity * Time.deltaTime;
                shadow2D.CasterTransform.position += new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime;
            }
        }

        //更新旋转
        private void UpdateRotation()
        {
            if (!_isGrounded && _rotationVelocity > 0f)
            {
                shadow2D.CasterTransform.Rotate(shadow2D.CasterTransform.forward, _rotationVelocity * Time.deltaTime);
            }
        }

        private void CheckGrounded()
        {
            if (shadow2D.CasterTransform.position.y + shadow2D.ShadowOffset.y <
                shadow2D.ShadowObjectTransform.position.y && !_isGrounded)
            {
                if (_isFirstGrounded)
                {
                    _isFirstGrounded = false;
                    onFirstGrounded.Invoke();
                }

                _isGrounded = true;
                shadow2D.CasterTransform.position = shadow2D.ShadowObjectTransform.position - shadow2D.ShadowOffset;
                onGrounded.Invoke();
            }
        }

        private void Update()
        {
            UpdateGroundPosition();
            UpdateVerticalPosition();
            UpdateRotation();
            CheckGrounded();
        }

        //降低平面速度 用于在碰到地面时调用
        public void SlowDownGroundVelocity(float division)
        {
            groundVelocity /= division;
        }

        public void SlowDownRotateVelocity(float division)
        {
            _rotationVelocity /= division;
        }

        //碰到地面时弹起
        public void Bounce(float division)
        {
            Init(groundVelocity, _lastVerticalVelocity / division, false, _rotationVelocity);
        }

        public void StopGroundMove()
        {
            groundVelocity = Vector2.zero;
        }

        public void StopVerticalMove()
        {
            _verticalVelocity = 0f;
        }

        public void StopRotate()
        {
            _rotationVelocity = 0f;
        }
    }
}