using UnityEngine;

public class InputTest_2 : MonoBehaviour
{
    private PlayerInputAction _playerInputAction;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
    }

    private void Update()
    {
        Debug.Log(_playerInputAction.PlayerInput.enabled);
    }
}
