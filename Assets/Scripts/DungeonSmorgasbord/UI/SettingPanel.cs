using UnityEngine;
using UnityEngine.UI;
using ZiercCode.Core.UI;
using ZiercCode.DungeonSmorgasbord.Locale;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Helper;
using ZiercCode.Old.Manager;

namespace ZiercCode.DungeonSmorgasbord.UI
{
    public class SettingPanel : BaseUIAnimationPanel
    {
        private static readonly string path = "Prefabs/UI/Panel/SettingMenu";
        public SettingPanel() : base(new UIType(path)) { }

        public override void OnEnter()
        {
            SetAction(GetBackInputAction(), context => PanelManager.Pop());
            //动画部分
            base.OnEnter();
            //读取默认值
            Toggle[] languageToggelGroup = UITool.GetComponentInChildrenUI<Transform>("LanguageSettings")
                .GetComponentsInChildren<Toggle>();
            languageToggelGroup[LocaleManager.Instance.GetSelectedLanguageID()].isOn = true;
            UITool.GetComponentInChildrenUI<RectTransform>("VolumeSettings").gameObject.SetActive(true);
            UITool.GetComponentInChildrenUI<RectTransform>("OtherSettings").gameObject.SetActive(false);
            UITool.GetComponentInChildrenUI<RectTransform>("LanguageSettings").gameObject.SetActive(false);
            UITool.GetComponentInChildrenUI<Toggle>("VolumeToggle").isOn = true;
            UITool.GetComponentInChildrenUI<Slider>("MasterSlider").value =
                GameEntry.Instance.ConfigManager.SettingsData.MasterVolume;
            UITool.GetComponentInChildrenUI<Slider>("MusicSlider").value =
                GameEntry.Instance.ConfigManager.SettingsData.MusicVolume;
            UITool.GetComponentInChildrenUI<Slider>("SFXSlider").value =
                GameEntry.Instance.ConfigManager.SettingsData.SfxVolume;
            UITool.GetComponentInChildrenUI<Slider>("EnvironmentSlider").value =
                GameEntry.Instance.ConfigManager.SettingsData.EnvironmentVolume;
            UITool.GetComponentInChildrenUI<Toggle>("FPS").isOn = GameEntry.Instance.ConfigManager.SettingsData.FPSOn;
            //音量设置逻辑
            UITool.GetComponentInChildrenUI<Slider>("EnvironmentSlider").onValueChanged
                .AddListener(SetEnvironmentVolume);
            UITool.GetComponentInChildrenUI<Slider>("MasterSlider").onValueChanged
                .AddListener(SetMasterVolume);
            UITool.GetComponentInChildrenUI<Slider>("MusicSlider").onValueChanged
                .AddListener(SetMusicVolume);
            UITool.GetComponentInChildrenUI<Slider>("SFXSlider").onValueChanged
                .AddListener(SetSfxVolume);
            //fps设置
            UITool.GetComponentInChildrenUI<Toggle>("FPS").onValueChanged.AddListener(SetFps);
            //页面切换逻辑
            UITool.GetComponentInChildrenUI<Toggle>("VolumeToggle").onValueChanged.AddListener(ison =>
            {
                UITool.GetComponentInChildrenUI<RectTransform>("VolumeSettings").gameObject.SetActive(ison);
            });
            UITool.GetComponentInChildrenUI<Toggle>("OtherToggle").onValueChanged.AddListener(ison =>
            {
                UITool.GetComponentInChildrenUI<RectTransform>("OtherSettings").gameObject.SetActive(ison);
            });
            UITool.GetComponentInChildrenUI<Toggle>("LanguageToggle").onValueChanged.AddListener(ison =>
            {
                UITool.GetComponentInChildrenUI<RectTransform>("LanguageSettings").gameObject.SetActive(ison);
            });
            //语言设置逻辑
            UITool.GetComponentInChildrenUI<Toggle>("Chinese").onValueChanged.AddListener(ison =>
            {
                if (ison) SetLanguage(LanguageEnum.Chinese);
            });
            UITool.GetComponentInChildrenUI<Toggle>("English").onValueChanged.AddListener(ison =>
            {
                if (ison) SetLanguage(LanguageEnum.English);
            });
            //返回逻辑
            UITool.GetComponentInChildrenUI<Button>("BackButton").onClick.AddListener(() => { PanelManager.Pop(); });
        }

        public override void OnExit()
        {
            base.OnExit();
            //移除事件监听
            UITool.GetComponentInChildrenUI<Button>("BackButton").onClick.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Slider>("MasterSlider").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Slider>("MusicSlider").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Slider>("SFXSlider").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Slider>("EnvironmentSlider").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Toggle>("VolumeToggle").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Toggle>("OtherToggle").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Toggle>("LanguageToggle").onValueChanged.RemoveAllListeners();
            UIManager.DestroyUI(UIType);
        }

        private void SetMusicVolume(float amount)
        {
            AudioPlayer.Instance.SetMusicVolume(amount);
            GameEntry.Instance.ConfigManager.SettingsData.MusicVolume = amount;
        }

        private void SetSfxVolume(float amount)
        {
            AudioPlayer.Instance.SetSfxVolume(amount);
            GameEntry.Instance.ConfigManager.SettingsData.SfxVolume = amount;
        }

        private void SetMasterVolume(float amount)
        {
            AudioPlayer.Instance.SetMasterVolume(amount);
            GameEntry.Instance.ConfigManager.SettingsData.MasterVolume = amount;
        }

        private void SetEnvironmentVolume(float amount)
        {
            AudioPlayer.Instance.SetEnvironmentVolume(amount);
            GameEntry.Instance.ConfigManager.SettingsData.EnvironmentVolume = amount;
        }

        private void SetLanguage(LanguageEnum language)
        {
            LocaleManager.Instance.SetLanguage(language);
            GameEntry.Instance.ConfigManager.SettingsData.Language = LocaleManager.Instance.GetSelectedLanguage();
        }

        private void SetFps(bool enable)
        {
            UITool.GetComponentInChildrenUI<Toggle>("FPS").isOn = enable;
            GameEntry.Instance.ConfigManager.SettingsData.FPSOn = enable;
        }
    }
}