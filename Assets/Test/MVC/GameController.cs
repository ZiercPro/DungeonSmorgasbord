using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;

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