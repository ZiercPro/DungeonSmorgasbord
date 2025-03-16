using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;

namespace ZiercCode._DungeonGame.Player
{
    public class Player_Gawain : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        [SerializeField] private Animator animator;
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private AutoFlipComponent autoFlipComponent;

        private PlayerInputAction _playerInputAction;

        private Vector2 _moveInput; //移动输入
        private Vector2 _mousePosition; //鼠标位置
        private Camera _mainCamera;

        private float _idle2Time = 10f; //播放待机动画idle2需要等待的时间

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
            _playerInputAction.HeroControl.Enable();
            moveComponent.MoveSpeed = moveSpeed;
        }

        private void Update()
        {
            //move
            _moveInput = _playerInputAction.HeroControl.Movement.ReadValue<Vector2>();
            moveComponent.Move(_moveInput);

            //flip
            if (!_mainCamera) _mainCamera = Camera.main;
            _mousePosition =
                _mainCamera.ScreenToWorldPoint(_playerInputAction.HeroControl.PointerPosition.ReadValue<Vector2>());
            autoFlipComponent.FaceTo(_mousePosition);

            //animation
            animator.SetBool("move", _moveInput != Vector2.zero);

            if (moveComponent.CurrentVelocity != Vector2.zero)
            {
                _idle2Time = 10f;
            }

            if (_idle2Time > 0f)
            {
                _idle2Time -= Time.deltaTime;
            }
            else
            {
                animator.SetTrigger("idle2");
                _idle2Time = 10f;
            }
        }
    }
}