using RMC.Mini;
using UnityEngine;
using ZiercCode.Core.Utilities;

namespace ZiercCode.Test.MVC
{
    public class UiMini : USingleton<UiMini>
    {
        [SerializeField] private GameObject startView;
        [Space] [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private SettingsView settingsView;
        [SerializeField] private GameView gameView;
        [SerializeField] private CheckBoxView checkBoxView;

        private MainMenuController _mainMenuController;
        private SettingsController _settingsController;
        private GameController _gameController;
        private CheckBoxController _checkBoxController;

        private SettingsModel _settingsModel;
        private CheckBoxModel _checkBoxModel;
        private GameModel _gameModel;

        private SettingsService _settingsService;

        private IContext _context;

        protected override void Awake()
        {
            base.Awake();
            _context = new Context();
            _gameModel = new GameModel();
            _checkBoxModel = new CheckBoxModel();
            _settingsModel = new SettingsModel();
            _settingsService = new SettingsService(settingsView);

            _settingsController = new(_settingsModel, settingsView, _settingsService);
            _checkBoxController = new CheckBoxController(_checkBoxModel, checkBoxView);
            _mainMenuController = new(mainMenuView);
            _gameController = new GameController(_gameModel, gameView);
        }

        private void Start()
        {
            _settingsModel.Initialize(_context);
            _checkBoxModel.Initialize(_context);
            _gameModel.Initialize(_context);

            mainMenuView.Initialize(_context);
            settingsView.Initialize(_context);
            gameView.Initialize(_context);
            checkBoxView.Initialize(_context);

            _settingsService.Initialize(_context);

            _settingsController.Initialize(_context);
            _mainMenuController.Initialize(_context);
            _gameController.Initialize(_context);
            _checkBoxController.Initialize(_context);

            startView.gameObject.SetActive(true);
            // _context.CommandManager.InvokeCommand(ZiercReference.GetReference<EnterMainMenuCommand>());
        }

        private void Update()
        {
            //监听UI输入
        }
    }
}