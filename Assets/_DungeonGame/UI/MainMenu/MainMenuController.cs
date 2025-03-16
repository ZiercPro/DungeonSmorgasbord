using RMC.Mini;
using RMC.Mini.Controller;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using ZiercCode._DungeonGame.UI.Settings;
using ZiercCode.Locale;
using EventBus = ZiercCode.Event.EventBus;

namespace ZiercCode._DungeonGame.UI.MainMenu
{
    public class MainMenuController : BaseController<Null, MainMenuView, Null>
    {
        public MainMenuController(MainMenuView view) : base(null, view, null)
        {
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                _view.StartButton.onClick.AddListener(OnStartButtonPressed);
                _view.SettingButton.onClick.AddListener(OnSettingsButtonPressed);
                _view.QuitButton.onClick.AddListener(OnQuitButtonPressed);

                Context.CommandManager.AddCommandListener<OpenSettingsCommand>(OnEnterSettingsCommand);
                Context.CommandManager.AddCommandListener<OpenMainMenuCommand>(OnEnterMainMenuCommand);
            }
        }

        private void OnStartButtonPressed()
        {
            EventBus.Invoke(new SceneEvent.ChangeSceneEvent { NextSceneName = "Scene_Hall" });
        }

        private void OnSettingsButtonPressed()
        {
            Context.CommandManager.InvokeCommand(new OpenSettingsCommand());
        }

        private void OnQuitButtonPressed()
        {
            string message = LocalizationComponent.Instance.GetText("Warning_ExitGame");

            void ConfirmQuit()
            {
#if UNITY_EDITOR

                EditorApplication.ExitPlaymode();
#endif
                Application.Quit();
            }

            void CancelQuit()
            {
                Enable();
            }

            OpenCheckBoxCommand openCheckBoxCommand = new OpenCheckBoxCommand();
            openCheckBoxCommand.Message = message;
            openCheckBoxCommand.ConfirmCallback = ConfirmQuit;
            openCheckBoxCommand.CancelCallback = CancelQuit;

            Context.CommandManager.InvokeCommand(openCheckBoxCommand);

            Disable();
        }

        private void OnEnterMainMenuCommand(OpenMainMenuCommand openMainMenuCommand)
        {
            ShowView();
            Enable();
        }

        private void OnEnterSettingsCommand(OpenSettingsCommand openSettingsCommand)
        {
            Disable();
            HideView();
        }

        private void HideView()
        {
            _view.gameObject.SetActive(false);
        }

        private void ShowView()
        {
            _view.gameObject.SetActive(true);
        }

        private void Enable()
        {
            _view.CanvasGroup.blocksRaycasts = true;
            _view.CanvasGroup.interactable = true;
            _view.CanvasGroup.alpha = 1f;
        }

        private void Disable()
        {
            _view.CanvasGroup.blocksRaycasts = false;
            _view.CanvasGroup.interactable = false;
            _view.CanvasGroup.alpha = 0.3f;
        }
    }
}