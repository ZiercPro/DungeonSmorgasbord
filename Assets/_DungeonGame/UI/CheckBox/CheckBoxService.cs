using RMC.Mini;
using RMC.Mini.Service;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ZiercCode._DungeonGame.UI.CheckBox
{
    public class CheckBoxService : BaseService
    {
        private PlayerInputAction _playerInputAction;

        public UnityEvent OnBackInput;

        public CheckBoxService()
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
            _playerInputAction.UI.Disable();
            _playerInputAction.UI.Back.started -= BackInput;
        }

        private void BackInput(InputAction.CallbackContext context)
        {
            OnBackInput?.Invoke();
        }
        
        public void SetUIInput(bool value)
        {
            if (value)
                _playerInputAction.UI.Enable();
            else _playerInputAction.UI.Disable();
        }
    }
}