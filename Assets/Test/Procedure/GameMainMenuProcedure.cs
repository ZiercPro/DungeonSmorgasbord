using UnityEditor.Rendering;
using UnityEngine;
using ZiercCode.Old.Audio;
using ZiercCode.Test.Event;
using ZiercCode.Test.RuntimeData;
using ZiercCode.Test.Scene;
using ZiercCode.Test.StateMachine;

namespace ZiercCode.Test.Procedure
{
    public class GameMainMenuProcedure : ProcedureBase
    {
        EventGroup eventGroup;

        public override void OnCreate(IStateMachine stateMachine)
        {
            base.OnCreate(stateMachine);
            eventGroup = new EventGroup();
        }

        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("进入主菜单");
            eventGroup.AddListener<ChangeSceneEvent.StartChangeSceneEvent>(OnStartGame);

            ZiercScene.LoadScene("MainMenuScene");
            AudioPlayer.Instance.PlayMusic("MenuBgm");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public void OnStartGame(IEventArgs args)
        {
            if (args is ChangeSceneEvent.StartChangeSceneEvent a)
            {
                //设置下一个场景
                GlobalData.nextSceneProcedureType = a.NextSceneProcedureType;

                StateMachine.ChangeState<GamePreloadProcedure>();
            }
        }

        public override void OnExit()
        {
            eventGroup.RemoveAllListener();
        }
    }
}