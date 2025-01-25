using UnityEngine;
using ZiercCode.Old.Audio;

namespace ZiercCode.Test.Procedure
{
    public class GamePlayProcedure : ProcedureBase
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("进入游戏场景");

            AudioPlayer.Instance.PlayMusic("IdleBgm");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();

            Debug.Log("离开游戏场景");
        }
    }
}