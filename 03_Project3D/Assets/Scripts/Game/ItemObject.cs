using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteraction
{
    [SerializeField] Item item;

    //public string ItemName => item.itemName;

    public string GetContext()
    {
        return item.itemName;
    }

    public void OnInteraction()
    {
        Inventory.Instance.AddItem(item);
        Destroy(gameObject);
    }

    public Item Pickup()
    {
        Destroy(gameObject);
        return item;
    }
}
