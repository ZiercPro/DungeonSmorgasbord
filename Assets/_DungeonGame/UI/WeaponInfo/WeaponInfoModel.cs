using RMC.Core.Observables;
using RMC.Mini.Model;

namespace ZiercCode._DungeonGame.UI.WeaponInfo
{
    public class WeaponInfoModel : BaseModel
    {
        public Observable<int> ProjectileCount;

        public WeaponInfoModel()
        {
            ProjectileCount = new Observable<int>();
        }
    }
}