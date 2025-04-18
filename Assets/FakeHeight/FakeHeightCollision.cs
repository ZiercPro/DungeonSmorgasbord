using UnityEngine;
using ZiercCode.Utilities;

namespace ZiercCode.FakeHeight
{
    /// <summary>
    /// 用于伪高度物体检测碰撞
    /// </summary>
    [RequireComponent(typeof(FakeHeightTransform))]
    public class FakeHeightCollision : MonoBehaviour
    {
        [SerializeField]
        private Vector2 colliderBox;

        private FakeHeightTransform fakeHeightTransform;
        private RangeDetector _rangeDetect = new(1);

        private float _rayDistance;

        private void Awake()
        {
            fakeHeightTransform = GetComponent<FakeHeightTransform>();

            _rayDistance = colliderBox.magnitude;
        }

        private void FixedUpdate()
        {
            CheckCollisions();
        }

        //运动方向碰到碰撞体后停止移动
        private void CheckCollisions()
        {
            if (_rangeDetect.DetectInBox(transform.position, colliderBox, transform.rotation.eulerAngles.z))
            {
                if (_rangeDetect.DetectInRay(transform.position,
                        fakeHeightTransform.groundVelocity, _rayDistance))
                {
                    fakeHeightTransform.StopGroundMove();
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, colliderBox);
        }
#endif
    }
}