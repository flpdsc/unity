using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//직렬화 
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

    public Item()
    {

    }

    public Item(string name, string grade, int str, int dex, int ap, int luk, int level)
    {
        this.name = name;
        this.grade = grade;
        this.str = str;
        this.dex = dex;
        this.ap = ap;
        this.luk = luk;
        this.level = level;
    }
}

public class SaveManager : MonoBehaviour
{
    [SerializeField] TextAsset csvText;
    [SerializeField] Item[] items;

    [ContextMenu("CSV Read")]
    public void ReadCSV()
    {
        string[] lines = csvText.text.Split('\n'); //데이터 전체를 띄어쓰기 기준으로 자름  
        items = new Item[lines.Length - 1]; //아이템 배열의 개수를 전체 데이터 수 -1개로 만듦 

        for (int i = 0; i < lines.Length; ++i)
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

    [ContextMenu("CSV Writer")]
    public void SaveCSV()
    {
        Item[] items = GetItemArray(3);
        string csv = string.Empty;

        for(int i=0; i<items.Length; ++i)
        {
            Item item = items[i];
            List<string> list = new List<string>();
            list.Add(item.name);
            list.Add(item.grade);
            list.Add(item.str.ToString());
            list.Add(item.dex.ToString());
            list.Add(item.ap.ToString());
            list.Add(item.luk.ToString());
            list.Add(item.level.ToString());

            csv += string.Join(",", list.ToArray()) + '\n';
        }

        SaveFile("csvItemData", csv);
    }


    private class ItemJson
    {
        public Item[] items;

        public ItemJson(Item[] items)
        {
            this.items = items;
        }

    }

    [ContextMenu("Convert to JSON")]
    public void ConvertToJson()
    {
        ItemJson itemJson = new ItemJson(GetItemArray(5));
        string json = JsonUtility.ToJson(itemJson, true);
        SaveFile("itemData", json);


    }

    [ContextMenu("Convert to Object")]
    public void ConvertToObject()
    {
        string json = LoadFile("itemData");
        object convert = JsonUtility.FromJson(json, typeof(ItemJson));
        ItemJson itemJson = convert as ItemJson;
        Item[] items = itemJson.items;

        this.items = items;
        Debug.Log("아이템 데이터 로드 완!");
    }

    private Item[] GetItemArray(int count)
    {
        Item[] items = new Item[count];
        for (int i = 0; i < items.Length; ++i)
        {
            string itemName = string.Format("롱소드 {0}", Random.Range(0, 100));
            Item newItem = new Item(itemName, "Hero", 100, 200, 10, 50, 200);
            items[i] = newItem;
        }
        return items;
    }

    private void SaveFile(string fileName, string text)
    {
        string path = string.Format("{0}/{1}.txt", Application.dataPath, fileName);
        StreamWriter sw = new StreamWriter(path);
        sw.Write(text);
        sw.Close();

        Debug.Log("파일 저장 완!");

    }

    private string LoadFile(string fileName)
    {
        string path = string.Format("{0}/{1}.txt", Application.dataPath, fileName);
        StreamReader sr = new StreamReader(path);
        string readToEnd = sr.ReadToEnd();
        sr.Close();
        return readToEnd;
    }
}