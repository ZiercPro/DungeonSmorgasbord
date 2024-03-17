using System.Collections.Generic;
using UnityEngine;

public class CanDropItems : MonoBehaviour
{
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