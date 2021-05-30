using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public int level; // El nivel del jugador
    public int totalXP; // Total de XP
    public int totalCoins; // Total de monedas
    public int unlockedSlots; // La cantidad de slots desbloqueada
    public List<Item> items; // Los items que tiene en su inventario, fuera de juego
    public int maxInventory; // Su cantidad maxima de cosas que puede guardar, fuera de juego

    public UserData()
    {
        level = 1;
        totalXP = 0;
        totalCoins = 0;
        unlockedSlots = 1;
        items = new List<Item>();
        maxInventory = 10;
    }

    public void CollectItems(List<Item> collectedItems)
    {
        foreach (Item collectedItem in collectedItems)
        {
            bool added = false;
            foreach (Item item in items)
            {
                if (collectedItem.id == item.id)
                {
                    item.itemAmount += collectedItem.itemAmount;
                    added = true;
                }
            }

            if (added)
                continue;

            if (items.Count < maxInventory)
            {
                items.Add(collectedItem);
            }
        }
    }
}

[System.Serializable]
public class Item
{
    public ItemName id;
    public int itemAmount;

    public Item(ItemName id, int itemAmount)
    {
        this.id = id;
        this.itemAmount = itemAmount;
    }
}
[System.Serializable]
public class ItemDescriptions
{
    public ItemDescription[] items;
}
[System.Serializable]
public class ItemDescription
{
    public string id;
    public string description;
    public int price;
    public int unlockOnLevel;
}

public enum ItemName
{
    BULLET,
    SHIELD
}
