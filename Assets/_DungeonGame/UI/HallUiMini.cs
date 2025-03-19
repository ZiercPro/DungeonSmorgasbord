using RMC.Mini;
using System;
using UnityEngine;
using ZiercCode._DungeonGame.UI.CheckBox;
using ZiercCode._DungeonGame.UI.PauseMenu;
using ZiercCode._DungeonGame.UI.Settings;

namespace ZiercCode._DungeonGame.UI
{
    public class HallUiMini : MonoBehaviour
    {
        [SerializeField] private PauseView pauseView;
        [SerializeField] private SettingsView settingsView;
        [SerializeField] private CheckBoxView checkBoxView;

        private PauseModel _pauseModel;
        private SettingsModel _settingsModel;
        private CheckBoxModel _checkBoxModel;

        private PauseService _pauseService;
        private SettingsService _settingsService;
        private CheckBoxService _checkBoxService;

        private PauseController _pauseController;
        private SettingsController _settingsController;
        private CheckBoxController _checkBoxController;

        private IContext _context;

        private void OnDestroy()
        {
            _pauseController.Dispose();
            _settingsController.Dispose();
            _checkBoxController.Dispose();

            _pauseService.Dispose();
            _settingsService.Dispose();
            _checkBoxService.Dispose();
        }

        private void Awake()
        {
            _pauseModel = new PauseModel();
            _settingsModel = new SettingsModel();
            _checkBoxModel = new CheckBoxModel();

            _pauseService = new PauseService();
            _settingsService = new SettingsService();
            _checkBoxService = new CheckBoxService();

            _pauseController = new PauseController(_pauseModel, pauseView, _pauseService);
            _settingsController = new SettingsController(_settingsModel, settingsView, _settingsService);
            _checkBoxController = new CheckBoxController(_checkBoxModel, checkBoxView, _checkBoxService);
        }


        private void Start()
        {
            _context = new Context();

            _pauseModel.Initialize(_context);
            _settingsModel.Initialize(_context);
            _checkBoxModel.Initialize(_context);

            _pauseService.Initialize(_context);
            _settingsService.Initialize(_context);
            _checkBoxService.Initialize(_context);

            pauseView.Initialize(_context);
            settingsView.Initialize(_context);
            checkBoxView.Initialize(_context);

            _pauseController.Initialize(_context);
            _settingsController.Initialize(_context);
            _checkBoxController.Initialize(_context);
        }
    }
}