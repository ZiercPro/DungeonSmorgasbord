using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI.Panel
{
    using Scene;
    using Helper;
    using UIFramework;

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
    }
}