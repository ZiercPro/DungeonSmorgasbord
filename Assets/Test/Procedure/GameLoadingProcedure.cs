using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using ZiercCode.Test.RuntimeData;
using ZiercCode.Test.Scene;

namespace ZiercCode.Test.Procedure
{
    public class GameLoadingProcedure : ProcedureBase
    {
        private AsyncOperationHandle<SceneInstance> _loadingOperation;

        public override void OnEnter()
        {
            base.OnEnter();

            //加载目标场景
            _loadingOperation = ZiercScene.LoadScene(GlobalData.nextSceneName);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (_loadingOperation.IsDone)
            {
                StateMachine.ChangeState(GlobalData.nextSceneProcedureType);
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            //卸载加载场景
            ZiercScene.UnloadScene("LoadingScene");
        }
    }
}