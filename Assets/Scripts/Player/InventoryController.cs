using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<Item> items;
    int maxCapacity;

    void Awake()
    {
        items = new List<Item>();
        maxCapacity = UserDataKeeper.userData.unlockedSlots;
    }

    public bool addItem(Item newItem)
    {
        foreach (Item item in items)
        {
            if (item.id == newItem.id)
            {
                item.itemAmount += newItem.itemAmount;
                return true;
            }
        }
        if (items.Count == maxCapacity)
            return false;
        
        items.Add(newItem);
        return true;
    }
}
