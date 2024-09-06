using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZiercCode.DungeonSmorgasbord.Locale;

namespace ZiercCode.Test.MVC
{
    public class SettingsView : MonoBehaviour, IView
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private Toggle volumePanelToggle;
        [SerializeField] private Toggle otherPanelToggle;
        [SerializeField] private Toggle languagePanelToggle;

        [SerializeField] private RectTransform volumeSettings;
        [SerializeField] private RectTransform otherSettings;
        [SerializeField] private RectTransform languageSettings;

        [SerializeField] private Button backButton;
        [SerializeField] private Button saveButton;

        [SerializeField] private Slider masterVolume;
        [SerializeField] private Slider musicVolume;
        [SerializeField] private Slider sfxVolume;
        [SerializeField] private Slider environmentVolume;

        [SerializeField] private Toggle fps;

        [SerializeField] private TMP_Dropdown languageDropdown;


        public Button BackButton => backButton;
        public Button SaveButton => saveButton;

        public Toggle VolumePanelToggle => volumePanelToggle;
        public Toggle OtherPanelToggle => otherPanelToggle;
        public Toggle LanguagePanelToggle => languagePanelToggle;

        public Toggle Fps => fps;

        public TMP_Dropdown LanguageDropDown => languageDropdown;

        public RectTransform VolumePanel => volumeSettings;
        public RectTransform OtherPanel => otherSettings;
        public RectTransform LanguagePanel => languageSettings;

        public Slider MasterVolume => masterVolume;
        public Slider MusicVolume => musicVolume;
        public Slider SfxVolume => sfxVolume;
        public Slider EnvironmentVolume => environmentVolume;

        public CanvasGroup CanvasGroup => canvasGroup;

        public void RequireIsInitialized()
        {
            if (_isInitialize) Debug.LogWarning($"{name}需要实例化");
        }

        public bool IsInitialized => _isInitialize;
        public IContext Context => _context;

        private bool _isInitialize;
        private IContext _context;

        public void Initialize(IContext context)
        {
            if (!_isInitialize)
            {
                _context = context;

                SettingsModel settingsModel = _context.ModelLocator.GetItem<SettingsModel>();

                settingsModel.VolumePanelToggle.AddListener(OnVolumeToggleChange);
                settingsModel.OtherPanelToggle.AddListener(OnOtherToggleChange);
                settingsModel.LanguagePanelToggle.AddListener(OnLanguageToggleChange);

                settingsModel.FpsToggle.AddListener(OnFpsToggleChange);
                settingsModel.LanguageEnum.AddListener(OnLanguageChange);

                settingsModel.MasterVolume.AddListener(OnMasterVolumeChange);
                settingsModel.MusicVolume.AddListener(OnMusicVolumeChange);
                settingsModel.SfxVolume.AddListener(OnSfxVolumeChange);
                settingsModel.EnvironmentVolume.AddListener(OnEnvironmentVolumeChange);

                _isInitialize = true;
            }
        }

        private void OnFpsToggleChange(bool s)
        {
            fps.isOn = s;
        }

        private void OnLanguageChange(LanguageEnum languageEnum)
        {
            int languageIndex = (int)languageEnum;
            if (languageDropdown.options.Count > languageIndex)
            {
                languageDropdown.value = languageIndex;
            }
            else
            {
                throw new Exception($"超出枚举{typeof(LanguageEnum)}范围");
            }
        }

        private void OnMasterVolumeChange(float value)
        {
            masterVolume.value = value;
        }

        private void OnMusicVolumeChange(float value)
        {
            musicVolume.value = value;
        }

        private void OnSfxVolumeChange(float value)
        {
            sfxVolume.value = value;
        }

        private void OnEnvironmentVolumeChange(float value)
        {
            environmentVolume.value = value;
        }

        private void OnVolumeToggleChange(bool s)
        {
            volumePanelToggle.isOn = s;
        }

        private void OnOtherToggleChange(bool s)
        {
            otherPanelToggle.isOn = s;
        }

        private void OnLanguageToggleChange(bool s)
        {
            languagePanelToggle.isOn = s;
        }
    }
}