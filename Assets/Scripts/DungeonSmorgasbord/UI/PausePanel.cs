using UnityEngine;
using UnityEngine.UI;
using ZiercCode.Core.UI;
using ZiercCode.Old.Scene;

namespace ZiercCode.DungeonSmorgasbord.UI
{
    public class PausePanel : BaseUIAnimationPanel
    {
        readonly static string path = "Prefabs/UI/Panel/PauseMenu";
        public PausePanel() : base(new UIType(path)) { }

        public override void OnEnter()
        {
            base.OnEnter();
            SetAction(GetBackInputAction(), context => PanelManager.Pop());
            BanPlayerInput();
            UITool.GetComponentInChildrenUI<Button>("BackButton").onClick.AddListener(() =>
            {
                PanelManager.Pop();
            });
            UITool.GetComponentInChildrenUI<Button>("SettingButton").onClick.AddListener(() =>
            {
                PanelManager.Push(new SettingPanel());
            });
            UITool.GetComponentInChildrenUI<Button>("MenuButton").onClick.AddListener(() =>
            {
                SceneSystem.SetScene(new StartScene());
            });
            Time.timeScale = 0f;
        }

        public override void OnPause()
        {
            base.OnPause();
            OutAnimate(1f);
        }

        public override void OnResume()
        {
            base.OnResume();
            InAnimate(1f);
        }

        public override void OnExit()
        {
            base.OnExit();
            UITool.GetComponentInChildrenUI<Button>("BackButton").onClick.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Button>("SettingButton").onClick.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Button>("MenuButton").onClick.RemoveAllListeners();
            Time.timeScale = 1f;
            ReleasePlayerInput();
            UIManager.DestroyUI(UIType);
        }
    }
}