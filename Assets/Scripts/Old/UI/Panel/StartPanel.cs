using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZiercCode.Core.UI;
using ZiercCode.Old.Scene;

namespace ZiercCode.Old.UI.Panel
{
    public class StartPanel : BaseAnimationPanel
    {
        private static readonly string path = "Prefabs/UI/Panel/MainMenu";
        public StartPanel() : base(new UIType(path)) { }

        public override void OnEnter()
        {
            base.OnEnter();
            UITool.GetComponentInChildrenUI<Button>("SettingButton").onClick.AddListener(() =>
            {
                PanelManager.Push(new SettingPanel());
            });


            UITool.GetComponentInChildrenUI<Button>("StartButton").onClick.AddListener(() =>
            {
                //游戏开始
                SceneSystem.SetScene(new LoadScene());
            });


            UITool.GetComponentInChildrenUI<Button>("QuitButton").onClick.AddListener(() =>
            {
                //游戏结束 
                Application.Quit();
            });
        }

        public override void OnPause()
        {
            OutAnimate(1f);
        }

        public override void OnResume()
        {
            InAnimate(1f);
        }

        public override void OnExit()
        {
            base.OnExit();
            UITool.GetComponentInChildrenUI<Button>("SettingButton").onClick.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Button>("StartButton").onClick.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Button>("QuitButton").onClick.RemoveAllListeners();
            UIManager.DestroyUI(UIType);
        }
    }
}