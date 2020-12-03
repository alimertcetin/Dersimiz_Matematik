using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_instance
{
    //The Lists of item class
    List<Item_instance> itemList;
    public event EventHandler OnItemListChanged;

    public inventory_instance()
    {
        itemList = new List<Item_instance>();

        AddItem(new Item_instance { itemType = Item_instance.ItemType.Sayi_0, amount = 1 });
        AddItem(new Item_instance { itemType = Item_instance.ItemType.Sayi_1, amount = 1 });
        AddItem(new Item_instance { itemType = Item_instance.ItemType.Sayi_2, amount = 1 });
        AddItem(new Item_instance { itemType = Item_instance.ItemType.Sayi_7, amount = 1 });
        AddItem(new Item_instance { itemType = Item_instance.ItemType.Sayi_8, amount = 1 });
        AddItem(new Item_instance { itemType = Item_instance.ItemType.Sayi_9, amount = 1 });

    }

    public void AddItem(Item_instance item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item_instance> GetItemList()
    {
        return itemList;
    }
}
