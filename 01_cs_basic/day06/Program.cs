using System;

namespace day06
{
    class Program
    {
        static void Main(string[] args)
        {
            Lotto win = new Lotto(new int[] {8, 11, 16, 19, 21, 25}, 40);
            List<Lotto> myLottos = new List<Lotto>();
            for(int i=0; i<10; ++i)
                myLottos.Add(Lotto.GenerateAuto());

            Console.WriteLine("당첨번호 : {0}", win);
            for(int i=0; i<myLottos.Count; ++i)
                Console.WriteLine("{0}번 : {1}, [순위 : {2}]", i, myLottos[i], Lotto.Compare(myLottos[i], win));
        }
    }
} 