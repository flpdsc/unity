using System;

namespace cs03
{
    class Program
    {
        static void Main(string[] args)
        {
            if (false) //Console.Read();
            {
                int read = Console.Read();
                read = Console.Read();
                read = Console.Read();
                //abc 입력
                Console.WriteLine(read); //99
            }

            if (false) //Console.ReadKey();
            {
                ConsoleKeyInfo keyinfo = Console.ReadKey(false); //false : 입력키 안 나옴
                Console.WriteLine("Key:{0}", keyinfo.Key);
                Console.WriteLine($"KeyChar:{keyinfo.KeyChar}");
                Console.WriteLine($"Modifiers:{keyinfo.Modifiers}");
            }

            if (false) //Split, Replace, Substring
            {
                //Split
                string list = "AAA,BBB,CCC,DDD";
                string[] names = list.Split(',');
                for(int i=0; i<names.Length; ++i)
                {
                    Console.WriteLine($"{i}번째 값은 : {names[i]}");
                }

                //Replace
                string someText = "My name is cat";
                someText = someText.Replace("cat", "bird");
                Console.WriteLine($"someText : {someText}");

                //Substring
                string stringEx = "Today is Friday";
                stringEx = stringEx.Substring(3);
                Console.WriteLine(stringEx); //ay is Friday

                string stringEx2 = "Today is Friday";
                stringEx2 = stringEx2.Substring(6, 2);
                Console.WriteLine(stringEx2); //is
            }

            if (true) //실습: 회원가입
            {
                string id = string.Empty;
                string pw, pwc;
                char masking = '*'; //caching
                
                while (true)
                {
                    Console.Clear();
                    pw = string.Empty;
                    pwc = string.Empty;

                    Console.Write("ID : ");
                    if (string.IsNullOrEmpty(id))
                    {
                        id = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine(id);
                    }

                    Console.Write("PW : ");
                    pw = InputPW(masking);

                    if (!IsRange(pw, 4, 20))
                    {
                        Console.WriteLine("비밀번호의 길이는 최소 4자, 최대 20자 입니다.");
                        Console.ReadKey();
                        continue;
                    }

                    Console.Write("PW Confirm : ");
                    pwc = InputPW(masking);

                    if (pw == pwc)
                    {
                        Console.WriteLine("비밀번호 같음");
                    }
                    else
                    {
                        Console.WriteLine("비밀번호 다름");
                    }
                    Console.ReadKey();
                }
            }
        }


        static bool IsRange(string str, int min, int max)
        {
            return str.Length >= min && str.Length <= max;
        }

        static string InputPW(char masking)
        {
            string inp = string.Empty;
            while (true)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                Console.Write(masking);
                inp += info.KeyChar;
            }
            return inp;
        }
    }
}