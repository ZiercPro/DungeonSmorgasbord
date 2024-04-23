using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZiercCode.Core.Extend;
using ZiercCode.Core.UI;
using ZiercCode.Old.Scene;

namespace ZiercCode.DungeonSmorgasbord.UI
{
    public class LoadPanel : BasePanel
    {
        private readonly static string path = "Prefabs/UI/Panel/LoadingPanel";
        public LoadPanel() : base(new UIType(path)) { }

        private AsyncOperation _loadAsync;

        public override void OnEnter()
        {
            base.OnEnter();
            GameScene gameSceneState = new();
            SceneSystem.SetScene(gameSceneState);
            _loadAsync = gameSceneState.AsyncOperation;
            MyCoroutineTool.Instance.StartMyCor(UpdateLoadingBar());
        }

        public override void OnExit()
        {
            base.OnExit();
            UIManager.DestroyUI(UIType);
        }

        private IEnumerator UpdateLoadingBar()
        {
            Slider bar = UITool.GetComponentInChildrenUI<Slider>("LoadingBar");
            TextMeshProUGUI progress = UITool.GetComponentInChildrenUI<TextMeshProUGUI>("LoadingProgress");
            while (!_loadAsync.isDone)
            {
                float visualValue = _loadAsync.progress + 0.1f;
                bar.value = visualValue;
                progress.text = ((int)(visualValue * 100f)).ToString() + '%';
                yield return null;
            }
        }
    }
}