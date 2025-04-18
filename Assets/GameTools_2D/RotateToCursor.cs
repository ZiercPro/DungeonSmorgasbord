using UnityEngine;
using ZiercCode.Management;

namespace ZiercCode.GameTools_2D
{
    //本身转向鼠标
    public class RotateToCursor : PauseBehaviour
    {
        [SerializeField] private float rotateRate = 10f;

        private PlayerInputAction _playerInputAction;

        private Vector2 _pointScreenPosition;
        private Vector2 _pointWorldPosition;

        private Camera _mainCamera;

        private void OnEnable()
        {
            _playerInputAction.HeroControl.Enable();
        }

        private void OnDisable()
        {
            _playerInputAction.HeroControl.Disable();
        }

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        protected override void PauseUpdate()
        {
            if (_mainCamera)
            {
                _pointScreenPosition = _playerInputAction.HeroControl.PointerPosition.ReadValue<Vector2>();

                _pointWorldPosition = _mainCamera.ScreenToWorldPoint(_pointScreenPosition);

                Vector2 direction = (_pointWorldPosition - (Vector2)transform.position).normalized;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                Quaternion rotation = Quaternion.AngleAxis(angle, transform.forward); //通过四元数实现，避免欧拉角多种表示问题
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateRate * Time.deltaTime);
            }
            else
            {
                _mainCamera = Camera.main;
            }
        }
    }
}