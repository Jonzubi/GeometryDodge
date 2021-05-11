using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<Item> items;
    SpawnManager m_spawnManager;
    int maxCapacity;

    void Awake()
    {
        items = UserDataKeeper.gameInventory;
        m_spawnManager = FindObjectOfType<SpawnManager>();
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

    public void OnItemUsed(int slotId, Vector3 position, Quaternion rotation)
    {
        // Significa que en el inventario hay menos items disponibles que el slot pulsado.
        // Ejemplo: Se ha pulsado el slot 9 cuando solo tenemos 1 item  en el inventario.
        if (slotId > items.Count - 1)
            return;
        m_spawnManager.OnItemUsed(items[slotId].id, position, rotation);
        if (items[slotId].itemAmount == 1)
        {
            items.RemoveAt(slotId);
        }
        else
        {
            items[slotId].itemAmount--;    
        }
            
    }
}
