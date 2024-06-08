using UnityEngine;

namespace ZiercCode.Test.Procedure
{
    public class GameResourceCheckProcedure : ProcedureBase
    {
        public override void OnEnter()
        {
            Debug.Log("enter resource check procedure!");
        }

        public override void OnUpdate()
        {
            Debug.Log("game checking resources!");
        }

        public override void OnExit()
        {
        }
    }
}