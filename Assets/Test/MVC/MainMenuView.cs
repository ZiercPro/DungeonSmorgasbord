using RMC.Mini;
using RMC.Mini.View;
using UnityEngine;
using UnityEngine.UI;

namespace ZiercCode.Test.MVC
{
    public class MainMenuView : MonoBehaviour, IView
    {
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button SettingButton { get; private set; }
        [field: SerializeField] public Button QuitButton { get; private set; }
        [field: SerializeField] public CanvasGroup CanvasGroup { get; private set; }

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

                _isInitialized = true;
            }
        }

        public void Dispose()
        {
            //todo
        }
    }
}