using RMC.Mini;
using RMC.Mini.Service;
using UnityEngine.Events;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode._DungeonGame._Scripts.WeaponClass;
using ZiercCode.EventBusSystem;

namespace ZiercCode._DungeonGame.UI.WeaponInfo
{
    public class WeaponInfoService : BaseService
    {
        private EventsGroup _eventsGroup;

        private BaseWeapon _myWeapon;

        public UnityEvent OnReloaded;
        public UnityEvent<float> OnStartReload;

        public WeaponInfoService(BaseWeapon myWeapon)
        {
            _myWeapon = myWeapon;
            _eventsGroup = new EventsGroup();
            OnReloaded = new UnityEvent();
            OnStartReload = new UnityEvent<float>();
        }

        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                _eventsGroup.AddListener<WeaponEvent.WeaponFired>(OnWeaponFire);
                _eventsGroup.AddListener<WeaponEvent.WeaponReloaded>(OnWeaponReload);
                _eventsGroup.AddListener<WeaponEvent.WeaponStartReload>(OnWeaponStartReload);
                _eventsGroup.AddListener<WeaponEvent.WeaponDataChanged>(OnWeaponDataChanged);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            _eventsGroup.RemoveAllListener();
        }

        /// <summary>
        /// 同步数据到model
        /// </summary>
        public void SyncModel()
        {
            WeaponInfoModel weaponInfoModel = Context.ModelLocator.GetItem<WeaponInfoModel>();

            if (_myWeapon.damage.ToDictionary.TryGetValue(DamageType.Fire, out float fire))
            {
                weaponInfoModel.FireDamage.Value = fire;
            }

            if (_myWeapon.damage.ToDictionary.TryGetValue(DamageType.Wood, out float wood))
            {
                weaponInfoModel.WoodDamage.Value = wood;
            }

            if (_myWeapon.damage.ToDictionary.TryGetValue(DamageType.Wind, out float wind))
            {
                weaponInfoModel.WindDamage.Value = wind;
            }

            if (_myWeapon.damage.ToDictionary.TryGetValue(DamageType.Electric, out float electric))
            {
                weaponInfoModel.ElectricDamage.Value = electric;
            }

            if (_myWeapon.damage.ToDictionary.TryGetValue(DamageType.Poison, out float poison))
            {
                weaponInfoModel.PoisonDamage.Value = poison;
            }

            if (_myWeapon.damage.ToDictionary.TryGetValue(DamageType.Voice, out float voice))
            {
                weaponInfoModel.VoiceDamage.Value = voice;
            }

            if (_myWeapon.damage.ToDictionary.TryGetValue(DamageType.Ice, out float ice))
            {
                weaponInfoModel.IceDamage.Value = ice;
            }

            if (_myWeapon.damage.ToDictionary.TryGetValue(DamageType.Void, out float vod))
            {
                weaponInfoModel.VoidDamage.Value = vod;
            }

            weaponInfoModel.CurrentMagazineCount.Value = _myWeapon.currentMagazineCount;
            weaponInfoModel.HitForce.Value = _myWeapon.hitForce;
            weaponInfoModel.CriticalChance.Value = _myWeapon.criticalChance;
            weaponInfoModel.CriticalRate.Value = _myWeapon.criticalDamageRate;
            weaponInfoModel.TriggerChance.Value = _myWeapon.triggerChance;
            weaponInfoModel.DamageReductionDistanceReduction.Value = _myWeapon.damageReductionByDistance;
            weaponInfoModel.ShootSpeed.Value = _myWeapon.shootSpeed;
            weaponInfoModel.ShootDistance.Value = _myWeapon.shootDistance;
            weaponInfoModel.MagazineCapacity.Value = _myWeapon.magazineCapacity;
            weaponInfoModel.ReloadTime.Value = _myWeapon.reloadTime;
            weaponInfoModel.Accuracy.Value = _myWeapon.accuracy;
            weaponInfoModel.ProjectileNumPerShoot.Value = _myWeapon.projectileNumPerShoot;
            weaponInfoModel.ProjectileSpeed.Value = _myWeapon.projectileSpeed;
            weaponInfoModel.ProjectileSize.Value = _myWeapon.projectileSize;
            weaponInfoModel.WeaponName.Value = _myWeapon.weaponName;
        }

        private void OnWeaponStartReload(IEventArgs args)
        {
            if (args is WeaponEvent.WeaponStartReload startReload)
            {
                if (startReload.Weapon == _myWeapon)
                {
                    OnStartReload.Invoke(_myWeapon.reloadTime);
                }
            }
        }

        private void OnWeaponReload(IEventArgs args)
        {
            if (args is WeaponEvent.WeaponReloaded weaponReload)
            {
                if (weaponReload.Weapon == _myWeapon)
                {
                    OnReloaded?.Invoke();
                }
            }
        }

        private void OnWeaponFire(IEventArgs args)
        {
            if (args is WeaponEvent.WeaponFired weaponReload)
            {
                WeaponInfoModel weaponInfoModel = Context.ModelLocator.GetItem<WeaponInfoModel>();
                if (weaponReload.Weapon == _myWeapon)
                {
                    weaponInfoModel.CurrentMagazineCount.Value = weaponReload.Weapon.currentMagazineCount;
                }
            }
        }

        private void OnWeaponDataChanged(IEventArgs args)
        {
            if (args is WeaponEvent.WeaponDataChanged weaponDataChanged)
            {
                if (weaponDataChanged.Weapon == _myWeapon)
                {
                    SyncModel();
                }
            }
        }
    }
}