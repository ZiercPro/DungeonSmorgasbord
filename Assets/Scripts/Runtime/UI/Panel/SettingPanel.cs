using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using ZiercCode.Runtime.Audio;
using ZiercCode.Runtime.Manager;
using ZiercCode.Runtime.UI.Framework;

namespace ZiercCode.Runtime.UI.Panel
{

    public class SettingPanel : BasePanel
    {
        private static readonly string path = "Prefabs/UI/Panel/SettingMenu";
        public SettingPanel() : base(new UIType(path)) { }

        public override void OnEnter()
        {
            //动画部分
            float start = 0f;
            CanvasGroup cGroup = UITool.GetOrAddComponent<CanvasGroup>();
            cGroup.interactable = true;
            cGroup.alpha = start;
            Tweener newTnr = cGroup.DOFade(1f, 0.5f).SetUpdate(true);
            //音量设置逻辑
            UITool.GetComponentInChildrenUI<Slider>("EnvironmentSlider").onValueChanged
                .AddListener(AudioPlayer.Instance.SetEnvironmentVolume);
            UITool.GetComponentInChildrenUI<Slider>("MasterSlider").onValueChanged
                .AddListener(AudioPlayer.Instance.SetMasterVolume);
            UITool.GetComponentInChildrenUI<Slider>("MusicSlider").onValueChanged
                .AddListener(AudioPlayer.Instance.SetMusicVolume);
            UITool.GetComponentInChildrenUI<Slider>("SFXSlider").onValueChanged
                .AddListener(AudioPlayer.Instance.SetSFXVolume);
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
                if (ison) GameRoot.Instance.LanguageManager.SetLanguage("Chinese");
            });
            UITool.GetComponentInChildrenUI<Toggle>("English").onValueChanged.AddListener(ison =>
            {
                if (ison) GameRoot.Instance.LanguageManager.SetLanguage("English");
            });
            //返回逻辑
            UITool.GetComponentInChildrenUI<Button>("BackButton").onClick.AddListener(() => { PanelManager.Pop(); });
            //读取默认值
            Toggle[] languageTgls = UITool.GetComponentInChildrenUI<Transform>("LanguageSettings")
                .GetComponentsInChildren<Toggle>();
            languageTgls[GameRoot.Instance.settingsData.Language].isOn = true;
            UITool.GetComponentInChildrenUI<RectTransform>("VolumeSettings").gameObject.SetActive(true);
            UITool.GetComponentInChildrenUI<RectTransform>("OtherSettings").gameObject.SetActive(false);
            UITool.GetComponentInChildrenUI<RectTransform>("LanguageSettings").gameObject.SetActive(false);
            UITool.GetComponentInChildrenUI<Slider>("MasterSlider").value = GameRoot.Instance.settingsData.MasterVolume;
            UITool.GetComponentInChildrenUI<Slider>("MusicSlider").value = GameRoot.Instance.settingsData.MusicVolume;
            UITool.GetComponentInChildrenUI<Slider>("SFXSlider").value = GameRoot.Instance.settingsData.SFXVolume;
            UITool.GetComponentInChildrenUI<Slider>("EnvironmentSlider").value =
                GameRoot.Instance.settingsData.EnvironmentVolume;
            UITool.GetComponentInChildrenUI<Toggle>("FPS").isOn = GameRoot.Instance.settingsData.FPSOn;
            UITool.GetComponentInChildrenUI<Toggle>("VolumeToggle").isOn = true;
        }

        public override void OnExit()
        {
            //移除事件监听
            UITool.GetComponentInChildrenUI<Button>("BackButton").onClick.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Slider>("MasterSlider").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Slider>("MusicSlider").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Slider>("SFXSlider").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Slider>("EnvironmentSlider").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Toggle>("VolumeToggle").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Toggle>("OtherToggle").onValueChanged.RemoveAllListeners();
            UITool.GetComponentInChildrenUI<Toggle>("LanguageToggle").onValueChanged.RemoveAllListeners();
            //同步设置值
            GameRoot.Instance.settingsData.EnvironmentVolume =
                UITool.GetComponentInChildrenUI<Slider>("EnvironmentSlider").value;
            GameRoot.Instance.settingsData.MasterVolume = UITool.GetComponentInChildrenUI<Slider>("MasterSlider").value;
            GameRoot.Instance.settingsData.MusicVolume = UITool.GetComponentInChildrenUI<Slider>("MusicSlider").value;
            GameRoot.Instance.settingsData.SFXVolume = UITool.GetComponentInChildrenUI<Slider>("SFXSlider").value;
            GameRoot.Instance.settingsData.FPSOn = UITool.GetComponentInChildrenUI<Toggle>("FPS").isOn;
            GameRoot.Instance.settingsData.Language = LanguageManager.GetSelectedLocalID();
            //动画部分
            CanvasGroup cGroup = UITool.GetOrAddComponent<CanvasGroup>();
            cGroup.interactable = false;
            Tweener newTnr = cGroup.DOFade(0f, 0.5f).SetUpdate(true).OnComplete(() =>
            {
                UIManager.DestroyUI(UIType);
            });
        }
    }
}