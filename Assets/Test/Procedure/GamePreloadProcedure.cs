using DG.Tweening;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Manager;
using ZiercCode.Test.Base;
using ZiercCode.Test.Resources;

namespace ZiercCode.Test.Procedure
{
    public class GamePreloadProcedure : ProcedureBase
    {
        private ResourceComponent _resourceComponent;

        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("enter preload!");

            _resourceComponent = GameEntry.GetComponent<ResourceComponent>();
            _resourceComponent.InitializeResource();

            DataManager.Initialize();

            DOTween.Init();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (_resourceComponent.IsInitialized)
            {
                StateMachine.ChangeState<GameChangingSceneProcedure>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("preload completed!");
        }
    }
}