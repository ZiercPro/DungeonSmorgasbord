using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using ZiercCode.Test.Procedure;
using ZiercCode.Test.Scene;

namespace ZiercCode
{
    public class GameChangingSceneProcedure : ProcedureBase
    {
        private AsyncOperationHandle<SceneInstance> _loadingOperation;

        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("进入场景转换流程");

            //卸载所有场景
            ZiercScene.UnloadAllActiveScene();

            //进入加载场景
            _loadingOperation = ZiercScene.LoadScene("LoadingScene");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (_loadingOperation.IsDone)
            {
                StateMachine.ChangeState<GamePreloadAssetsProcedure>();//todo 不需要这个流程
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}