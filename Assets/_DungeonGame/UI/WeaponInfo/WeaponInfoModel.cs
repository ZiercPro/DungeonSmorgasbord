using RMC.Core.Observables;
using RMC.Mini.Model;

namespace ZiercCode._DungeonGame.UI.WeaponInfo
{
    public class WeaponInfoModel : BaseModel
    {
        public Observable<float> FireDamage;
        public Observable<float> IceDamage;
        public Observable<float> WoodDamage;
        public Observable<float> VoiceDamage;
        public Observable<float> PoisonDamage;
        public Observable<float> ElectricDamage;
        public Observable<float> WindDamage;
        public Observable<float> VoidDamage;
        public Observable<float> HitForce;
        public Observable<float> CriticalRate;
        public Observable<float> CriticalChance;
        public Observable<float> TriggerChance;
        public Observable<float> DamageReductionDistanceReduction;
        public Observable<float> ShootSpeed;
        public Observable<float> ShootDistance;
        public Observable<int> CurrentMagazineCount;
        public Observable<int> MagazineCapacity;
        public Observable<float> ReloadTime;
        public Observable<float> Accuracy;
        public Observable<float> ProjectileNumPerShoot;
        public Observable<float> ProjectileSpeed;
        public Observable<float> ProjectileSize;
        public Observable<string> WeaponName;

        public WeaponInfoModel()
        {
            CurrentMagazineCount = new Observable<int>();
            FireDamage = new Observable<float>();
            IceDamage = new Observable<float>();
            WoodDamage = new Observable<float>();
            VoiceDamage = new Observable<float>();
            PoisonDamage = new Observable<float>();
            ElectricDamage = new Observable<float>();
            WindDamage = new Observable<float>();
            VoidDamage = new Observable<float>();
            HitForce = new Observable<float>();
            CriticalRate = new Observable<float>();
            CriticalChance = new Observable<float>();
            TriggerChance = new Observable<float>();
            DamageReductionDistanceReduction = new Observable<float>();
            ShootSpeed = new Observable<float>();
            ShootDistance = new Observable<float>();
            MagazineCapacity = new Observable<int>();
            ReloadTime = new Observable<float>();
            Accuracy = new Observable<float>();
            ProjectileNumPerShoot = new Observable<float>();
            ProjectileSpeed = new Observable<float>();
            ProjectileSize = new Observable<float>();
            WeaponName = new Observable<string>();
        }
    }
}