using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using ZiercCode.Old.Audio;
using ZiercCode.Test.Event;
using ZiercCode.Test.RuntimeData;
using ZiercCode.Test.Scene;
using ZiercCode.Test.StateMachine;

namespace ZiercCode.Test.Procedure
{
    public class GameMainMenuProcedure : ProcedureBase
    {
        private EventGroup _eventGroup;
        private AsyncOperationHandle<SceneInstance> _loadingOperation;

        public override void OnCreate(IStateMachine stateMachine)
        {
            base.OnCreate(stateMachine);
            _eventGroup = new EventGroup();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _eventGroup.AddListener<SceneEvent.ChangeSceneEvent>(OnChangeScene);
            AudioPlayer.Instance.PlayMusic("MenuBgm");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public void OnChangeScene(IEventArgs args)
        {
            if (args is SceneEvent.ChangeSceneEvent a)
            {
                //设置下一个场景
                GlobalData.nextSceneProcedureType = a.NextSceneProcedureType;
                GlobalData.nextSceneName = a.NextSceneName;

                StateMachine.ChangeState<GameChangingSceneProcedure>();
            }
        }

        public override void OnExit()
        {
            _eventGroup.RemoveAllListener();
        }
    }
}