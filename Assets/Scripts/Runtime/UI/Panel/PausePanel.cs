using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PausePanel : BasePanel
{
    readonly static string path = "Prefabs/UI/Panel/PauseMenu";
    public PausePanel() : base(new UIType(path)) { }
    public override void OnEnter()
    {
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
            GameRoot.Instance.SceneSystem.SetScene(new StartScene());
        });
        GameRoot.Instance.Pause();
    }
    public override void OnPause()
    {
        GameRoot.Instance.Pause();
        CanvasGroup cGroup = UITool.GetOrAddComponent<CanvasGroup>();
        cGroup.interactable = false;
        cGroup.DOFade(0f, 0.5f).SetUpdate(true);

    }
    public override void OnResume()
    {
        CanvasGroup cGroup = UITool.GetOrAddComponent<CanvasGroup>();
        cGroup.DOFade(1f, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            cGroup.interactable = true;
        });

    }
    public override void OnExit()
    {
        UITool.GetComponentInChildrenUI<Button>("BackButton").onClick.RemoveAllListeners();
        UITool.GetComponentInChildrenUI<Button>("SettingButton").onClick.RemoveAllListeners();
        UITool.GetComponentInChildrenUI<Button>("MenuButton").onClick.RemoveAllListeners();
        UIManager.DestroyUI(UIType);
        GameRoot.Instance.Play();
    }
}
