using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    Item[] items;

    const int MAX_INVENTORY = 20;

    private new void Awake()
    {
        base.Awake();
        items = new Item[MAX_INVENTORY];
    }

    public void AddItem(Item item)
    {
        for(int i=0; i<items.Length; ++i)
        {
            if(items[i] == null)
            {
                items[i] = item;
                break;
            }
        }
        PickupUI.Instance.PickupItem(item);
        UpdateUI();
    }

    public void MoveItem(int start, int end)
    {
        if (start == end)
            return;

        Item startItem = items[start];
        Item endItem = items[end];

        items[start] = endItem;
        items[end] = startItem;

        UpdateUI();
    }

    public void UpdateUI()
    {
        InventoryUI.Instance.UpdateItems(items);
    }
}
