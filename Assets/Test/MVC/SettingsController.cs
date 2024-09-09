using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Locale;
using ZiercCode.Old.Audio;
using ZiercCode.Test.Base;
using ZiercCode.Test.Locale;
using ZiercCode.Test.Reference;

namespace ZiercCode.Test.MVC
{
    public class SettingsController : BaseController<SettingsModel, SettingsView, SettingsService>
    {
        public SettingsController(SettingsModel settingsModel,
            SettingsView settingsView,
            SettingsService settingsService) : base(settingsModel, settingsView, settingsService)
        {
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                _view.BackButton.onClick.AddListener(OnBackButtonPressed);
                _view.ApplyButton.onClick.AddListener(OnApplyButtonPressed);

                _view.settingsChanged.AddListener(Model_OnSettingsValueStateChange);

                _model.MasterVolume.AddListener(Model_OnMasterVolumeChange);
                _model.MusicVolume.AddListener(Model_OnMusicVolumeChange);
                _model.SfxVolume.AddListener(Model_OnSfxVolumeChange);
                _model.EnvironmentVolume.AddListener(Model_OnEnvironmentVolumeChange);
                _model.VolumePanelToggle.AddListener(Model_OnVolumeToggleChange);
                _model.OtherPanelToggle.AddListener(Model_OnOtherToggleChange);
                _model.LanguagePanelToggle.AddListener(Model_OnLanguageToggleChange);
                _model.LanguageEnum.AddListener(Model_OnLanguageEnumChange);
                _model.FpsToggle.AddListener(Model_OnFpsToggleChange);


                _view.VolumePanelToggle.onValueChanged.AddListener(View_OnVolumeTogglePressed);
                _view.OtherPanelToggle.onValueChanged.AddListener(View_OnOtherTogglePressed);
                _view.LanguagePanelToggle.onValueChanged.AddListener(View_OnLanguageTogglePressed);
                _view.MasterVolume.onValueChanged.AddListener(View_OnMasterVolumeChange);
                _view.MusicVolume.onValueChanged.AddListener(View_OnMusicVolumeChange);
                _view.SfxVolume.onValueChanged.AddListener(View_OnSfxVolumeChange);
                _view.EnvironmentVolume.onValueChanged.AddListener(View_OnEnvironmentVolumeChange);
                _view.LanguageDropdown.onValueChanged.AddListener(View_OnLanguageEnumChange);
                _view.Fps.onValueChanged.AddListener(View_OnFpsChange);


                Context.CommandManager.AddCommandListener<OpenSettingsCommand>(OnEnterSettingsCommand);
                Context.CommandManager.AddCommandListener<OpenMainMenuCommand>(OnEnterMainMenuCommand);

                _service.Load();
            }
        }

        private void OnEnterSettingsCommand(OpenSettingsCommand openSettingsCommand)
        {
            Enter();
        }

        private void OnEnterMainMenuCommand(OpenMainMenuCommand openMainMenuCommand)
        {
            Exit();
        }

        private void OnBackButtonPressed()
        {
            if (!_view.settingsChanged.Value)
            {
                Context.CommandManager.InvokeCommand(ZiercReference.GetReference<OpenMainMenuCommand>());
            }
            else
            {
                string message = GameEntry.GetComponent<LocalizationComponent>().GetText("100");

                void ConfirmAction()
                {
                    Context.CommandManager.InvokeCommand(ZiercReference.GetReference<OpenMainMenuCommand>());
                }

                void CancelAction()
                {
                    _service.Load();
                    Context.CommandManager.InvokeCommand(ZiercReference.GetReference<OpenMainMenuCommand>());
                }

                OpenCheckBoxCommand openCheckBoxCommand = ZiercReference.GetReference<OpenCheckBoxCommand>();
                openCheckBoxCommand.Message = message;
                openCheckBoxCommand.ConfirmCallback = ConfirmAction;
                openCheckBoxCommand.CancelCallback = CancelAction;

                //弹窗提示没有保存
                Context.CommandManager.InvokeCommand(openCheckBoxCommand);

                //禁用界面
                Disable();
            }
        }

        private void OnApplyButtonPressed()
        {
            _service.Save();
            _view.settingsChanged.Value = false;
        }

        private void Enable()
        {
            _view.CanvasGroup.blocksRaycasts = true;
            _view.CanvasGroup.interactable = true;
            _view.CanvasGroup.alpha = 1;
        }

        private void Disable()
        {
            _view.CanvasGroup.blocksRaycasts = false;
            _view.CanvasGroup.interactable = false;
            _view.CanvasGroup.alpha = 0.3f;
        }

        private void HideView()
        {
            _view.gameObject.SetActive(false);
        }

        private void ShowView()
        {
            _view.gameObject.SetActive(true);
        }

        private void Enter()
        {
            ShowView();
            Enable();
            InitializeChildView();

            _view.settingsChanged.Value = false;
            _view.ApplyButton.gameObject.SetActive(false);
        }

        private void Exit()
        {
            _service.Save();
            Disable();
            HideView();
        }

        private void InitializeChildView()
        {
            _model.VolumePanelToggle.Value = true;
            _model.OtherPanelToggle.Value = false;
            _model.LanguagePanelToggle.Value = false;

            _view.VolumeSettings.gameObject.SetActive(true);
            _view.OtherSettings.gameObject.SetActive(false);
            _view.LanguageSettings.gameObject.SetActive(false);
        }

        private void Model_OnSettingsValueStateChange(bool s)
        {
            _view.ApplyButton.gameObject.SetActive(s);
        }

        private void Model_OnFpsToggleChange(bool s)
        {
            _view.Fps.isOn = s;
        }

        private void Model_OnVolumeToggleChange(bool s)
        {
            _view.VolumeSettings.gameObject.SetActive(s);
        }

        private void Model_OnOtherToggleChange(bool s)
        {
            _view.OtherSettings.gameObject.SetActive(s);
        }

        private void Model_OnLanguageToggleChange(bool s)
        {
            _view.LanguageSettings.gameObject.SetActive(s);
        }

        private void View_OnVolumeTogglePressed(bool s)
        {
            _model.VolumePanelToggle.Value = s;
        }

        private void View_OnOtherTogglePressed(bool s)
        {
            _model.OtherPanelToggle.Value = s;
        }

        private void View_OnLanguageTogglePressed(bool s)
        {
            _model.LanguagePanelToggle.Value = s;
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
            _model.MasterVolume.Value = value;
        }

        private void View_OnMusicVolumeChange(float value)
        {
            _model.MusicVolume.Value = value;
        }

        private void View_OnSfxVolumeChange(float value)
        {
            _model.SfxVolume.Value = value;
        }

        private void View_OnEnvironmentVolumeChange(float value)
        {
            _model.EnvironmentVolume.Value = value;
        }

        private void Model_OnLanguageEnumChange(LanguageEnum languageEnum)
        {
            GameEntry.GetComponent<LocalizationComponent>().SetLanguage(languageEnum);
        }

        private void View_OnLanguageEnumChange(int languageIndex)
        {
            _model.LanguageEnum.Value = (LanguageEnum)languageIndex;
        }

        private void View_OnFpsChange(bool s)
        {
            _model.FpsToggle.Value = s;
        }
    }
}