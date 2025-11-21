using DG.Tweening;
using UnityEngine;
using ZiercCode._DungeonGame._Scripts;
using ZiercCode._DungeonGame._Scripts.Text;
using ZiercCode._DungeonGame.Config;
using ZiercCode.Audio;
using ZiercCode.Locale;

namespace ZiercCode._DungeonGame.GameEntry
{
    public class GameEntry : MonoBehaviour
    {
        //临时逻辑
        //进入游戏第一个加载的场景名称
        [SerializeField]
        private string sceneName;

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
            // AudioPlayer.Instance.Init(); //音频组件
            ConfigComponent.Instance.Initialize(); //配置组件
            //GameState.Instance.Initialize(); // 游戏状态管理
            //SceneComponent.Instance.Initialize(); //场景组件

            TextPopup.Instance.Init(); //伤害显示

            //加载配置//
            ConfigComponent.Instance.LoadGameSettings();
            ConfigComponent.Instance.LoadGameSave();

            //适用配置//
            //设置语言
            LocalizationComponent.Instance.SetLanguage(ConfigComponent.Instance.GameSettings.Language);

            //设置音量
            AudioPlayer.Instance.SetMasterVolume(ConfigComponent.Instance.GameSettings.MasterVolume);
            AudioPlayer.Instance.SetEnvironmentVolume(ConfigComponent.Instance.GameSettings.EnvironmentVolume);
            AudioPlayer.Instance.SetMusicVolume(ConfigComponent.Instance.GameSettings.MusicVolume);
            AudioPlayer.Instance.SetSfxVolume(ConfigComponent.Instance.GameSettings.SfxVolume);

            //显示帧数
            FPSShower.Instance.SetFPS(ConfigComponent.Instance.GameSettings.FPSOn);
        }

        private void EnterGame()
        {
            SceneComponent.Instance.LoadScene(sceneName);
        }
    }
}