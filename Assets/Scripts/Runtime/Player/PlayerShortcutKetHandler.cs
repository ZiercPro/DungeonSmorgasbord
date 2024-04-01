using System;
using UnityEngine;

namespace ZiercCode.Runtime.Player
{
    public class PlayerShortcutKetHandler : MonoBehaviour
    {
        private InputManager _inputManager;

        private void Awake()
        {
            _inputManager = GetComponent<InputManager>();
        }
        
        
    }
}