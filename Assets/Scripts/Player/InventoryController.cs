using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<Item> items;

    void Awake()
    {
        items = new List<Item>();    
    }

    public void addItem(Item newItem)
    {
        foreach (Item item in items)
        {
            if (item.id == newItem.id)
            {
                item.itemAmount += newItem.itemAmount;
                return;
            }
        }
        items.Add(newItem);
    }
}
