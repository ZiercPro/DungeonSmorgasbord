using RMC.Mini;
using UnityEngine;
using ZiercCode._DungeonGame.UI.CheckBox;
using ZiercCode._DungeonGame.UI.MainMenu;
using ZiercCode._DungeonGame.UI.Settings;

namespace ZiercCode._DungeonGame.UI
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
        private MainMenuService _mainMenuService;
        private CheckBoxService _checkBoxService;

        private IContext _context;

        private void OnDestroy()
        {
            _mainMenuController.Dispose();
            _settingsController.Dispose();
            _checkBoxController.Dispose();

            _settingsService.Dispose();
            _mainMenuService.Dispose();
            _checkBoxService.Dispose();
        }

        private void Awake()
        {
            _context = new Context();
            //model
            _settingsModel = new SettingsModel();
            _checkBoxModel = new CheckBoxModel();

            //service
            _settingsService = new SettingsService();
            _mainMenuService = new MainMenuService();
            _checkBoxService = new CheckBoxService();

            //controller
            _settingsController = new(_settingsModel, settingsView, _settingsService);
            _checkBoxController = new(_checkBoxModel, checkBoxView, _checkBoxService);
            _mainMenuController = new(mainMenuView, _mainMenuService);
        }

        private void Start()
        {
            _checkBoxModel.Initialize(_context);
            _settingsModel.Initialize(_context);

            _settingsService.Initialize(_context);
            _checkBoxService.Initialize(_context);
            _mainMenuService.Initialize(_context);

            mainMenuView.Initialize(_context);
            settingsView.Initialize(_context);
            checkBoxView.Initialize(_context);

            _settingsController.Initialize(_context);
            _mainMenuController.Initialize(_context);
            _checkBoxController.Initialize(_context);
        }
    }
}