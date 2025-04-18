using UnityEngine;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.Audio;
using ZiercCode.EventBusSystem;

namespace ZiercCode._DungeonGame._Scripts.WeaponComponent
{
    [RequireComponent(typeof(SfxRandomPlayer))]
    public class GunPlayRandomSfxComponent : BaseWeaponComponent
    {
        private SfxRandomPlayer _sfxRandomPlayer;

        protected override void Awake()
        {
            base.Awake();
            _sfxRandomPlayer = GetComponent<SfxRandomPlayer>();
        }

        protected void OnEnable()
        {
            EventsGroup.AddListener<WeaponEvent.WeaponFired>(OnFire);
        }

        protected void OnDisable()
        {
            EventsGroup.RemoveAllListener();
        }

        private void OnFire(IEventArgs args)
        {
            if (args is WeaponEvent.WeaponFired weaponFire)
            {
                if (weaponFire.Weapon == MyWeapon)
                {
                    _sfxRandomPlayer.PlayRandomSfx();
                }
            }
        }
    }
}