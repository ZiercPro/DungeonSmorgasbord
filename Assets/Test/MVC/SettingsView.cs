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
        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }

        [field: SerializeField] public Toggle VolumePanelToggle { get; private set; }
        [field: SerializeField] public Toggle OtherPanelToggle { get; private set; }
        [field: SerializeField] public Toggle LanguagePanelToggle { get; private set; }

        [field: SerializeField] public RectTransform VolumeSettings { get; private set; }
        [field: SerializeField] public RectTransform OtherSettings { get; private set; }
        [field: SerializeField] public RectTransform LanguageSettings { get; private set; }

        [field: SerializeField] public Button BackButton { get; private set; }
        [field: SerializeField] public Button SaveButton { get; private set; }

        [field: SerializeField] public Slider MasterVolume { get; private set; }
        [field: SerializeField] public Slider MusicVolume { get; private set; }
        [field: SerializeField] public Slider SfxVolume { get; private set; }
        [field: SerializeField] public Slider EnvironmentVolume { get; private set; }

        [field: SerializeField] public Toggle Fps { get; private set; }

        [field: SerializeField] public TMP_Dropdown LanguageDropdown { get; private set; }

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
            Fps.isOn = s;
        }

        private void OnLanguageChange(LanguageEnum languageEnum)
        {
            int languageIndex = (int)languageEnum;
            if (LanguageDropdown.options.Count > languageIndex)
            {
                LanguageDropdown.value = languageIndex;
            }
            else
            {
                throw new Exception($"超出枚举{typeof(LanguageEnum)}范围");
            }
        }

        private void OnMasterVolumeChange(float value)
        {
            MasterVolume.value = value;
        }

        private void OnMusicVolumeChange(float value)
        {
            MusicVolume.value = value;
        }

        private void OnSfxVolumeChange(float value)
        {
            SfxVolume.value = value;
        }

        private void OnEnvironmentVolumeChange(float value)
        {
            EnvironmentVolume.value = value;
        }

        private void OnVolumeToggleChange(bool s)
        {
            VolumePanelToggle.isOn = s;
        }

        private void OnOtherToggleChange(bool s)
        {
            OtherPanelToggle.isOn = s;
        }

        private void OnLanguageToggleChange(bool s)
        {
            LanguagePanelToggle.isOn = s;
        }
    }
}