using RMC.Mini;
using RMC.Mini.Controller;
using Unity.VisualScripting;

namespace ZiercCode.Test.MVC
{
    public class GameController : BaseController<GameModel, GameView, Null>
    {
        public GameController(GameModel model, GameView view) : base(model, view, null)
        {
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