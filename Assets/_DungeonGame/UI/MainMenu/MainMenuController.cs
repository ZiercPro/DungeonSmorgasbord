using RMC.Mini;
using RMC.Mini.Controller;
using Unity.VisualScripting;


namespace ZiercCode._DungeonGame.UI.MainMenu
{
    public class MainMenuController : BaseController<Null, MainMenuView, MainMenuService>
    {
        public MainMenuController(MainMenuView view, MainMenuService service) : base(null, view, service)
        {
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                _view.StartButton.onClick.AddListener(OnStartButtonPressed);
                _view.SettingButton.onClick.AddListener(OnSettingsButtonPressed);
                _view.QuitButton.onClick.AddListener(OnQuitButtonPressed);

                Context.CommandManager.AddCommandListener<ExitSettingsCommand>(ExitSettingsCommandCallBack);

                _service.OnBackInput.AddListener(OnQuitButtonPressed); //回退快捷键
            }
        }

        //按下开始游戏 通过事件通知
        private void OnStartButtonPressed()
        {
            _service.LoadScene("Scene_Hall");
        }

        //按下设置 禁用自身
        private void OnSettingsButtonPressed()
        {
            _service.SetUIInput(false);
            _view.CanvasGroupUser.Disable(0.3f);
            Context.CommandManager.InvokeCommand(new OpenSettingsCommand());
        }

        //按下退出 先禁用主菜单 并弹出确认对话框
        private void OnQuitButtonPressed()
        {
            string message = _service.GetLocaleString("Warning_ExitGame");

            void ConfirmQuit()
            {
                _service.QuitGame();
            }

            void CancelQuit()
            {
                _view.CanvasGroupUser.Enable();
            }

            Context.CommandManager.InvokeCommand(new OpenCheckBoxCommand
            {
                Message = message, ConfirmCallback = ConfirmQuit, CancelCallback = CancelQuit
            });

            _view.CanvasGroupUser.Disable(0.3f);
        }

        private void ExitSettingsCommandCallBack(ExitSettingsCommand openMainMenuCommand)
        {
            _service.SetUIInput(true);
            _view.gameObject.SetActive(true);
            _view.CanvasGroupUser.Enable();
        }
    }
}