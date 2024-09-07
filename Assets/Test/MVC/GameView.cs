using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using UnityEngine;

namespace ZiercCode.Test.MVC
{
    public class GameView : MonoBehaviour, IView
    {
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


        private bool _isInitialized;
        private IContext _context;

        public bool IsInitialized => _isInitialized;
        public IContext Context => _context;
    }
}