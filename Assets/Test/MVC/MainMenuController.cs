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
        private readonly MainMenuView _mainMenuView;

        public MainMenuController(MainMenuView mainMenuView) : base(null, mainMenuView, null)
        {
            _mainMenuView = mainMenuView;
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                Context.CommandManager.AddCommandListener<EnterMainMenuCommand>(OnEnterMainMenuCommand);
                Context.CommandManager.AddCommandListener<EnterSettingsCommand>(OnEnterSettingsCommand);

                _mainMenuView.onStartButtonPressed.AddListener(OnStartButtonPressed);
                _mainMenuView.onSettingsButtonPressed.AddListener(OnSettingsButtonPressed);
                _mainMenuView.onQuitButtonPressed.AddListener(OnQuitButtonPressed);
            }
        }

        private void OnStartButtonPressed()
        {
            Debug.Log("start button pressed");
            //设置需要加载的资源
            GlobalData.nextAssetsLabels = new();
            GlobalData.nextAssetsLabels.Add(new List<string>() { "GameScene" });
            //广播事件
            ZiercEvent.Invoke(
                new ChangeSceneEvent.StartChangeSceneEvent() { NextSceneProcedureType = typeof(GamePlayProcedure) });
        }

        private void OnSettingsButtonPressed()
        {
            Context.CommandManager.InvokeCommand(new EnterSettingsCommand());
        }

        private void OnQuitButtonPressed()
        {
            Debug.Log("quit button pressed");
        }

        private void OnEnterMainMenuCommand(EnterMainMenuCommand enterMainMenuCommand)
        {
            ShowView();
        }

        private void OnEnterSettingsCommand(EnterSettingsCommand enterSettingsCommand)
        {
            HideView();
        }

        private void HideView()
        {
            _mainMenuView.CanvasGroup.blocksRaycasts = false;
            _mainMenuView.CanvasGroup.interactable = false;
            _mainMenuView.CanvasGroup.alpha = 0f;
            _mainMenuView.gameObject.SetActive(false);
        }

        private void ShowView()
        {
            _mainMenuView.gameObject.SetActive(true);
            _mainMenuView.CanvasGroup.blocksRaycasts = true;
            _mainMenuView.CanvasGroup.interactable = true;
            _mainMenuView.CanvasGroup.alpha = 1f;
        }
    }
}