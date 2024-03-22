using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

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

    public void DropItems()
    {
        foreach (DroppedItemConfig item in droppedItems)
        {
            item.InstantiateItem(this.transform, burstForce);
            // float chance = item.DropChance / 100f;
            // MyMath.IsEffected(chance);
            // for (int i = 0; i < item.ItemNum; i++)
            // {
            //     GameObject newdrop = Instantiate(item.DroppedItemTemp, transform.position, Quaternion.identity);
            //     r2d = newdrop.GetComponent<Rigidbody2D>();
            //     r2d.AddForce(MyMath.GetRandomVector2() * burstForce, ForceMode2D.Impulse);
            // }
        }
    }


    public float BurstForce => burstForce;
    public List<DroppedItemConfig> DroppedItems => droppedItems;
}