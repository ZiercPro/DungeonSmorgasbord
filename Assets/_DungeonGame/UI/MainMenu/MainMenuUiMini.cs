using RMC.Mini;
using UnityEngine;
using ZiercCode._DungeonGame.UI.Settings;
using ZiercCode.Test.MVC;

namespace ZiercCode._DungeonGame.UI.MainMenu
{
    public class MainMenuUiMini : MonoBehaviour
    {
        [Space] [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private SettingsView settingsView;
        [SerializeField] private CheckBoxView checkBoxView;

        private MainMenuController _mainMenuController;
        private SettingsController _settingsController;
        private CheckBoxController _checkBoxController;

        private SettingsModel _settingsModel;
        private CheckBoxModel _checkBoxModel;

        private SettingsService _settingsService;

        private IContext _context;

        private PlayerInputAction _playerInputAction;

        private void OnEnable()
        {
            _playerInputAction.UI.Enable();
        }

        private void OnDisable()
        {
            _playerInputAction.UI.Disable();
        }

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();

            _context = new Context();
            //model
            _checkBoxModel = new CheckBoxModel();
            _settingsModel = new SettingsModel();

            //service
            _settingsService = new SettingsService(settingsView);

            //controller
            _settingsController = new(_settingsModel, settingsView, _settingsService);
            _checkBoxController = new CheckBoxController(_checkBoxModel, checkBoxView);
            _mainMenuController = new(mainMenuView);
        }

        private void Start()
        {
            _settingsModel.Initialize(_context);
            _checkBoxModel.Initialize(_context);

            mainMenuView.Initialize(_context);
            settingsView.Initialize(_context);
            checkBoxView.Initialize(_context);

            _settingsService.Initialize(_context);

            _settingsController.Initialize(_context);
            _mainMenuController.Initialize(_context);
            _checkBoxController.Initialize(_context);
        }
    }
}