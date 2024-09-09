using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ZiercCode.Test.Base;
using ZiercCode.Test.Event;
using ZiercCode.Test.Locale;
using ZiercCode.Test.Procedure;
using ZiercCode.Test.Reference;
using ZiercCode.Test.RuntimeData;

namespace ZiercCode.Test.MVC
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

                Context.CommandManager.AddCommandListener<OpenMainMenuCommand>(OnEnterMainMenuCommand);
                Context.CommandManager.AddCommandListener<OpenSettingsCommand>(OnEnterSettingsCommand);

                _view.StartButton.onClick.AddListener(OnStartButtonPressed);
                _view.SettingButton.onClick.AddListener(OnSettingsButtonPressed);
                _view.QuitButton.onClick.AddListener(OnQuitButtonPressed);
            }
        }

        private void OnStartButtonPressed()
        {
            //设置需要加载的资源
            GlobalData.nextAssetsLabels = new();
            GlobalData.nextAssetsLabels.Add(new List<string>() { "GameScene" });
            //广播事件
            ZiercEvent.Invoke(
                new SceneEvent.ChangeSceneEvent()
                {
                    NextSceneProcedureType = typeof(GamePlayProcedure), NextSceneName = "GameScene"
                });
        }

        private void OnSettingsButtonPressed()
        {
            Context.CommandManager.InvokeCommand(ZiercReference.GetReference<OpenSettingsCommand>());
        }

        private void OnQuitButtonPressed()
        {
            string message = GameEntry.GetComponent<LocalizationComponent>().GetText("101");

            void ConfirmQuit()
            {
                Enable();
            }

            void CancelQuit()
            {
                Enable();
            }

            OpenCheckBoxCommand openCheckBoxCommand = ZiercReference.GetReference<OpenCheckBoxCommand>();
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