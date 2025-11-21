using UnityEngine;
using ZiercCode._DungeonGame._Scripts.WeaponClass;
using ZiercCode.EventBusSystem;

namespace ZiercCode._DungeonGame._Scripts.WeaponComponent
{
    [RequireComponent(typeof(BaseWeapon))]
    public abstract class BaseWeaponComponent : MonoBehaviour
    {
        protected BaseWeapon MyWeapon;

        protected EventsGroup EventsGroup = new();

        protected virtual void Awake()
        {
            MyWeapon = GetComponent<BaseWeapon>();
        }
    }
}