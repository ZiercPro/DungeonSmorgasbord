using UnityEngine;
using ZiercCode.Test.Procedure;
using ZiercCode.Test.RuntimeData;
using ZiercCode.Test.Scene;

namespace ZiercCode
{
    public class GameChangingSceneProcedure : ProcedureBase
    {
        AsyncOperation loadingOperation;

        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("进入场景转换流程");

            //卸载所有场景
            ZiercScene.UnloadAllActiveScene();

            //进入加载场景
            loadingOperation = ZiercScene.LoadScene("LoadingScene");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (loadingOperation.isDone)
            {
                StateMachine.ChangeState(GlobalData.nextSceneProcedureType);
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            ZiercScene.UnLoadScene("LoadingScene");
        }
    }
}