using RMC.Mini.Model;

namespace ZiercCode._DungeonGame.UI.PauseMenu
{
    public class PauseModel : BaseModel
    {
        public bool IsPause; //游戏是否暂停

        public PauseModel()
        {
            IsPause = false;
        }
    }
}