using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using ZiercCode.DungeonSmorgasbord.Locale;
using ZiercCode.Old.Audio;

namespace ZiercCode.Test.MVC
{
    public class SettingsController : BaseController<SettingsModel, SettingsView, SettingsService>
    {
        private readonly SettingsView _settingsView;
        private readonly SettingsModel _settingsModel;
        private readonly SettingsService _settingsService;

        public SettingsController(SettingsModel settingsModel,
            SettingsView settingsView,
            SettingsService settingsService) : base(settingsModel, settingsView, settingsService)
        {
            _settingsView = settingsView;
            _settingsModel = settingsModel;
            _settingsService = settingsService;
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                _settingsView.BackButton.onClick.AddListener(OnBackButtonPressed);

                _settingsModel.MasterVolume.AddListener(Model_OnMasterVolumeChange);
                _settingsModel.MusicVolume.AddListener(Model_OnMusicVolumeChange);
                _settingsModel.SfxVolume.AddListener(Model_OnSfxVolumeChange);
                _settingsModel.EnvironmentVolume.AddListener(Model_OnEnvironmentVolumeChange);
                _settingsModel.VolumePanelToggle.AddListener(Model_OnVolumeToggleChange);
                _settingsModel.OtherPanelToggle.AddListener(Model_OnOtherToggleChange);
                _settingsModel.LanguagePanelToggle.AddListener(Model_OnLanguageToggleChange);
                _settingsModel.LanguageEnum.AddListener(Model_OnLanguageEnumChange);


                _settingsView.VolumePanelToggle.onValueChanged.AddListener(View_OnVolumeTogglePressed);
                _settingsView.OtherPanelToggle.onValueChanged.AddListener(View_OnOtherTogglePressed);
                _settingsView.LanguagePanelToggle.onValueChanged.AddListener(View_OnLanguageTogglePressed);
                _settingsView.MasterVolume.onValueChanged.AddListener(View_OnMasterVolumeChange);
                _settingsView.MusicVolume.onValueChanged.AddListener(View_OnMusicVolumeChange);
                _settingsView.SfxVolume.onValueChanged.AddListener(View_OnSfxVolumeChange);
                _settingsView.EnvironmentVolume.onValueChanged.AddListener(View_OnEnvironmentVolumeChange);
                _settingsView.LanguageDropDown.onValueChanged.AddListener(View_OnLanguageEnumChange);


                Context.CommandManager.AddCommandListener<EnterSettingsCommand>(OnEnterSettingsCommand);
                Context.CommandManager.AddCommandListener<EnterMainMenuCommand>(OnEnterMainMenuCommand);

                _settingsService.Load();
            }
        }

        private void OnEnterSettingsCommand(EnterSettingsCommand enterSettingsCommand)
        {
            ShowView();

            InitializeChildView();
        }

        private void OnEnterMainMenuCommand(EnterMainMenuCommand enterMainMenuCommand)
        {
            HideView();
        }

        private void OnBackButtonPressed()
        {
            HideView();
            Context.CommandManager.InvokeCommand(new EnterMainMenuCommand());
        }

        private void HideView()
        {
            _settingsView.CanvasGroup.blocksRaycasts = false;
            _settingsView.CanvasGroup.interactable = false;
            _settingsView.CanvasGroup.alpha = 0;
            _settingsView.gameObject.SetActive(false);
        }

        private void ShowView()
        {
            _settingsView.gameObject.SetActive(true);
            _settingsView.CanvasGroup.blocksRaycasts = true;
            _settingsView.CanvasGroup.interactable = true;
            _settingsView.CanvasGroup.alpha = 1;
        }

        private void InitializeChildView()
        {
            _settingsModel.VolumePanelToggle.Value = true;
            _settingsModel.OtherPanelToggle.Value = false;
            _settingsModel.LanguagePanelToggle.Value = false;

            _settingsView.VolumePanel.gameObject.SetActive(true);
            _settingsView.OtherPanel.gameObject.SetActive(false);
            _settingsView.LanguagePanel.gameObject.SetActive(false);
        }

        private void Model_OnVolumeToggleChange(bool s)
        {
            _settingsView.VolumePanel.gameObject.SetActive(s);
        }

        private void Model_OnOtherToggleChange(bool s)
        {
            _settingsView.OtherPanel.gameObject.SetActive(s);
        }

        private void Model_OnLanguageToggleChange(bool s)
        {
            _settingsView.LanguagePanel.gameObject.SetActive(s);
        }

        private void View_OnVolumeTogglePressed(bool s)
        {
            _settingsModel.VolumePanelToggle.Value = s;
        }

        private void View_OnOtherTogglePressed(bool s)
        {
            _settingsModel.OtherPanelToggle.Value = s;
        }

        private void View_OnLanguageTogglePressed(bool s)
        {
            _settingsModel.LanguagePanelToggle.Value = s;
        }

        private void Model_OnMasterVolumeChange(float value)
        {
            AudioPlayer.Instance.SetMasterVolume(value);
        }

        private void Model_OnMusicVolumeChange(float value)
        {
            AudioPlayer.Instance.SetMusicVolume(value);
        }

        private void Model_OnSfxVolumeChange(float value)
        {
            AudioPlayer.Instance.SetSfxVolume(value);
        }

        private void Model_OnEnvironmentVolumeChange(float value)
        {
            AudioPlayer.Instance.SetEnvironmentVolume(value);
        }

        private void View_OnMasterVolumeChange(float value)
        {
            _settingsModel.MasterVolume.Value = value;
        }

        private void View_OnMusicVolumeChange(float value)
        {
            _settingsModel.MusicVolume.Value = value;
        }

        private void View_OnSfxVolumeChange(float value)
        {
            _settingsModel.SfxVolume.Value = value;
        }

        private void View_OnEnvironmentVolumeChange(float value)
        {
            _settingsModel.EnvironmentVolume.Value = value;
        }

        private void Model_OnLanguageEnumChange(LanguageEnum languageEnum)
        {
            LocaleManager.Instance.SetLanguage(languageEnum);
        }

        private void View_OnLanguageEnumChange(int languageIndex)
        {
            _settingsModel.LanguageEnum.Value = (LanguageEnum)languageIndex;
        }
    }
}