using RMC.Mini;
using RMC.Mini.Controller;
using Unity.VisualScripting;
using UnityEngine;
using ZiercCode._DungeonGame;
using ZiercCode._DungeonGame.UI.Settings;

namespace ZiercCode
{
    public class PauseController : BaseController<Null, PauseView, Null>
    {
        public PauseController(Null model, PauseView view, Null service) : base(model, view, service)
        {
        }

        public override void Initialize(IContext context)
        {
            base.Initialize(context);
            _view.ContinueButton.onClick.AddListener(OnContinueButtonPressed);
            _view.SettingsButton.onClick.AddListener(OnSettingsButtonPressed);
            _view.MainMenuButton.onClick.AddListener(OnMainMenuButtonPressed);
        }

        public override void Dispose()
        {
            _view.ContinueButton.onClick.RemoveListener(OnContinueButtonPressed);
            _view.SettingsButton.onClick.RemoveListener(OnSettingsButtonPressed);
            _view.MainMenuButton.onClick.RemoveListener(OnMainMenuButtonPressed);
        }

        //按下继续 游戏继续 时间尺度恢复正常 暂停界面退出
        private void OnContinueButtonPressed()
        {
            _view.CanvasGroupUser.Disable(0f);
            _view.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

        //按下设置按钮 当前界面颜色变淡 并禁用
        private void OnSettingsButtonPressed()
        {
            _view.CanvasGroupUser.Disable(0.3f);
            Context.CommandManager.InvokeCommand(new OpenSettingsCommand());
        }

        //按下主界面按钮 退出到主界面
        private void OnMainMenuButtonPressed()
        {
            SceneComponent.Instance.LoadScene("Scene_MainMenu");
        }
    }
}