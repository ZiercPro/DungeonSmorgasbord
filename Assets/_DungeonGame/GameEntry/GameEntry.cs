using DG.Tweening;
using UnityEngine;
using ZiercCode._DungeonGame.Config;
using ZiercCode._DungeonGame.Juice;
using ZiercCode.Audio;
using ZiercCode.DungeonSmorgasbord.Locale;
using ZiercCode.Locale;

namespace ZiercCode._DungeonGame.GameEntry
{
    public class GameEntry : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        private void Start()
        {
            StartGame();
            EnterGame();
        }

        //游戏启动时
        private void StartGame()
        {
            //初始化//
            DOTween.Init(); //DOTween
            LocalizationComponent.Instance.InitializeCustomText(); //本地化组件
            AudioPlayer.Instance.Init(); //音频组件
            ConfigComponent.Instance.Initialize(); //配置组件

            TextPopup.Instance.Init(); //伤害显示


            //加载配置//
            ConfigComponent.Instance.LoadGameSettings();

            //适用配置//
            //设置语言
            LocalizationComponent.Instance.SetLanguage(ConfigComponent.Instance.GameSettings.Language);

            //设置音量
            AudioPlayer.Instance.SetMasterVolume(ConfigComponent.Instance.GameSettings.MasterVolume);
            AudioPlayer.Instance.SetEnvironmentVolume(ConfigComponent.Instance.GameSettings.EnvironmentVolume);
            AudioPlayer.Instance.SetMusicVolume(ConfigComponent.Instance.GameSettings.MusicVolume);
            AudioPlayer.Instance.SetSfxVolume(ConfigComponent.Instance.GameSettings.SfxVolume);
        }

        private void EnterGame()
        {
            SceneComponent.Instance.LoadScene(sceneName);
        }
    }
}