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

                Context.CommandManager.AddCommandListener<OpenMainMenuCommand>(OpenMainMenuCommand);
            }
        }

        //按下开始游戏 通过事件通知
        private void OnStartButtonPressed()
        {
            EventBus.Invoke(new SceneEvent.ChangeSceneEvent { NextSceneName = "Scene_Hall" });
        }

        //按下设置 禁用自身
        private void OnSettingsButtonPressed()
        {
            _view.CanvasGroupUser.Disable(0.3f);
            Context.CommandManager.InvokeCommand(new OpenSettingsCommand());
        }

        //按下退出 先禁用主菜单 并弹出确认对话框
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
                _view.CanvasGroupUser.Enable();
            }

            OpenCheckBoxCommand openCheckBoxCommand = new();
            openCheckBoxCommand.Message = message;
            openCheckBoxCommand.ConfirmCallback = ConfirmQuit;
            openCheckBoxCommand.CancelCallback = CancelQuit;

            Context.CommandManager.InvokeCommand(openCheckBoxCommand);

            _view.CanvasGroupUser.Disable(0.3f);
        }

        private void OpenMainMenuCommand(OpenMainMenuCommand openMainMenuCommand)
        {
            _view.gameObject.SetActive(true);
            _view.CanvasGroupUser.Enable();
        }
    }
}