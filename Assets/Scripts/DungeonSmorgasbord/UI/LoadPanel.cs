using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZiercCode.Core.UI;
using ZiercCode.Old.Helper;
using ZiercCode.Old.Scene;

namespace ZiercCode.DungeonSmorgasbord.UI
{

    public class LoadPanel : BasePanel
    {
        private readonly static string path = "Prefabs/UI/Panel/LoadingPanel";
        public LoadPanel() : base(new UIType(path)) { }

        public override void OnEnter()
        {
            Slider bar = UITool.GetComponentInChildrenUI<Slider>("LoadingBar");
            TextMeshProUGUI progress = UITool.GetComponentInChildrenUI<TextMeshProUGUI>("LoadingProgress");

            GameObject tool = new GameObject("AsyncTool");
            AsyncLoadTool com = tool.AddComponent<AsyncLoadTool>();
            com.AsyncLoad(bar, progress, new GameScene(), "Game");
        }

        public override void OnExit()
        {
            base.OnExit();
            UIManager.DestroyUI(UIType);
        }
    }
}