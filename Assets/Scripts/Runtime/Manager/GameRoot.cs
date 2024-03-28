using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Runtime.Manager
{
    using Data;
    using Basic;
    using Audio;
    using Scene;
    using Data.Base;
    using Scene.Base;

    /// <summary>
    /// 游戏程序全局管理器
    /// </summary>
    public class GameRoot : UnitySingleton<GameRoot>
    {
        public PlayerInputAction _playerInputAction { get; private set; }
        public LanguageManager LanguageManager { get; private set; }
        public AudioName AudioName { get; private set; }
        public UnityEvent OnEsc { get; private set; }
        public UnityEvent OnTab { get; private set; }
        public SceneSystem SceneSystem { get; private set; }

        public SettingsData settingsData;

        private IDataService jsonService;

        //添加static是为了在游戏结束时不去生成新的gameroot对象
        public static event Action OnGamePaues;
        public static event Action OnGameResume;

        private void OnEnable()
        {
            _playerInputAction.UI.Back.performed += OnEscPressed;
            _playerInputAction.PlayerInput.View.performed += OnTabPressed;
        }

        private void OnDisable()
        {
            //如果gameroot不是单例时，不会被分配 _playerinputaction，此时直接调用会报错
            if (_playerInputAction != null)
            {
                _playerInputAction.UI.Back.performed -= OnEscPressed;
                _playerInputAction.PlayerInput.View.performed -= OnTabPressed;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            OnEsc = new UnityEvent();
            OnTab = new UnityEvent();
            AudioName = new AudioName();
            SceneSystem = new SceneSystem();
            jsonService = new JsonDataService();
            _playerInputAction = new PlayerInputAction();
            LanguageManager = GetComponent<LanguageManager>();
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
                loadedData = jsonService.LoadData<SettingsData>("/settings.json", false);
                Debug.Log("Settings loading complete!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Can not load settings data due to:{e.Message};{e.StackTrace}");
                loadedData = null;
                //提示玩家 加载失败
            }

            if (loadedData == null) loadedData = new SettingsData(0f, 0f, 0f, 0f, false, 0);
            settingsData = loadedData;
        }

        private void SaveSettings()
        {
            if (jsonService.SaveData("/settings.json", settingsData, false))
                Debug.Log("Settings saved!");
            else
                Debug.LogError("Settings can not save!");
        }

        /// <summary>
        /// 游戏初始化
        /// </summary>
        private void GameInit()
        {
            AudioPlayerManager.Instance.SetEnvironmentVolume(settingsData.EnvironmentVolume);
            AudioPlayerManager.Instance.SetMasterVolume(settingsData.MasterVolume);
            AudioPlayerManager.Instance.SetMusicVolume(settingsData.MusicVolume);
            AudioPlayerManager.Instance.SetSFXVolume(settingsData.SFXVolume);

            _playerInputAction.UI.Enable();

            LanguageManager.SetLanguage(settingsData.Language);
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