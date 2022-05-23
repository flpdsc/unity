using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    [SerializeField] ItemData[] itemData;

    public Item GetItem(string itemName, int count)
    {
        //아이템 데이터 검색 
        ItemData data = GetItemData(itemName);
        if(data == null)
        {
            Debug.Log(itemName + "은 없습니다.");
            return null;
        }

        //해당 데이터를 Item 객체로 생성 후 반환 
        return new Item(data, count);
    }

    public ItemData GetItemData(string name)
    {
        foreach(ItemData data in itemData)
        {
            if(data.name == name)
                return data;
        }

        return null;
    }

}
