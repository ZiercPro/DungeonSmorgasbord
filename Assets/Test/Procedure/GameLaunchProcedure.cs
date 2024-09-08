using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ZiercCode.Test.Base;
using ZiercCode.Test.Data;
using ZiercCode.Test.Locale;
using ZiercCode.Test.Reference;
using ZiercCode.Test.Resources;
using ZiercCode.Test.RuntimeData;
using ZiercCode.Test.StateMachine;

namespace ZiercCode.Test.Procedure
{
    public class GameLaunchProcedure : ProcedureBase
    {
        private LocalizationComponent _localizationComponent;
        private ResourceComponent _resourceComponent;
        private DataComponent _dataComponent;

        public override void OnCreate(IStateMachine stateMachine)
        {
            base.OnCreate(stateMachine);
            //获取组件
            _localizationComponent = GameEntry.GetComponent<LocalizationComponent>();
            _resourceComponent = GameEntry.GetComponent<ResourceComponent>();
            _dataComponent = GameEntry.GetComponent<DataComponent>();
        }

        public override void OnEnter()
        {
            Debug.Log("游戏启动");
            //初始化资源组件
            _resourceComponent.InitializeResource();

            //初始化游戏数据
            _dataComponent.Initialize();

            //初始化Dotween
            DOTween.Init();

            //初始化Addressable
            Addressables.InitializeAsync();

            //初始化本地化组件
            _localizationComponent.InitializeCustomText();

            //设置场景
            GlobalData.nextSceneName = "MainMenuScene";
            GlobalData.nextSceneProcedureType = typeof(GameMainMenuProcedure);
            //设置加载的资源
            GlobalData.nextAssetsLabels = new List<List<string>>();
            GlobalData.nextAssetsLabels.Add(new List<string> { "MainMenuScene" });
        }

        public override void OnUpdate()
        {
            if (_resourceComponent.IsInitialized)
            {
                StateMachine.ChangeState<GameChangingSceneProcedure>();
            }
        }

        public override void OnExit()
        {
            Debug.Log("启动完成");
        }
    }
}