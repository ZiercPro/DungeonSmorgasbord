using System.Collections.Generic;
using UnityEngine;

public class CanDropItems : MonoBehaviour
{
    [SerializeField] private float burstForce;
    [SerializeField] private List<DroppedItemConfig> droppedItems;

    public void DropItems()
    {
        Rigidbody2D r2d = null;
        foreach (DroppedItemConfig item in droppedItems)
        {
            float chance = item.DropChance / 100f;
            MyMath.IsEffected(chance);
            for (int i = 0; i < item.ItemNum; i++)
            {
                GameObject newdrop = Instantiate(item.DroppedItemTemp, transform.position, Quaternion.identity);
                r2d = newdrop.GetComponent<Rigidbody2D>();
                r2d.AddForce(MyMath.GetRandomVector2() * burstForce, ForceMode2D.Impulse);
            }
        }
    }

    public float BurstForce => burstForce;
    public List<DroppedItemConfig> DroppedItems => droppedItems;
}