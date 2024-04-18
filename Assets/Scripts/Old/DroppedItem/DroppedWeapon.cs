using UnityEngine;
using ZiercCode.Runtime.Component.Hero;

namespace ZiercCode.Runtime.DroppedItem
{

    public class DroppedWeapon : DroppedItem
    {
        [SerializeField] private GameObject droppedItemTemp;
        [SerializeField] private Transform animatorTransform;
        [SerializeField] private float getRadius;

        private HeroWeaponHandler _heroWeaponHandler;

        private GameObject itemInstance;

        private void Awake()
        {
            itemInstance = Instantiate(droppedItemTemp, animatorTransform);
        }

        public override void GetItem()
        {
            Collider2D[] c2ds = Physics2D.OverlapCircleAll(transform.position, getRadius);
            foreach (Collider2D hero in c2ds)
            {
                if (hero.TryGetComponent(out HeroWeaponHandler hw))
                {
                    GameObject weaponInstance = hw.EquipWeapon(itemInstance);
                    itemInstance = weaponInstance;
                    weaponInstance.transform.SetParent(animatorTransform);
                    weaponInstance.transform.localScale = Vector3.one;
                    weaponInstance.transform.localPosition = Vector3.zero;
                    weaponInstance.transform.localRotation = Quaternion.identity;
                }
            }
        }
    }
}