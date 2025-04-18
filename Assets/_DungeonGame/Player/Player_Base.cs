using UnityEngine;
using ZiercCode.Management;

namespace ZiercCode._DungeonGame.Player
{
    public class Player_Base : PauseBehaviour
    {
        [SerializeField]
        private Transform weaponPoint; //武器装备点

        public void SetWeapon(Transform weaponTransform)
        {
            weaponTransform.SetParent(weaponPoint);
            weaponTransform.localPosition = Vector3.zero;
        }
    }
}