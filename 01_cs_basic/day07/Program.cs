using System;
using System.Collections.Generic;

namespace day07
{
    public enum JOB
    {
        Warrior,
        Archor,
        Wizard,
    }

    public enum TYPE
    {
        Weapon,
        Hat,
        Armor,
        Pants,

        Count,
    }
    
    class Item
    {
        public string name { get; private set; }
        public JOB job { get; private set; }
        public TYPE type { get; private set; }

        public Item(String name, JOB job, TYPE type)
        {
            this.name = name;
            this.job = job;
            this.type = type;
        }
    }
    
    class Player
    {
        public readonly string name;
        private Item[] equips;
        private Inventory inven;

        private static readonly string EMPTY = "비어있음";

        public Item weapon => equips[(int)TYPE.Weapon];
        public Item hat => equips[(int)TYPE.Hat];
        public Item armor => equips[(int)TYPE.Armor];
        public Item pants => equips[(int)TYPE.Pants];

        public Player(string name)
        {
            this.name = name;
            equips = new Item[(int)TYPE.Count];
            inven = new Inventory();
        }
        
        public void PrintInfo()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("이름 : {0}", name);
            Console.WriteLine("------------------");
            Console.WriteLine("무기 : {0}", weapon==null ? EMPTY : weapon.name);
            Console.WriteLine("모자 : {0}", hat==null ? EMPTY : hat.name);
            Console.WriteLine("상의 : {0}", armor==null ? EMPTY : armor.name);
            Console.WriteLine("하의 : {0}", pants==null ? EMPTY : pants.name);
            Console.WriteLine("------------------");
        }

        public Item EquipItem(Item item)
        {
            //equips의 type번째는 type에 해당하는 부위
            Item take = equips[(int)item.type]; //이전에 장비하고 있던 아이템 대입
            equips[(int)item.type] = item; //type번째에 해당하는 부위에 아이템 대입
            return take;
        }

        public Item EquipItem(Item item, out bool isSwap) //오버로딩
        {
            Item beforeEquip = EquipItem(item); //item을 장비하고 이전에 장비한 아이템 대입
            isSwap = beforeEquip != null; //이전에 장비한 아이템의 여부를 isSwap으로 대입
            return beforeEquip; //beforeEquip 반환
        }

        public Item UnequipItem(TYPE type)
        {
            Item take = equips[(int)type];
            equips[(int)type] = null;
            return take;
        }

        public void OpenInventory()
        {
            PrintInfo();
            inven.Open();
            inven.Print();
        }
    }
    
    class Inventory
    {
        List<Item> list;
        int selected = -1;

        public Inventory()
        {
            list = new List<Item>();
        }

        public void Push(Item item)
        {
            list.Add(item);
        }

        public Item Pop(int index)
        {
            Item popItem = list[index];
            list.RemoveAt(index);
            return popItem;
        }

        public void Open()
        {
            selected = 1;
        }

        public void Close()
        {
            selected = -1;
        }

        public void Print()
        {
            (int beforeLeft, int beforeTop) = Console.GetCursorPosition();

            int top = 0;
            Console.SetCursorPosition(30, top++);
            Console.WriteLine("----[인벤토리]----");
            for(int i=1; i<=6; ++i)
            {
                Console.SetCursorPosition(30, top++);
                if(selected == i)
                {
                    Console.Write(">>");
                }
                if(list.Count<=i)
                {
                    Console.WriteLine($"{i}. 비어있음");
                }
                else
                {
                    // i번째 아이템 접근
                    Item item = list[i];
                    Console.WriteLine("{0}. {1}", i, item.name);
                }
            }
            Console.SetCursorPosition(30, top++);
            Console.WriteLine("------------------");
            Console.SetCursorPosition(beforeLeft, beforeTop);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //객체 선언
            Player player = new Player("테스터");
            
            player.EquipItem(new Item("개 쩌는 하의", JOB.Archor, TYPE.Pants));
            
            player.OpenInventory();
        }
    }
}