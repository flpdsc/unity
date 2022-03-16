using System;
using MY;

namespace MY
{

    enum WEEK
    {
        MON,
        TUE,
        WEN
    }
}

namespace _01_cs_basic
{
    struct State
    {
        //public: 묶어쓰지 않고 각자 사용
        public int hp;
        public int power;
    }

    class Item
    {
        public int hp;
        public int power;

        //생성자
        public Item()
        {
            hp = 0;
            power = 0;
        }
        public Item(int hp, int power)
        {
            this.hp = hp;
            this.power = power;
        }

        public void Print()
        {
            Console.WriteLine($"HP:{hp}, POWER:{power}");
        }
    }


    class Program
    {

        static void Main(string[] args)
        {
 
            bool isBool = true;
            int num = 10;
            float height = 150.3f;
            double pi = 3.141592;
            string name = "MY NAME";

            Console.Write("Hi! ");
            Console.WriteLine("Hello World!");

            Console.WriteLine("num : " + num);
            Console.WriteLine($"num : { num }");
            Console.WriteLine($"isBool : { isBool }");
            Console.WriteLine($"height : { height }");
            Console.WriteLine($"pi : { pi }");
            Console.WriteLine($"name : { name }");

            //열거형
            WEEK week = WEEK.MON;
            Console.WriteLine(week);

            // ::, -> 사용 불가 "." 사용

            State state = new State(); //값 타입
            Item item = new Item(); //참조 타입

            State state2 = state;
            Item item2 = item;

            state.hp = 100;
            item.hp = 100;

            Console.WriteLine(state2.hp); //0
            Console.WriteLine(item2.hp); //100

            Some();

            // GC 
            Item myItem = new Item();
            myItem.Print();

            //조건문
            int number = 10;
            if (number <= 10)
            {
                Console.WriteLine("10보다 작거나 같다.");
            }
            else if (number > 50)
            {
                Console.WriteLine("50보다 크다.");
            }
            else
            {
                Console.WriteLine("둘 다 아니다");
            }

            //반복문
            for (int i = 0; i < number; ++i)
            {
                Console.WriteLine("HI!");
            }

            //분기문
            WEEK weeks = WEEK.MON;
            switch (weeks)
            {
                case WEEK.MON:
                    Console.WriteLine("월!");
                    break;
                case WEEK.TUE:
                    Console.WriteLine("화!");
                    break;
                case WEEK.WEN:
                    Console.WriteLine("수!");
                    break;

            }

            //while문도 그대로

            //배열
            int[] numbers = new int[5];
            numbers[0] = 10;
            for(int i=0; i<5; ++i)
                Console.WriteLine(numbers[i]);

            int[] boxs = null; //아무런 메모리도 참조하지 않고 있다

            int num1 = 111;
            int num2 = 200;

            Swap(num1, num2); //참조타입으로 값을전달
            Console.WriteLine($"Num1 : {num1}, Num2 : {num2}");

            SwapRef(ref num1, ref num2); //참조타입으로 값을전달
            Console.WriteLine($"Num1 : {num1}, Num2 : {num2}");

            int[] array1 = new int[] { 1, 2, 3, 4 };
            int[] array2 = new int[] { 100, 200, 300, 400 };

            Swap(array1, array2);

            Console.WriteLine($"array1 : {string.Join(",", array1)}");
            Console.WriteLine($"array2 : {string.Join(",", array2)}");
        }

        static void Some()
        {

        }

        static void SwapRef(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        static void Swap(int a, int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        static void Swap(int[] array1, int[] array2)
        {
            for (int i = 0; i<array1.Length; ++i)
            {
                int tmp = array1[i];
                array1[i] = array2[i];
                array2[i] = tmp;
            }            
        }
    }
}