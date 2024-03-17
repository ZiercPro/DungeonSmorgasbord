using UnityEngine;

[System.Serializable]
public class DroppedItemConfig
{
    [SerializeField] [Range(0, 100)] private int dropChance;
    [SerializeField] private GameObject droppedItemTemp;
    [SerializeField] private bool haveItemNumRange;
    [SerializeField] private int itemNum;
    [SerializeField] private int minNum;
    [SerializeField] private int maxNum;


    public GameObject DroppedItemTemp => droppedItemTemp;
    public bool HaveItemNumRange => haveItemNumRange;
    public int DropChance => dropChance;
    public int ItemNum => itemNum;
    public int MaxNum => maxNum;
    public int MinNum => minNum;
}