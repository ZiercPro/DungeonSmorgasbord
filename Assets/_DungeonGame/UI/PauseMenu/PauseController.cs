using RMC.Mini;
using RMC.Mini.Controller;

namespace ZiercCode._DungeonGame.UI.PauseMenu
{
    public class PauseController : BaseController<PauseModel, PauseView, PauseService>
    {
        public PauseController(PauseModel model, PauseView view, PauseService service) : base(model, view, service)
        {
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                _view.ContinueButton.onClick.AddListener(OnContinueButtonPressed);
                _view.SettingsButton.onClick.AddListener(OnSettingsButtonPressed);
                _view.MainMenuButton.onClick.AddListener(OnMainMenuButtonPressed);

                Context.CommandManager.AddCommandListener<ExitSettingsCommand>(ExitSettingsCommandCallback);

                _service.OnBackInput.AddListener(OnBackInput);
            }
        }

        private void OnBackInput()
        {
            if (_model.IsPause)
            {
                OnContinueButtonPressed();
            }
            else
            {
                _view.gameObject.SetActive(true);
                _view.CanvasGroupUser.Enable();
                _service.SetTimeScale(0f);
            }
        }

        //按下继续 游戏继续 时间尺度恢复正常 暂停界面退出
        private void OnContinueButtonPressed()
        {
            _view.CanvasGroupUser.Disable();
            _view.gameObject.SetActive(false);
            _service.SetTimeScale(1f);
        }

        //按下设置按钮 当前界面颜色变淡 并禁用该界面
        private void OnSettingsButtonPressed()
        {
            _service.SetUIInput(false);
            _view.CanvasGroupUser.Disable(0.3f);
            Context.CommandManager.InvokeCommand(new OpenSettingsCommand());
        }

        //按下主界面按钮 退出到主界面
        private void OnMainMenuButtonPressed()
        {
            OnContinueButtonPressed();
            SceneComponent.Instance.LoadScene("Scene_MainMenu", true);
        }

        private void ExitSettingsCommandCallback(ExitSettingsCommand command)
        {
            _service.SetUIInput(true);
            _view.gameObject.SetActive(true);
            _view.CanvasGroupUser.Enable();
        }
    }
}