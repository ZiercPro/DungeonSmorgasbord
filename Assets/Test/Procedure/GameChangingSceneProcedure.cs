using UnityEngine;
using ZiercCode.Test.Procedure;
using ZiercCode.Test.Scene;

namespace ZiercCode
{
    public class GameChangingSceneProcedure : ProcedureBase
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("enter scene changing!");

            //卸载所有场景
            ZiercScene.UnloadAllActiveScene();

            //进入加载场景
            ZiercScene.LoadScene("LoadingScene");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (ZiercScene.ActiveSceneCount == 1)
            {
                StateMachine.ChangeState<GameMainMenuProcedure>();
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            ZiercScene.UnLoadScene("LoadingScene");
        }
    }
}