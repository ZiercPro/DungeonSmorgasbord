using RMC.Mini;
using RMC.Mini.Service;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using ZiercCode._DungeonGame.Config;
using ZiercCode.Audio;
using ZiercCode.Locale;

namespace ZiercCode._DungeonGame.UI.Settings
{
    public class SettingsService : BaseService
    {
        private PlayerInputAction _playerInputAction;

        public UnityEvent OnBackInput;

        public SettingsService()
        {
            _playerInputAction = new PlayerInputAction();
            OnBackInput = new UnityEvent();
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                _playerInputAction.UI.Enable();
                _playerInputAction.UI.Back.started += BackInput;
            }
        }

        public override void Dispose()
        {
            _playerInputAction.UI.Back.started -= BackInput;
            _playerInputAction.UI.Disable();
        }

        private void BackInput(InputAction.CallbackContext context)
        {
            OnBackInput?.Invoke();
        }


        //从文件中加载设置数据 同步到视图？ 应该是同步到model
        public void LoadGameSettings()
        {
            ConfigComponent.Instance.LoadGameSettings(); //从文件中加载游戏设置
            GameSettings settings = ConfigComponent.Instance.GameSettings;

            //修改模型数据
            SettingsModel model = Context.ModelLocator.GetItem<SettingsModel>();

            model.MasterVolume.Value = settings.MasterVolume;
            model.MusicVolume.Value = settings.MusicVolume;
            model.EnvironmentVolume.Value = settings.EnvironmentVolume;
            model.SfxVolume.Value = settings.SfxVolume;
            model.FpsOn.Value = settings.FPSOn;
            model.Language.Value = settings.Language;
        }

        //将model数据写入文件
        public void SaveGameSettings()
        {
            SettingsModel model = Context.ModelLocator.GetItem<SettingsModel>();

            GameSettings settings = ConfigComponent.Instance.GameSettings;

            settings.FPSOn = model.FpsOn.Value;
            settings.Language = model.Language.Value;
            settings.MasterVolume = model.MasterVolume.Value;
            settings.MusicVolume = model.MusicVolume.Value;
            settings.SfxVolume = model.SfxVolume.Value;
            settings.EnvironmentVolume = model.EnvironmentVolume.Value;

            ConfigComponent.Instance.SaveGameSettings(settings);
        }

        public void UpdateAudioVolume(SettingsModel model)
        {
            GameSettings settings = ConfigComponent.Instance.GameSettings;

            AudioPlayer.Instance.SetMasterVolume(model.MasterVolume.Value);
            AudioPlayer.Instance.SetEnvironmentVolume(model.EnvironmentVolume.Value);
            AudioPlayer.Instance.SetMusicVolume(model.MusicVolume.Value);
            AudioPlayer.Instance.SetSfxVolume(model.SfxVolume.Value);
        }

        public void UpdateLanguage(int language)
        {
            LocalizationComponent.Instance.SetLanguage(language);
        }

        public string GetLocaleString(string key)
        {
            return LocalizationComponent.Instance.GetText(key);
        }
        
        public void SetUIInput(bool value)
        {
            if (value)
                _playerInputAction.UI.Enable();
            else _playerInputAction.UI.Disable();
        }
    }
}