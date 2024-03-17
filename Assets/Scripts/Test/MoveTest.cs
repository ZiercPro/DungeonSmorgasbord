using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTest : MonoBehaviour
{
    private InputManager _inputManager;
    private Movement _movement;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _inputManager = GetComponent<InputManager>();
    }

    private void Start()
    {
        _movement.Initialize(2f);
        _inputManager.MovementInputPeforming += moveDir => { _movement.MovePerform(moveDir); };
    }
}