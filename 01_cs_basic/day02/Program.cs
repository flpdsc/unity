using System;

namespace day02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region c#의 자료형

            if (false)
            {
                Console.WriteLine("Hello World!");

                int number = 5000;
                float weight = 50.5f;
                double pi = 3.1415;
                char word = 'a';
                string name = "AAA";

                // object 자료형 : c#의 모든 자료형이 상속하고 있는 자료형. (최상위 자료형?)
                // 그렇기 때문에 어떠한 자료형의 값이라도 대입받을 수 있다.
                // 박싱 : boxing
                // => 대입한 메모리를 감싸서 Heap에 올려두고 참조하는 것.
                object box = number;

                // castring (=>자료형 변환)
                // 언박싱 : unboxing
                // => 오브젝트가 감싸고 있던 메모리를 다시 원래대로 되돌려주는 것.
                int score = (int)box;

                string text = number.ToString("#,##0");

                Console.WriteLine(text);

                text = weight.ToString();
                text = pi.ToString();
                text = word.ToString();

                // Equals는 object 클래스에 존재하는 함수다.
                // 따라서 어떠한 자료형도 호출할 수 있고 매게변수를 object로 받기 때문에
                // 역시나 어떠한 자료형도 받아와서 비교할 수 있다.
                if (!number.Equals(name))
                {
                    // WriteLine도 오버로딩된 함수의 매게변수 혹은 object로 변수를 받아와
                    // ToString() 함수를 호출해 문자열을 출력한다.
                    Console.WriteLine(name);
                }
            }

            #endregion

            #region string과 문자열

            int aaa = 100;
            int bbb = 200;

            // 문자열 상수는 immutable type이다. (불변하는 값)
            // string 변수는 mutable type이다. (불변하지 않는 값)
            // string은 문자열을 참조한다. (문자열이 아니다.)
            string job = "warrior";
            job = "archor";
            for(int i = 0; i<job.Length; i++)
                Console.WriteLine("{0} : {1}", i, job[i]);
            
            // 따라서 job[0]에다 'a'를 대입하는 행위는 허용되지 않는다.
            // 불변하는 값인 문자열 상수에 접근했기 때문.

            // 출력문
            // 1번째 매게변수는 문자열 서식
            // 2번째 이상부터는 서식에 들어갈 값들.
            // {0}은 0번째 값, {1}은 1번째 값.
            Console.WriteLine("AAA:{1}, BBB:{0},{0}", aaa, bbb);
            Console.WriteLine($"AAA:{aaa}, BBB:{bbb}");

            #endregion

            // 비밀번호 :
            // 비밀번호 확인 :
            // 두 값이 동일하면 '비밀번호가 동일합니다.'
            // 두 값이 다르면 '비밀번호가 다릅니다.' 출력            
            string password;
            string passwordRe;

            Console.Write("비밀번호 : ");
            password = Console.ReadLine();

            Console.Write("비밀번호 확인 : ");
            passwordRe = Console.ReadLine();

            if(password == passwordRe)
            {
                Console.WriteLine("비밀번호가 동일합니다.");
            }
            else
            {
                Console.WriteLine("비밀번호가 다릅니다.");
            }
        }
    }
}