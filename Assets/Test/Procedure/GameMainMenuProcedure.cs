using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using ZiercCode.Old.Audio;
using ZiercCode.Test.Base;
using ZiercCode.Test.Resources;
using ZiercCode.Test.Scene;

namespace ZiercCode.Test.Procedure
{
    public class GameMainMenuProcedure : ProcedureBase
    {
        AsyncOperation loadingOperation;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("进入主菜单");

            ResourceComponent resourceComponent = GameEntry.GetComponent<ResourceComponent>();

            loadingOperation = ZiercScene.LoadScene("MainMenuScene");
            AudioPlayer.Instance.PlayMusic("MenuBgm");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}