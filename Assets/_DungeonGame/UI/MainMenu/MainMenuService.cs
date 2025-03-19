using RMC.Mini;
using RMC.Mini.Service;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using ZiercCode.Locale;

namespace ZiercCode._DungeonGame.UI.MainMenu
{
    public class MainMenuService : BaseService
    {
        public UnityEvent OnBackInput;

        private PlayerInputAction _playerInputAction;

        public MainMenuService()
        {
            OnBackInput = new UnityEvent();
            _playerInputAction = new PlayerInputAction();
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

        public void SetUIInput(bool value)
        {
            if (value)
                _playerInputAction.UI.Enable();
            else _playerInputAction.UI.Disable();
        }

        private void BackInput(InputAction.CallbackContext context)
        {
            OnBackInput?.Invoke();
        }


        public string GetLocaleString(string key)
        {
            return LocalizationComponent.Instance.GetText(key);
        }

        public void LoadScene(string sceneName)
        {
            SceneComponent.Instance.LoadScene(sceneName, true);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR

            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
    }
}