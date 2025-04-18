using RMC.Mini;
using RMC.Mini.Service;
using UnityEngine.Events;
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
            weaponInfoModel.ProjectileCount.Value = _myWeapon.projectileCount;
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
                    weaponInfoModel.ProjectileCount.Value = weaponReload.Weapon.projectileCount;
                }
            }
        }
    }
}