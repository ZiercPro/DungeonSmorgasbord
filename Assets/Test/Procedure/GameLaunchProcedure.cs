using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ZiercCode.DungeonSmorgasbord.Manager;
using ZiercCode.Test.Base;
using ZiercCode.Test.Resources;
using ZiercCode.Test.RuntimeData;
using ZiercCode.Test.StateMachine;

namespace ZiercCode.Test.Procedure
{
    public class GameLaunchProcedure : ProcedureBase
    {
        private ResourceComponent _resourceComponent;
        private DataComponent _dataComponent;

        public override void OnCreate(IStateMachine stateMachine)
        {
            base.OnCreate(stateMachine);
        }

        public override void OnEnter()
        {
            Debug.Log("游戏启动");

            //获取组件
            _resourceComponent = GameEntry.GetComponent<ResourceComponent>();
            _dataComponent = GameEntry.GetComponent<DataComponent>();
            //初始化资源组件
            _resourceComponent.InitializeResource();

            //初始化游戏数据
            _dataComponent.Initialize();

            //初始化Dotween
            DOTween.Init();

            //初始化Addressable
            Addressables.InitializeAsync();
            
            
            //设置场景
            GlobalData.nextSceneProcedureType = typeof(GameMainMenuProcedure);

        }

        public override void OnUpdate()
        {
            if (_resourceComponent.IsInitialized)
            {
                StateMachine.ChangeState<GamePreloadProcedure>();
            }
        }

        public override void OnExit()
        {
            Debug.Log("启动完成");
        }
    }
}