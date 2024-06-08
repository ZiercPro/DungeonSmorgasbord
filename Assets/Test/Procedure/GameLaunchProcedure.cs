using UnityEngine;
using ZiercCode.Test.StateMachine;

namespace ZiercCode.Test.Procedure
{
    public class GameLaunchProcedure : ProcedureBase
    {
        public override void OnCreate(IStateMachine stateMachine)
        {
            base.OnCreate(stateMachine);
            Debug.Log("launch procedure created");
        }

        public override void OnEnter()
        {
            Debug.Log("launch game!");
        }

        public override void OnUpdate()
        {
            StateMachine.ChangeState<GameResourceCheckProcedure>();
        }

        public override void OnExit()
        {
            Debug.Log("launch leave!");
        }
    }
}