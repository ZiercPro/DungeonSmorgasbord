using NaughtyAttributes.Scripts.Core.DrawerAttributes_SpecialCase;
using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Old.DroppedItem;

namespace ZiercCode.DungeonSmorgasbord.Item
{
    public class CanDropItems : MonoBehaviour
    {
        [Button("Add")]
        private void AddDrop()
        {
            if (droppedItems == null) droppedItems = new List<DroppedItemConfig>();
            DroppedItemConfig newDrop = new DroppedItemConfig();
            droppedItems.Add(newDrop);
        }

        [Button("Delete")]
        private void DeleteDrop()
        {
            if (droppedItems != null && droppedItems.Count > 0)
            {
                int last = droppedItems.Count - 1;
                droppedItems.RemoveAt(last);
            }
        }

        [SerializeField] private float burstForce = 6f;
        [SerializeField] private List<DroppedItemConfig> droppedItems;

        /// <summary>
        /// 生成掉落物
        /// </summary>
        public void DropItems()
        {
            foreach (DroppedItemConfig item in droppedItems)
            {
                item.InstantiateItem(this.transform, burstForce);
            }
        }


        public float BurstForce => burstForce;
        public List<DroppedItemConfig> DroppedItems => droppedItems;
    }
}