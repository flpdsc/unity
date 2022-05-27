using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTable : MonoBehaviour
{
    [System.Serializable]
    public struct DropTable
    {
        public ItemData.ITEM_TYPE type;
        public int itemCount;
        public float percent;

        public Item GetItem()
        {
            return ItemManager.Instance.GetItem(type, itemCount);
        }

    };

    [SerializeField] DropTable[] dropTables;

    float totalPercent = 0f;

    private void Start()
    {
        for(int i=0; i<dropTables.Length; ++i)
        {
            totalPercent += dropTables[i].percent;
        }
        
    }

    public void DropRandomItem()
    {
        //총 확률에 비율을 곱해 원하는 위치 지정 
        float pick = totalPercent * Random.value;
        float category = 0;
        Item dropItem = null;

        //모든 테이블 돌면서 위치에 해당하는 아이템 추출 
        for(int i=0; i<dropTables.Length; ++i)
        {
            category += dropTables[i].percent;
            if(pick < category)
            {
                dropItem = dropTables[i].GetItem();
                break;
            }
        }

        //실제 아이템 오브젝트로 생성 
        ItemObject io = ItemManager.Instance.GetItemObject(dropItem);
        Transform itemBox = io.transform;

        itemBox.transform.position = transform.position;
    }
}
