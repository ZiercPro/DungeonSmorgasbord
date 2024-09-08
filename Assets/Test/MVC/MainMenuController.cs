using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ZiercCode.Test.Event;
using ZiercCode.Test.Procedure;
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
            Context.CommandManager.InvokeCommand(new OpenSettingsCommand());
        }

        private void OnQuitButtonPressed()
        {
            Debug.Log("quit button pressed");
        }

        private void OnEnterMainMenuCommand(OpenMainMenuCommand openMainMenuCommand)
        {
            ShowView();
        }

        private void OnEnterSettingsCommand(OpenSettingsCommand openSettingsCommand)
        {
            HideView();
        }

        private void HideView()
        {
            _view.CanvasGroup.blocksRaycasts = false;
            _view.CanvasGroup.interactable = false;
            _view.CanvasGroup.alpha = 0f;
            _view.gameObject.SetActive(false);
        }

        private void ShowView()
        {
            _view.gameObject.SetActive(true);
            _view.CanvasGroup.blocksRaycasts = true;
            _view.CanvasGroup.interactable = true;
            _view.CanvasGroup.alpha = 1f;
        }
    }
}