using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZiercCode.Test.MVC
{
    public class MainMenuView : MonoBehaviour, IView
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;

        [SerializeField] private CanvasGroup canvasGroup;

        public UnityEvent onStartButtonPressed;
        public UnityEvent onSettingsButtonPressed;
        public UnityEvent onQuitButtonPressed;

        public CanvasGroup CanvasGroup => canvasGroup;

        public void RequireIsInitialized()
        {
            if (!_isInitialized)
            {
                Debug.LogWarning($"{name}需要初始化");
            }
        }

        public bool IsInitialized => _isInitialized;
        public IContext Context => _context;

        private bool _isInitialized;
        private IContext _context;

        public void Initialize(IContext context)
        {
            if (!_isInitialized)
            {
                _context = context;

                onStartButtonPressed = new UnityEvent();
                onSettingsButtonPressed = new UnityEvent();
                onQuitButtonPressed = new UnityEvent();

                startButton.onClick.AddListener(PressStartButton);
                settingsButton.onClick.AddListener(PressSettingsButton);
                quitButton.onClick.AddListener(PressQuitButton);

                _isInitialized = true;
            }
        }

        private void PressStartButton()
        {
            onStartButtonPressed?.Invoke();
        }

        private void PressSettingsButton()
        {
            onSettingsButtonPressed?.Invoke();
        }

        private void PressQuitButton()
        {
            onQuitButtonPressed?.Invoke();
        }
    }
}