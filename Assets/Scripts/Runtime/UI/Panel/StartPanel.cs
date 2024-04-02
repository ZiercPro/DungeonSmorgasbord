using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZiercCode.Runtime.Manager;
using ZiercCode.Runtime.Scene;
using ZiercCode.Runtime.UI.Framework;

namespace ZiercCode.Runtime.UI.Panel
{
    public class StartPanel : BasePanel
    {
        private static readonly string path = "Prefabs/UI/Panel/MainMenu";
        public StartPanel() : base(new UIType(path)) { }

        public override void OnEnter()
        {
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
            CanvasGroup cgroup = UITool.GetOrAddComponent<CanvasGroup>();
            cgroup.interactable = false;
            cgroup.DOFade(0, 0.5f);
        }

        public override void OnResume()
        {
            CanvasGroup cgroup = UITool.GetOrAddComponent<CanvasGroup>();
            cgroup.DOFade(1, 0.5f).OnComplete(() =>
            {
                cgroup.interactable = true;
            });
        }

        public override void OnExit()
        {
            UITool.GetComponentInChildrenUI<Button>("SettingButton").onClick.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Button>("StartButton").onClick.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Button>("QuitButton").onClick.RemoveAllListeners();
            UIManager.DestroyUI(UIType);
        }
    }
}