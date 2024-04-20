using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZiercCode.Old.Hero
{
    public class HeroInputManager : MonoBehaviour
    {
        private PlayerInputAction _playerInputAction;
        private Camera _mainCamera;

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


        //鼠标位置
        public event Action<Vector2> MousePositionChanging;


        //冲刺
        public event Action<Vector2> DashButtonPressedStarted;
        public event Action<Vector2> DashButtonPressedPerformed;
        public event Action<Vector2> DashButtonPressedCanceled;

        //互动
        public event Action InteractButtonPressStarted;
        public event Action InteractButtonPressPerformed;
        public event Action InteractButtonPressCanceled;

        private void OnEnable()
        {
            _playerInputAction.HeroControl.MouseClickLeft.started += OnMouseLeftStarted;
            _playerInputAction.HeroControl.MouseClickLeft.canceled += OnMouseLeftCanceled;
            _playerInputAction.HeroControl.MouseClickLeft.performed += OnMouseLeftPerformed;

            _playerInputAction.HeroControl.MouseClickRight.started += OnMouseRightStarted;
            _playerInputAction.HeroControl.MouseClickRight.canceled += OnMouseRightCanceled;
            _playerInputAction.HeroControl.MouseClickRight.performed += OnMouseRightPerformed;

            _playerInputAction.HeroControl.Interact.started += OnInteractStarted;
            _playerInputAction.HeroControl.Interact.canceled += OnInteractCanceled;
            _playerInputAction.HeroControl.Interact.performed += OnInteractPerformed;

            _playerInputAction.HeroControl.Dash.started += OnDashPressedStarted;
            _playerInputAction.HeroControl.Dash.canceled += OnDashPressedCanceled;
            _playerInputAction.HeroControl.Dash.performed += OnDashPressedPerformed;

            _playerInputAction.HeroControl.Movement.started += OnMovementInputStarted;
            _playerInputAction.HeroControl.Movement.canceled += OnMovementInputCanceled;
        }

        private void OnDisable()
        {
            _playerInputAction.HeroControl.MouseClickLeft.started -= OnMouseLeftStarted;
            _playerInputAction.HeroControl.MouseClickLeft.canceled -= OnMouseLeftCanceled;
            _playerInputAction.HeroControl.MouseClickLeft.performed -= OnMouseLeftPerformed;

            _playerInputAction.HeroControl.MouseClickRight.started -= OnMouseRightStarted;
            _playerInputAction.HeroControl.MouseClickRight.canceled -= OnMouseRightCanceled;
            _playerInputAction.HeroControl.MouseClickRight.performed -= OnMouseRightPerformed;

            _playerInputAction.HeroControl.Interact.started -= OnInteractStarted;
            _playerInputAction.HeroControl.Interact.canceled -= OnInteractCanceled;
            _playerInputAction.HeroControl.Interact.performed -= OnInteractPerformed;

            _playerInputAction.HeroControl.Dash.started -= OnDashPressedStarted;
            _playerInputAction.HeroControl.Dash.canceled -= OnDashPressedCanceled;
            _playerInputAction.HeroControl.Dash.performed -= OnDashPressedPerformed;

            _playerInputAction.HeroControl.Movement.started -= OnMovementInputStarted;
            _playerInputAction.HeroControl.Movement.canceled -= OnMovementInputCanceled;
        }

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            OnMovementInput();
            OnMousePositionChanging();
        }

        public void SetHeroControl(bool enable)
        {
            if (enable)
                _playerInputAction.HeroControl.Enable();
            else
                _playerInputAction.HeroControl.Disable();
        }


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
            MovementInputPerforming?.Invoke(_playerInputAction.HeroControl.Movement.ReadValue<Vector2>());
        }

        private void OnMovementInput()
        {
            MovementInputPerforming?.Invoke(_playerInputAction.HeroControl.Movement.ReadValue<Vector2>());
        }

        private void OnMovementInputCanceled(InputAction.CallbackContext context)
        {
            MovementInputPerforming?.Invoke(_playerInputAction.HeroControl.Movement.ReadValue<Vector2>());
        }

        #endregion

        #region MousePositionInput

        private void OnMousePositionChanging()
        {
            Vector2 mousePos =
                _mainCamera.ScreenToWorldPoint(_playerInputAction.HeroControl.PointerPosition.ReadValue<Vector2>());
            MousePositionChanging?.Invoke(mousePos);
        }

        #endregion

        #region Dash

        private void OnDashPressedStarted(InputAction.CallbackContext context)
        {
            DashButtonPressedStarted?.Invoke(_playerInputAction.HeroControl.Movement.ReadValue<Vector2>());
        }

        private void OnDashPressedPerformed(InputAction.CallbackContext context)
        {
            DashButtonPressedPerformed?.Invoke(_playerInputAction.HeroControl.Movement.ReadValue<Vector2>());
        }

        private void OnDashPressedCanceled(InputAction.CallbackContext context)
        {
            DashButtonPressedCanceled?.Invoke(_playerInputAction.HeroControl.Movement.ReadValue<Vector2>());
        }

        #endregion
    }
}