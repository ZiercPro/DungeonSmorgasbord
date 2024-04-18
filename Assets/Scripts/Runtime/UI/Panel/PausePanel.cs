using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZiercCode.Core.UI;
using ZiercCode.Runtime.Hero;
using ZiercCode.Runtime.Manager;
using ZiercCode.Runtime.Scene;

namespace ZiercCode.Runtime.UI.Panel
{
    public class PausePanel : BasePanel
    {
        readonly static string path = "Prefabs/UI/Panel/PauseMenu";
        public PausePanel() : base(new UIType(path)) { }

        private HeroInputManager _heroInputManager;

        public override void OnEnter()
        {
            BanHeroInput();
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
            Time.timeScale = 1f;
            ReleaseHeroInput();
        }
        public override void OnEsc()
        {
            PanelManager.Pop();
        }
        //禁用玩家输入
        private void BanHeroInput()
        {
            if (GameManager.playerTrans)
            {
                _heroInputManager = GameManager.playerTrans.GetComponent<HeroInputManager>();
                _heroInputManager.enabled = false;
            }
        }
        //放行玩家输入
        private void ReleaseHeroInput()
        {
            _heroInputManager.enabled = true;
            _heroInputManager = null;
        }

    }
}