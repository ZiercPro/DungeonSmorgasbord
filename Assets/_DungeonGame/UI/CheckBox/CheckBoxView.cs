using RMC.Mini;
using RMC.Mini.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZiercCode._DungeonGame.UI.CheckBox
{
    public class CheckBoxView : MonoBehaviour, IView
    {
        [field: SerializeField] public CanvasGroupUser CanvasGroupUser { get; private set; }
        [field: SerializeField] public Button ConfirmButton { get; private set; }
        [field: SerializeField] public Button CancelButton { get; private set; }
        [field: SerializeField] public TextMeshProUGUI MessageText { get; private set; }

        public void Initialize(IContext context)
        {
            if (!_isInitialized)
            {
                _context = context;
                _isInitialized = true;
            }
        }

        public void RequireIsInitialized()
        {
        }

        public void Dispose()
        {
        }

        private bool _isInitialized;
        private IContext _context;

        public bool IsInitialized => _isInitialized;
        public IContext Context => _context;
    }
}