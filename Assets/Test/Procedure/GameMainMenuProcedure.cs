using UnityEngine;
using ZiercCode.Old.Audio;
using ZiercCode.Test.Scene;

namespace ZiercCode.Test.Procedure
{
    public class GameMainMenuProcedure : ProcedureBase
    {
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("enter mainMenu!");

            ZiercScene.LoadScene("MainMenuScene");

            AudioPlayer.Instance.PlayAudio(AudioName.MenuBgm);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}