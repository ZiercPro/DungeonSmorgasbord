using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using ZiercCode.Runtime.Audio;
using ZiercCode.Runtime.Basic;
using ZiercCode.Runtime.Data;
using ZiercCode.Runtime.Scene;

namespace ZiercCode.Runtime.Manager
{
    /// <summary>
    /// 游戏程序全局管理器
    /// </summary>
    public class GameRoot : UnitySingleton<GameRoot>
    {
        public PlayerInputAction PlayerInputAction { get; private set; }
        public LanguageManager LanguageManager { get; private set; }
        public UnityEvent OnEsc { get; private set; }
        public UnityEvent OnTab { get; private set; }
        public SceneSystem SceneSystem { get; private set; }

        public SettingsData SettingsData;

        private IDataService _jsonService;

        //添加static是为了在游戏结束时不去生成新的gameroot对象
        public static event Action OnGamePaues;
        public static event Action OnGameResume;

        private void OnEnable()
        {
            PlayerInputAction.UI.Back.performed += OnEscPressed;
            PlayerInputAction.PlayerInput.View.performed += OnTabPressed;
        }

        private void OnDisable()
        {
            //如果gameroot不是单例时，不会被分配 _playerinputaction，此时直接调用会报错
            if (PlayerInputAction != null)
            {
                PlayerInputAction.UI.Back.performed -= OnEscPressed;
                PlayerInputAction.PlayerInput.View.performed -= OnTabPressed;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            OnEsc = new UnityEvent();
            OnTab = new UnityEvent();
            SceneSystem = new SceneSystem();
            _jsonService = new JsonDataService();
            PlayerInputAction = new PlayerInputAction();
            LanguageManager = GetComponentInChildren<LanguageManager>();
        }
        
        private void Start()
        {
            LoadSettings();
            GameInit();
            SceneSystem.SetScene(new StartScene());
        }

        private void OnDestroy()
        {
            SaveSettings();
        }

        /// <summary>
        /// 加载设置信息
        /// </summary>
        private void LoadSettings()
        {
            //默认值
            SettingsData loadedData = null;
            try
            {
                loadedData = _jsonService.LoadData<SettingsData>("/settings.json", false);
                Debug.Log("Settings loading complete!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Can not load settings data due to:{e.Message};{e.StackTrace}");
                loadedData = null;
                //提示玩家 加载失败
            }

            if (loadedData == null) loadedData = new SettingsData(0f, 0f, 0f, 0f, false, 0);
            SettingsData = loadedData;
        }

        private void SaveSettings()
        {
            if (_jsonService.SaveData("/settings.json", SettingsData, false))
                Debug.Log("Settings saved!");
            else
                Debug.LogError("Settings can not save!");
        }

        /// <summary>
        /// 游戏初始化
        /// </summary>
        private void GameInit()
        {
            AudioPlayer.Instance.SetEnvironmentVolume(SettingsData.EnvironmentVolume);
            AudioPlayer.Instance.SetMasterVolume(SettingsData.MasterVolume);
            AudioPlayer.Instance.SetMusicVolume(SettingsData.MusicVolume);
            AudioPlayer.Instance.SetSFXVolume(SettingsData.SFXVolume);

            PlayerInputAction.UI.Enable();

            LanguageManager.SetLanguage(SettingsData.Language);
            AudioPlayer.Instance.PlayAudioAsync(AudioName.MenuBgm);
        }

        private void OnEscPressed(InputAction.CallbackContext context)
        {
            OnEsc?.Invoke();
        }

        private void OnTabPressed(InputAction.CallbackContext context)
        {
            OnTab?.Invoke();
        }

        /// <summary>
        /// 时间流速 0
        /// </summary>
        public void Pause()
        {
            Time.timeScale = 0f;
            OnGamePaues?.Invoke();
        }

        /// <summary>
        /// 时间流速 1
        /// </summary>
        public void Play()
        {
            Time.timeScale = 1f;
            OnGameResume?.Invoke();
        }
    }
}