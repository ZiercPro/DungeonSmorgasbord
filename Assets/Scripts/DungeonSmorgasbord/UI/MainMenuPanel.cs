using UnityEngine;
using UnityEngine.UI;
using ZiercCode.Core.UI;
using ZiercCode.Old.Scene;

namespace ZiercCode.DungeonSmorgasbord.UI
{
    public class MainMenuPanel : BaseUIAnimationPanel
    {
        private static readonly string path = "Prefabs/UI/Panel/MainMenu";
        public MainMenuPanel() : base(new UIType(path)) { }

        public override void OnEnter()
        {
            base.OnEnter();
            UITool.GetComponentInChildrenUI<Button>("SettingButton").onClick
                .AddListener(() => PanelManager.Push(new SettingPanel()));


            UITool.GetComponentInChildrenUI<Button>("StartButton").onClick.AddListener(() =>
                SceneSystem.SetScene(new LoadingScene()));

            UITool.GetComponentInChildrenUI<Button>("QuitButton").onClick.AddListener(() => Application.Quit());
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