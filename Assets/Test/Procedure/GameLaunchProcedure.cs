using UnityEngine;
using ZiercCode.Test.Resources;
using ZiercCode.Test.StateMachine;

namespace ZiercCode.Test.Procedure
{
    public class GameLaunchProcedure : ProcedureBase
    {
        private ResourceComponent _resourceComponent;

        public override void OnCreate(IStateMachine stateMachine)
        {
            base.OnCreate(stateMachine);
        }

        public override void OnEnter()
        {
            Debug.Log("launch game!");
        }

        public override void OnUpdate()
        {
            StateMachine.ChangeState<GamePreloadProcedure>();
        }

        public override void OnExit()
        {
            Debug.Log("launch leave!");
        }
    }
}