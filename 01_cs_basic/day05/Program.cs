using System;

namespace day05
{
    class Vector2
    {
        // 자동 프로퍼티
        // get은 public, set은 private
        public int x { get; private set; }
        public int y { get; private set;}
        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        //함수 오버라이딩
        public override string ToString()
        {
            return String.Format($"({x}, {y})");
        }

        //연산자 오버로딩
        public static Vector2 operator+(Vector2 p1, Vector2 p2)
        {
            return new Vector2(p1.x+p2.x, p1.y+p2.y);
        }

        public static bool operator==(Vector2 p1, Vector2 p2)
        {
            return (p1.x==p2.x && p1.y==p2.y);
        }
        public static bool operator!=(Vector2 p1, Vector2 p2)
        {
            return (p1.x!=p2.x || p1.y!=p2.y);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Vector2 position = new Vector2(0, 0);
            Console.WriteLine("x: {0}, y: {1}", position.x, position.y);
            Console.WriteLine(position); //ToString 함수 오버라이딩

            //연산자 오버로딩
            Vector2 pos1 = new Vector2(1, 3);
            Vector2 pos2 = new Vector2(4, 2);
            Vector2 sum = pos1+pos2;
            Console.WriteLine("pos1 + pos2 = {0}", sum);

            pos1 = new Vector2(1, 1);
            pos2 = new Vector2(1, 1);

            Console.WriteLine("pos1과 pos2는 같은 값인가? {0}", pos1==pos2);
            Console.WriteLine("pos1과 pos2는 같은 주소값인가? {0}", pos1.Equals(pos2));
        }        
    }
}