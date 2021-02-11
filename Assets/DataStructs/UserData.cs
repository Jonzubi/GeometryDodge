﻿using System.Collections;
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
            foreach (Item item in items)
            {
                if (collectedItem.id == item.id)
                {
                    item.itemAmount += collectedItem.itemAmount;
                }
            }

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
    public int id;
    public int itemAmount;

    public Item(int id, int itemAmount)
    {
        this.id = id;
        this.itemAmount = itemAmount;
    }
}

public enum ItemName
{
    BULLET
}
