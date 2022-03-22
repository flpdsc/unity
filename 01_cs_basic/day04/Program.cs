using System;
using System.Collections.Generic;
using System.IO;

namespace day04
{
    class Inventory
    {
        public readonly int MAX_COUNT; // 상수 : 생성자에 한 번 값 대입 가능

        string[] items; //실제 아이템 베열
        int count; //아이템 갯수

        #region 프로퍼티(속성) getter, setter
        // public int Count
        // {
        //     get
        //     {
        //         return count;
        //     }

        //     set
        //     {
        //         count = value;
        //     }
        // }
        #endregion

        public int Count => count; //public 변수 Count에 접근하면 count 값을 넘겨줌

        //생성자가 호출되면서 내부 멤버변수도 초기화되기 때문에 readonly 상수 대입 가능
        public Inventory(int maxCount)
        {
            MAX_COUNT = maxCount;
            items = new string[maxCount];
        }

        public void Add(string item)
        {
            if(count>=MAX_COUNT)
            {
                Console.WriteLine("가방이 꽉 찼습니다.");
                return;
            }
            Console.WriteLine("{0}을 추가했습니다.", item);
            items[count] = item;
            count++;
        }

        public string GetList()
        {
            return string.Join(',', items);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //문자열
            if(false)
            {
                string names = "Hello World!";
                
                //split : 특정 문자 기준으로 자르기
                string[] split = names.Split(' ');
                for(int i=0; i<split.Length; ++i)
                    Console.WriteLine("{0}. {1}", i, split[i]);

                //replace : 특정 문자열을 원하는 문자열로 변경
                string replace = names.Replace("Hello", "Bye");
                Console.WriteLine($"names : {names}");
                Console.WriteLine($"replace : {replace}");
                
                //substring : 특정 index부터 끝까지 추출
                string substring = names.Substring(4);
                Console.WriteLine($"substring : {substring}");
                substring = names.Substring(7, 2);
                Console.WriteLine($"substring : {substring}");
                

                //contains : 특정 문자열이 포함되어 있는지 확인
                bool isContains = names.Contains("World");
                Console.WriteLine("Contains : {0}에는 {1}이 포함되어 있는가? : {2}", names, "World", isContains);

                //format : 원하는 형식으로 문자 조합
                string text1 = "Monday";
                string text2 = "Coming";
                string format = string.Format("{0} is {1}.", text1, text2);
                Console.WriteLine("format : {0}", format);

                //IndexOf : 위치 찾기
                int index = names.IndexOf("World!");
                Console.WriteLine("IndexOf : World!는 {0}번째에 있습니다.", index);
                index = names.IndexOf("Worlds");
                Console.WriteLine("IndexOf : World!는 {0}번째에 있습니다.", index); //없으면 -1 출력

                //Join : 문자 배열 조합
                string[] words = new string[] {"Dog", "Cat", "Bird"};
                Console.WriteLine("Join : {0}", string.Join(',', words));                
                Console.WriteLine("Join : {0}", string.Join("이랑 ", words));
            }

            //파일 읽기
            if(false)
            {
                string[] reads = ReadFile("test");
                Console.WriteLine("----텍스트 파일 읽기----");
                for(int i=0; i<reads.Length; ++i)
                    Console.WriteLine(reads[i]);
                Console.WriteLine("------------------------");
            }

            //상수
            if(false)
            {
                const int NUMBER = 10;
                //NUMBER = 20; //(X)
                const int MAX_NAME_LENGTH = 20;
                //const int MAX_NAME_LENGTH; //(X)
            }
        
            //실습
            //1. 인벤토리 아이템을 직접 입력해서 추가가능
            //2. 인벤토리의 아이템을 확인가능
            //[인벤토리 (n/N)]
            //= AAA, BBB, CCC
            //아이템 입력 : <입력받기>
            if(true)
            {
                Inventory inventory1 = new Inventory(3);
                Inventory inventory2 = new Inventory(21);

                Console.WriteLine("inventory1의 size : {0}", inventory1.MAX_COUNT);
                Console.WriteLine("inventory2의 size : {0}", inventory2.MAX_COUNT);
                
                while(true)
                {
                    Console.Clear();
                    Console.WriteLine("[인벤토리 ({0}/{1})]", inventory1.Count, inventory1.MAX_COUNT);
                    Console.WriteLine("= {0}", inventory1.GetList());
                    Console.Write("아이템 입력 : ");
                    inventory1.Add(Console.ReadLine());
                    Console.ReadKey();                 
                }
            }
        }

        static string[] ReadFile(string fileName)
        {
            List<string> lineList = new List<string>();
            //파일 읽기
            string porjectPath = Directory.GetParent(Environment.CurrentDirectory).FullName;
            string path = string.Format("{0}/{1}.txt", porjectPath, fileName);

            try
            {
                //using이 끝나면 sr을 자동으로 닫는다
                using(StreamReader sr = new StreamReader(path)) //스트림리더를 path 경로의 파일로 객체 생성
                {
                    while(sr.EndOfStream == false)  //마지막을 가리키고 있지 않다면
                    {
                        lineList.Add(sr.ReadLine());    //한 줄 읽어와서 list에 추가
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"파일 읽기 에러 : {ex.Message}");
            }
            return lineList.ToArray(); //List를 Array로 변환 후 리턴
        }
    }
}