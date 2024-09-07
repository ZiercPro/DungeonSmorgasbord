using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using ZiercCode.Test.Base;
using ZiercCode.Test.Resources;
using ZiercCode.Test.RuntimeData;
using ZiercCode.Test.StateMachine;

namespace ZiercCode.Test.Procedure
{
    public class GamePreloadAssetsProcedure : ProcedureBase
    {
        private List<AsyncOperationHandle> _handles;
        private float startMoment;

        public override void OnCreate(IStateMachine stateMachine)
        {
            base.OnCreate(stateMachine);
            _handles = new List<AsyncOperationHandle>();
        }

        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("进入预加载流程");

            Debug.Log("资源卸载中...");

            startMoment = Time.time;

            UnLoad();

            Debug.Log($"卸载完毕，用时{Time.time - startMoment}");

            startMoment = Time.time;

            Preload();

            Debug.Log("资源加载中...");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (IsPreloadDone())
            {
                StateMachine.ChangeState<GameLoadingProcedure>();
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

        private void Preload()
        {
            //加载资源
            ResourceComponent resourceComponent = GameEntry.GetComponent<ResourceComponent>();

            foreach (var labels in GlobalData.nextAssetsLabels)
            {
                _handles.Add(resourceComponent.LoadAssets(labels, null, Addressables.MergeMode.Intersection));
            }
        }

        private void UnLoad()
        {
            //卸载之前的资源
            ResourceComponent resourceComponent = GameEntry.GetComponent<ResourceComponent>();

            resourceComponent.ReleaseAllLoadedAssets();
        }
    }
}