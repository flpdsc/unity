using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Item
{
    public string name;
    public string grade;
    public int str;
    public int dex;
    public int ap;
    public int luk;
    public int level;
}

public class SaveManager : MonoBehaviour
{
    [SerializeField] TextAsset csvText;
    [SerializeField] Item[] items;

    [ContextMenu("CSV Read")]
    public void ReadCSV()
    {
        string[] lines = csvText.text.Split('\n'); //데이터 전체를 띄어쓰기 기준으로 자름  
        items = new Item[lines.Length-1]; //아이템 배열의 개수를 전체 데이터 수 -1개로 만듦 

        for(int i=0; i<lines.Length; ++i)
        {
            if (i <= 0) continue;

            string[] elements = lines[i].Split(',');
            Item item = new Item();
            item.name = elements[0];
            item.grade = elements[1];
            item.str = int.Parse(elements[2]);
            item.dex = int.Parse(elements[3]);
            item.ap = int.Parse(elements[4]);
            item.luk = int.Parse(elements[5]);
            item.level = int.Parse(elements[6]);

            items[i - 1] = item;
        }
    }
}