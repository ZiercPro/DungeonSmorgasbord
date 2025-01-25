using RMC.Mini;
using RMC.Mini.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZiercCode.Test.MVC
{
    public class GameView : MonoBehaviour, IView
    {
        [field: SerializeField] public Slider HealthSlider { get; private set; }
        [field: SerializeField] public TextMeshProUGUI CurrentHealthText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TotalHealthText { get; private set; }
        [field: SerializeField] public Image CurrentWeapon { get; private set; }
        [field: SerializeField] public RectTransform ItemsContainer { get; private set; }
        [field: SerializeField] public RectTransform ItemTemplate { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ElementText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI CoinText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI CurrentLevelText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI CurrentPositionText { get; private set; }

        public void Initialize(IContext context)
        {
            if (_isInitialized) return;

            _context = context;

            _isInitialized = true;
        }

        public void RequireIsInitialized()
        {
            if (!_isInitialized) Debug.LogWarning("GameView 需要初始化");
        }

        public void Dispose()
        {
            //todo
        }

        private bool _isInitialized;
        private IContext _context;

        public bool IsInitialized => _isInitialized;
        public IContext Context => _context;
    }
}