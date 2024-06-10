using UnityEngine;
using ZiercCode.Test.Base;
using ZiercCode.Test.ObjectPool;
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
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (_resourceComponent.IsInitialized)
            {
                Debug.Log(ZiercPool.Get("TestSquare"));
                StateMachine.ChangeState<GameMainMenuProcedure>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("preload completed!");
        }
    }
}