using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using Unity.VisualScripting;

namespace ZiercCode.Test.MVC
{
    public class GameController : BaseController<GameModel, GameView, Null>
    {
        private GameModel _model;
        private GameView _view;

        public GameController(GameModel model, GameView view) : base(model, view, null)
        {
            _model = model;
            _view = view;
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                
                
            }
        }
    }
}