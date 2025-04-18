using RMC.Mini;
using RMC.Mini.Service;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using ZiercCode.Management;

namespace ZiercCode._DungeonGame.UI.PauseMenu
{
    public class PauseService : BaseService
    {
        private PlayerInputAction _playerInputAction;

        public UnityEvent OnBackInput;

        public PauseService()
        {
            _playerInputAction = new PlayerInputAction();
            OnBackInput = new UnityEvent();
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                _playerInputAction.UI.Enable();
                _playerInputAction.UI.Back.started += BackInput;
            }
        }

        public override void Dispose()
        {
            _playerInputAction.UI.Back.started -= BackInput;
            _playerInputAction.UI.Disable();
        }

        private void BackInput(InputAction.CallbackContext context)
        {
            OnBackInput?.Invoke();
        }

        public bool IsGamePaused()
        {
            return GameState.Instance.IsPaused;
        }

        public void PauseGame()
        {
            GameState.Instance.PauseGame();
        }

        public void ResumeGame()
        {
            GameState.Instance.ResumeGame();
        }

        public void SetUIInput(bool value)
        {
            if (value)
            {
                _playerInputAction.UI.Enable();
            }
            else
            {
                _playerInputAction.UI.Disable();
            }
        }
    }
}