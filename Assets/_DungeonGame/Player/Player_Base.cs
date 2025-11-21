using UnityEngine;
using ZiercCode._DungeonGame._Scripts.EntityClasses;
using ZiercCode._DungeonGame._Scripts.WeaponClass;

namespace ZiercCode._DungeonGame.Player
{
    public abstract class Player_Base : Entity
    {
        [SerializeField]
        private Transform weaponPoint; //武器装备点

        public void SetWeapon(Transform weaponTransform)
        {
            weaponTransform.SetParent(weaponPoint);
            weaponTransform.localPosition = Vector3.zero;
            weaponTransform.GetComponent<BaseWeapon>().Init(this);
        }
    }
}