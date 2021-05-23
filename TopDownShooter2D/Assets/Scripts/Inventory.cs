using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { item_Type = Item.ItemType.Pistol, amount = 1 });
        //AddItem(new Item { item_Type = Item.ItemType.Shotgun, amount = 1 });
        AddItem(new Item { item_Type = Item.ItemType.Medkit, amount = 1 });
        AddItem(new Item { item_Type = Item.ItemType.HealthPotion, amount = 1 });
        AddItem(new Item { item_Type = Item.ItemType.Coin, amount = 1 });
        //AddItem(new Item { item_Type = Items.ItemType.Bullet, amount = 1 });
    }
    public bool hasItem(Item.ItemType type)
    {
        foreach(Item item in itemList)
        {
            if (item.getType() == type) return true;
        }
        return false;
    }
    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        if(item.getType() == Item.ItemType.Shotgun) weaponsManager.setWeaponAvailability(1, true);
        if(item.getType() == Item.ItemType.Rifle) weaponsManager.setWeaponAvailability(2, true);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}



