using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Test
{
    public class InputTest : MonoBehaviour
    {
        private Vector2 _moveDir;
        private float _speed;
        private PlayerInputAction _playerInputAction;

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
        }

        private void Start()
        {
            _playerInputAction.PlayerInput.Enable();
            _speed = 1f;
        }

        private void Update()
        {
            transform.Translate(_moveDir * _speed);
            Debug.Log(_playerInputAction.PlayerInput.enabled);
        }

        // public void UIInput_1(InputAction.CallbackContext context)
        // {
        //     Debug.Log(context);
        // }
        //
        // public void Move(InputAction.CallbackContext context)
        // {
        //     _moveDir = context.ReadValue<Vector2>();
        // }
    }
}