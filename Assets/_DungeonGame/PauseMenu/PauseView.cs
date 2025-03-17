using RMC.Mini;
using RMC.Mini.View;
using UnityEngine;
using UnityEngine.UI;

namespace ZiercCode
{
    public class PauseView : MonoBehaviour, IView //暂停界面
    {
        [field: SerializeField] public Button ContinueButton { get; private set; }
        [field: SerializeField] public Button SettingsButton { get; private set; }
        [field: SerializeField] public Button MainMenuButton { get; private set; }
        [field: SerializeField] public CanvasGroupUser CanvasGroupUser { get; private set; }

        private bool _isInitialized;
        private IContext _context;

        public void Dispose()
        {
        }

        public void Initialize(IContext context)
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
            }
        }

        public void RequireIsInitialized()
        {
        }

        public bool IsInitialized => _isInitialized;
        public IContext Context => _context;
    }
}