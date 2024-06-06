using RMC.Core.Architectures.Mini.Context;
using UnityEngine;
using ZiercCode.Core.Utilities;

namespace ZiercCode.Test.MVC
{
    public class UiMini : USingleton<UiMini>
    {
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private SettingsView settingsView;

        private MainMenuController _mainMenuController;
        private SettingsController _settingsController;

        private SettingsModel _settingsModel;

        private SettingsService _settingsService;

        private IContext _context;

        protected override void Awake()
        {
            base.Awake();
            _context = new Context();
            _settingsModel = new SettingsModel();
            _settingsService = new SettingsService();

            _settingsController = new(_settingsModel, settingsView, _settingsService);
            _mainMenuController = new(mainMenuView);
        }

        private void Start()
        {
            _settingsModel.Initialize(_context);

            mainMenuView.Initialize(_context);
            settingsView.Initialize(_context);

            _settingsService.Initialize(_context);

            _settingsController.Initialize(_context);
            _mainMenuController.Initialize(_context);
        }
    }
}