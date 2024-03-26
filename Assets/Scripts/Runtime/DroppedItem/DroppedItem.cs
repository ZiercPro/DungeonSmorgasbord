using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public int num;
    private static List<DroppedItem> _items;

    protected static bool _isClearing;

    protected void OnEnable()
    {
        if (_items == null) _items = new List<DroppedItem>();
        _items.Add(this);
    }

    public virtual void GetItem()
    {
        if (_isClearing) return;
        _items.Remove(this);
    }

    public static void ClearAllItem()
    {
        if (_items == null || _items.Count == 0) return;
        _isClearing = true;
        MyMath.ForeachChangeListAvailable(_items, item =>
        {
            if (item != null)
            {
                //Debug.Log(item.name);
                _items.Remove(item);
                Destroy(item.gameObject);
            }
        });
    }
}