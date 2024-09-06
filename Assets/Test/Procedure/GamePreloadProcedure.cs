using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using ZiercCode.Test.Base;
using ZiercCode.Test.Resources;

namespace ZiercCode.Test.Procedure
{
    public class GamePreloadProcedure : ProcedureBase
    {
        private List<AsyncOperationHandle> _handles;
        private float startMoment;

        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("进入预加载流程");

            _handles = new List<AsyncOperationHandle>();

            startMoment = Time.time;

            //加载资源
            ResourceComponent resourceComponent = GameEntry.GetComponent<ResourceComponent>();
            _handles.Add(resourceComponent.LoadAssets("MainMenuScene"));

            Debug.Log("资源加载中...");
        }


        public override void OnUpdate()
        {
            base.OnUpdate();

            if (IsPreloadDone())
            {
                StateMachine.ChangeState<GameChangingSceneProcedure>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log($"预加载完毕，用时{Time.time - startMoment}");
        }

        private bool IsPreloadDone()
        {
            foreach (var handle in _handles)
            {
                if (!handle.IsDone)
                {
                    return false;
                }
            }

            return true;
        }
    }
}