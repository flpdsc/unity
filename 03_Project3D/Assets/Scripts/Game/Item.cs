using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public ItemData data;
    public int count;

    public string itemName => data.itemName;
    public string description => data.itemDescription;
    public Sprite itemSprite => data.itemSprite;

    public Item(ItemData data, int count)
    {
        this.data = data;
        this.count = count;

    }
}
