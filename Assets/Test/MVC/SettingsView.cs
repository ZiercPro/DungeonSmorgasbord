using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using ZiercCode.DungeonSmorgasbord.Locale;
using ZiercCode.Test.ObserverValue;

namespace ZiercCode.Test.MVC
{
    public class SettingsView : MonoBehaviour, IView
    {
        public ObserverValue<bool> settingsChanged;
        public bool IsInitialized => _isInitialize;
        public IContext Context => _context;

        private bool _isInitialize;
        private IContext _context;

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

        private void OnEnable()
        {
            MasterVolume.onValueChanged.AddListener(OnAnyValueChange);
            MusicVolume.onValueChanged.AddListener(OnAnyValueChange);
            SfxVolume.onValueChanged.AddListener(OnAnyValueChange);
            EnvironmentVolume.onValueChanged.AddListener(OnAnyValueChange);
            Fps.onValueChanged.AddListener(OnAnyValueChange);
            LanguageDropdown.onValueChanged.AddListener(OnAnyValueChange);
        }

        private void OnDisable()
        {
            MasterVolume.onValueChanged.RemoveListener(OnAnyValueChange);
            MusicVolume.onValueChanged.RemoveListener(OnAnyValueChange);
            SfxVolume.onValueChanged.RemoveListener(OnAnyValueChange);
            EnvironmentVolume.onValueChanged.RemoveListener(OnAnyValueChange);
            Fps.onValueChanged.RemoveListener(OnAnyValueChange);
            LanguageDropdown.onValueChanged.RemoveListener(OnAnyValueChange);
        }


        public void Initialize(IContext context)
        {
            if (!_isInitialize)
            {
                _context = context;

                SettingsModel settingsModel = _context.ModelLocator.GetItem<SettingsModel>();

                settingsChanged = new ObserverValue<bool>(false);

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

        private void OnAnyValueChange<T>(T value)
        {
            settingsChanged.Value = true;
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