using RMC.Mini;
using RMC.Mini.View;
using UnityEngine;
using UnityEngine.UI;

namespace ZiercCode._DungeonGame.UI.MainMenu
{
    public class MainMenuView : MonoBehaviour, IView
    {
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button SettingButton { get; private set; }
        [field: SerializeField] public Button QuitButton { get; private set; }
        [field: SerializeField] public CanvasGroupUser CanvasGroupUser { get; private set; }

        public bool IsInitialized => _isInitialized;
        public IContext Context => _context;

        private bool _isInitialized;
        private IContext _context;

        public void RequireIsInitialized()
        {
        }
        
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
        }
    }
}