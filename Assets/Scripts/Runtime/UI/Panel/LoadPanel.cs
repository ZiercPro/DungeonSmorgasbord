using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZiercCode.Runtime.Helper;
using ZiercCode.Runtime.Scene;
using ZiercCode.Runtime.UI.Framework;

namespace ZiercCode.Runtime.UI.Panel
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
    }
}