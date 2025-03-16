using Cinemachine;
using UnityEngine;

namespace ZiercCode.GameTools_2D
{
    public class CameraLerpToPointer : MonoBehaviour //让摄像机在玩家与鼠标之间的位置
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform cameraTarget;

        [SerializeField, Range(0f, 1f)] private float toPointerRate; //摄像机倾向鼠标位置的程度 0-1

        private Transform _cameraFollow; //摄像机真正跟随的目标
        private PlayerInputAction _playerInputAction;

        public void SetCameraTarget(Transform cT) //设置摄像机的目标 不是跟随对象
        {
            cameraTarget = cT;
        }

        private void OnDisable()
        {
            _playerInputAction.HeroControl.Disable();
        }

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            _cameraFollow = new GameObject("CameraFollower").transform;
        }

        private void Start()
        {
            _playerInputAction.HeroControl.Enable();
            GetComponent<CinemachineVirtualCamera>().m_Follow = _cameraFollow;
            GetComponent<CinemachineVirtualCamera>().m_LookAt = _cameraFollow;
        }

        private void Update()
        {
            UpdateCameraFollower();
        }

        private void UpdateCameraFollower()
        {
            if (!cameraTarget) return;

            Vector2 pointPosition =
                mainCamera.ScreenToWorldPoint(_playerInputAction.HeroControl.PointerPosition.ReadValue<Vector2>());
            _cameraFollow.position =
                Vector2.Lerp(cameraTarget.position, pointPosition, toPointerRate);
        }
    }
}