using System;
using UnityEngine;
using UnityEngine.InputSystem;
using ZiercCode.Runtime.Manager;

namespace ZiercCode.Runtime.Player
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInputAction _playerInputAction;
        private Camera _mainCamera;

        private bool _isPlayerInputActive;
        private bool _isUIInputActive;

        //鼠标左键
        public event Action MouseLeftClickStarted;
        public event Action MouseLeftClickPerformed;
        public event Action MouseLeftClickCanceled;

        //鼠标右键
        public event Action MouseRightClickStarted;
        public event Action MouseRightClickPerformed;
        public event Action MouseRightClickCanceled;

        //移动
        public event Action<Vector2> MovementInputPerforming;

        private bool _isMovementPerforming;

        //鼠标位置
        public event Action<Vector2> MousePositionChanging;

        private bool _isMouseMoving;

        //冲刺
        public event Action<Vector2> DashButtonPressedStarted;
        public event Action<Vector2> DashButtonPressedPerformed;
        public event Action<Vector2> DashButtonPressedCanceled;

        //互动
        public event Action InteractButtonPressStarted;
        public event Action InteractButtonPressPerformed;
        public event Action InteractButtonPressCanceled;

        //ESC
        public event Action EscButtonPressStarted;
        public event Action EscButtonPressing;
        public event Action EscButtonReleased;

        private void OnEnable()
        {
            _playerInputAction.PlayerInput.MouseClickLeft.started += OnMouseLeftStarted;
            _playerInputAction.PlayerInput.MouseClickLeft.canceled += OnMouseLeftCanceled;
            _playerInputAction.PlayerInput.MouseClickLeft.performed += OnMouseLeftPerformed;

            _playerInputAction.PlayerInput.MouseClickRight.started += OnMouseRightStarted;
            _playerInputAction.PlayerInput.MouseClickRight.canceled += OnMouseRightCanceled;
            _playerInputAction.PlayerInput.MouseClickRight.performed += OnMouseRightPerformed;

            _playerInputAction.PlayerInput.Interact.started += OnInteractStarted;
            _playerInputAction.PlayerInput.Interact.canceled += OnInteractCanceled;
            _playerInputAction.PlayerInput.Interact.performed += OnInteractPerformed;

            _playerInputAction.PlayerInput.Dash.started += OnDashPressedStarted;
            _playerInputAction.PlayerInput.Dash.canceled += OnDashPressedCanceled;
            _playerInputAction.PlayerInput.Dash.performed += OnDashPressedPerformed;

            _playerInputAction.PlayerInput.Movement.started += OnMovementInputStarted;
            _playerInputAction.PlayerInput.Movement.canceled += OnMovementInputCanceled;

            _playerInputAction.UI.Back.performed += OnEscButtonPressing;
            _playerInputAction.UI.Back.started += OnEscButtonPressStart;
            _playerInputAction.UI.Back.canceled += OnEscButtonRelease;
            GameRoot.OnGamePaues += OnPause;
            GameRoot.OnGameResume += OnResume;
        }

        private void OnDisable()
        {
            _playerInputAction.PlayerInput.MouseClickLeft.started -= OnMouseLeftStarted;
            _playerInputAction.PlayerInput.MouseClickLeft.canceled -= OnMouseLeftCanceled;
            _playerInputAction.PlayerInput.MouseClickLeft.performed -= OnMouseLeftPerformed;

            _playerInputAction.PlayerInput.MouseClickRight.started -= OnMouseRightStarted;
            _playerInputAction.PlayerInput.MouseClickRight.canceled -= OnMouseRightCanceled;
            _playerInputAction.PlayerInput.MouseClickRight.performed -= OnMouseRightPerformed;

            _playerInputAction.PlayerInput.Interact.started -= OnInteractStarted;
            _playerInputAction.PlayerInput.Interact.canceled -= OnInteractCanceled;
            _playerInputAction.PlayerInput.Interact.performed -= OnInteractPerformed;

            _playerInputAction.PlayerInput.Dash.started -= OnDashPressedStarted;
            _playerInputAction.PlayerInput.Dash.canceled -= OnDashPressedCanceled;
            _playerInputAction.PlayerInput.Dash.performed -= OnDashPressedPerformed;

            _playerInputAction.PlayerInput.Movement.started -= OnMovementInputStarted;
            _playerInputAction.PlayerInput.Movement.canceled -= OnMovementInputCanceled;

            _playerInputAction.UI.Back.performed -= OnEscButtonPressing;
            _playerInputAction.UI.Back.started -= OnEscButtonPressStart;
            _playerInputAction.UI.Back.canceled -= OnEscButtonRelease;

            GameRoot.OnGamePaues -= OnPause;
            GameRoot.OnGameResume -= OnResume;

            _playerInputAction.PlayerInput.Disable();
        }

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            _isMouseMoving = true;
            _isMovementPerforming = true;
        }

        private void Update()
        {
            OnMovementInput();
            OnMousePositionChanging();
        }

        public void SetPlayerInput(bool isActive)
        {
            if (isActive)
                _playerInputAction.PlayerInput.Enable();
            else
                _playerInputAction.PlayerInput.Disable();

            _isPlayerInputActive = isActive;
        }

        public void SetUIInput(bool isActive)
        {
            if (isActive)
                _playerInputAction.UI.Enable();
            else
                _playerInputAction.UI.Disable();

            _isUIInputActive = isActive;
        }

        #region GameManage

        private void OnPause()
        {
            _isMouseMoving = false;
            _playerInputAction.PlayerInput.Disable();
        }

        private void OnResume()
        {
            _isMouseMoving = true;
            _playerInputAction.PlayerInput.Enable();
        }

        #endregion

        #region LeftClick

        private void OnMouseLeftStarted(InputAction.CallbackContext context)
        {
            MouseLeftClickStarted?.Invoke();
        }

        private void OnMouseLeftPerformed(InputAction.CallbackContext context)
        {
            MouseLeftClickPerformed?.Invoke();
        }

        private void OnMouseLeftCanceled(InputAction.CallbackContext context)
        {
            MouseLeftClickCanceled?.Invoke();
        }

        #endregion

        #region RightClick

        private void OnMouseRightStarted(InputAction.CallbackContext context)
        {
            MouseRightClickStarted?.Invoke();
        }

        private void OnMouseRightPerformed(InputAction.CallbackContext context)
        {
            MouseRightClickPerformed?.Invoke();
        }

        private void OnMouseRightCanceled(InputAction.CallbackContext context)
        {
            MouseRightClickCanceled?.Invoke();
        }

        #endregion

        #region Interact

        private void OnInteractStarted(InputAction.CallbackContext context)
        {
            InteractButtonPressStarted?.Invoke();
        }

        private void OnInteractPerformed(InputAction.CallbackContext context)
        {
            InteractButtonPressPerformed?.Invoke();
        }

        private void OnInteractCanceled(InputAction.CallbackContext context)
        {
            InteractButtonPressCanceled?.Invoke();
        }

        #endregion

        #region MovementInput

        private void OnMovementInputStarted(InputAction.CallbackContext context)
        {
            MovementInputPerforming?.Invoke(_playerInputAction.PlayerInput.Movement.ReadValue<Vector2>());
            _isMovementPerforming = true;
        }

        private void OnMovementInput()
        {
            if (!_isMovementPerforming) return;
            MovementInputPerforming?.Invoke(_playerInputAction.PlayerInput.Movement.ReadValue<Vector2>());
        }

        private void OnMovementInputCanceled(InputAction.CallbackContext context)
        {
            MovementInputPerforming?.Invoke(_playerInputAction.PlayerInput.Movement.ReadValue<Vector2>());
            _isMovementPerforming = false;
        }

        #endregion

        #region MousePositionInput

        private void OnMousePositionChanging()
        {
            if (!_isMouseMoving) return;
            Vector2 mousePos =
                _mainCamera.ScreenToWorldPoint(_playerInputAction.PlayerInput.PointerPosition.ReadValue<Vector2>());
            MousePositionChanging?.Invoke(mousePos);
        }

        #endregion

        #region Dash

        private void OnDashPressedStarted(InputAction.CallbackContext context)
        {
            DashButtonPressedStarted?.Invoke(_playerInputAction.PlayerInput.Movement.ReadValue<Vector2>());
        }

        private void OnDashPressedPerformed(InputAction.CallbackContext context)
        {
            DashButtonPressedPerformed?.Invoke(_playerInputAction.PlayerInput.Movement.ReadValue<Vector2>());
        }

        private void OnDashPressedCanceled(InputAction.CallbackContext context)
        {
            DashButtonPressedCanceled?.Invoke(_playerInputAction.PlayerInput.Movement.ReadValue<Vector2>());
        }

        #endregion

        #region ESC

        private void OnEscButtonPressStart(InputAction.CallbackContext context)
        {
            EscButtonPressStarted?.Invoke();
        }

        private void OnEscButtonPressing(InputAction.CallbackContext context)
        {
            EscButtonPressing?.Invoke();
        }

        private void OnEscButtonRelease(InputAction.CallbackContext context)
        {
            EscButtonReleased?.Invoke();
        }

        #endregion
    }
}