using UnityEngine;
using UnityEngine.UI;
using ZiercCode.Core.UI;
using ZiercCode.Old.Scene;

namespace ZiercCode.Old.UI.Panel
{
    public class DeadPanel : BaseAnimationPanel
    {
        public readonly static string path = "Prefabs/UI/Panel/DeadPanel";
        public DeadPanel() : base(new UIType(path)) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Button reStartButton = UITool.GetComponentInChildrenUI<Button>("ReStartButton");
            reStartButton.onClick.AddListener(() =>
            {
                //重新开始
                Debug.Log("restart");
            });
            Button mainMenuButton = UITool.GetComponentInChildrenUI<Button>("MenuButton");
            mainMenuButton.onClick.AddListener(() =>
            {
                SceneSystem.SetScene(new StartScene());
            });
        }

        public override void OnExit()
        {
            base.OnExit();
            UIManager.DestroyUI(UIType);
        }
        
    }
}