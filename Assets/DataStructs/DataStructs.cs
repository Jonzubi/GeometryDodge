using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
public class Item : ICloneable
{
    public ItemName id;
    public int itemAmount;

    public Item(ItemName id, int itemAmount)
    {
        this.id = id;
        this.itemAmount = itemAmount;
    }

    public object Clone()
    {
        return this.MemberwiseClone();
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
    public string id; // ItemName, lo pongo en String ya que si no no se puede leer el JSON
    public string description; // Descripcion del Item, aparecera al seleccionar en el inventario
    public int price; // Precio del Item en el Shop
    public int unlockOnLevel; // Nivel en el que se desbloquea el Item para comprarlo en el Shop
    public short spawnProbability; // Probabilidad a la que se Spawnea en el juego
    public short spawnAfterRound; // Indica a partir de que ronda se debe considerar spawnear el Item (0 si es desde el inicio)
}

public enum ItemName
{
    BULLET,
    SHIELD
}
