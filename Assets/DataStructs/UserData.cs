using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public int level;
    public int totalXP;
    public int totalCoins;
    public int unlockedSlots;
    public List<Item> items;
    public int maxInventory;

    public UserData()
    {
        level = 1;
        totalXP = 0;
        totalCoins = 0;
        unlockedSlots = 1;
        items = new List<Item>();
        maxInventory = 10;
    }
}

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public int itemAmount;
}
