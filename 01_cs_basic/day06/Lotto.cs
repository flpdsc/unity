using System;

namespace day06
{
    class Lotto
    {
        int[] lotto;
        int bonus;

        public Lotto()
        {
            lotto = null;
            bonus = 0;
        }

        public Lotto(int[] lotto, int bonus)
        {
            this.lotto = lotto;
            this.bonus = bonus;
        }

        private static Random random = new Random();

        public static void Generate(out int[] lotto, out int bonus)
        {
            Stack<int> stack = new Stack<int>();

            while(stack.Count<7)
            {
                int r = random.Next(1, 46);
                if(!stack.Contains(r))
                    stack.Push(r);
            }

            bonus = stack.Pop();
            lotto = stack.ToArray();
            Array.Sort(lotto);
        }

        public static Lotto GenerateAuto()
        {
            List<int> list = new List<int>();

            while(list.Count<7)
            {
                int r = random.Next(1, 46);

                if(!list.Contains(r))
                    list.Add(r);
            }

            Lotto newLotto = new Lotto();
            newLotto.bonus = list[list.Count-1];
            list.RemoveAt(list.Count-1);
            list.Sort();
            newLotto.lotto = list.ToArray();

            return newLotto;
        }

        public static int Compare(Lotto my, Lotto win)
        {
            int compare = 0;
            bool isBonus = my.bonus ==win.bonus;

            for(int i=0; i<my.lotto.Length; ++i)
            {
                if(win.lotto.Contains(my.lotto[i]))
                {
                    compare++;
                }
            }

            if(compare == 6) return 1;
            else if(compare == 5 && isBonus) return 2;
            else if(compare == 5) return 3;
            else if(compare == 4) return 4;
            else if(compare == 3) return 5;
            else return 0; 
        }

        public override string ToString()
        {
            return String.Format("{0} + {1}", string.Join(',', lotto), bonus);
        }
    }
}