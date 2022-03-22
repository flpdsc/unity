using System;

namespace day05
{
    struct Vector2
    {
        public int x;
        public int y;

        public static Vector2 Up = new Vector2(0, 1);
        public static Vector2 Down = new Vector2(0, -1);
        public static Vector2 Left = new Vector2(-1, 0);
        public static Vector2 Right = new Vector2(1, 0);

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

        //연산자 오버로딩(+, -, 비교연산자)
        public static Vector2 operator+(Vector2 p1, Vector2 p2)
        {
            return new Vector2(p1.x+p2.x, p1.y+p2.y);
        }

        public static Vector2 operator+(Vector2 p1, VECTOR dir)
        {
            switch(dir)
            {
                case VECTOR.Up:
                    return p1+Vector2.Up;
                case VECTOR.Down:
                    return p1+Vector2.Down;
                case VECTOR.Left:
                    return p1+Vector2.Left;
                case VECTOR.Right:
                    return p1+Vector2.Right;
                default:
                    return p1;    
            }
        }

        public static Vector2 operator-(Vector2 p1, Vector2 p2)
        {
            return new Vector2(p1.x-p2.x, p1.y-p2.y);
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

    //구조체는 상속이 되지 않음
    //struct Vector3 : Vector2
    struct Vector3
    {
        public int x, y, z;
        public Vector3(int x)
        {
            this.x = x;
            y = 0;
            z = 0;
            Console.WriteLine("1번");
        }

        public Vector3(int x, int y) : this(x)
        {
            this.y = y;
            z = 0;
            Console.WriteLine("2번");
        }

        public Vector3(int x, int y, int z) : this(x, y)
        {
            this.z = z;
            Console.WriteLine("3번");
        }

        public Vector3(Vector2 position) : this(position.x, position.y)
        {
            z = 0;
        }
        
        public override string ToString()
        {
            return String.Format($"({x}, {y}, {z})");
        }

        public static Vector3 operator+(Vector3 p1, Vector3 p2)
        {
            return new Vector3(p1.x+p2.x, p1.y+p2.y, p1.z+p2.z);
        }
        public static Vector3 operator+(Vector3 vec3, Vector2 vec2)
        {
            return new Vector3(vec3.x+vec2.x, vec3.y+vec2.y, vec3.z);
        }

    }

    enum VECTOR
    {
        Up,
        Down,
        Left,
        Right,
    }

    class Program
    {
        static void Main(string[] args)
        {
            if(false)
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
                Console.WriteLine("pos1에서 pos2 방향은? {0}", pos2-pos1);
                pos1 += pos2;
                Console.WriteLine(pos1);

                Console.WriteLine("pos1과 pos2는 같은 값인가? {0}", pos1==pos2);
                //클래스 일 경우 False(참조형식), 구조체일 경우 True(값형식)
                Console.WriteLine("pos1과 pos2는 같은 주소값인가? {0}", pos1.Equals(pos2));

                Vector3 vec3 = new Vector3(1);
                Console.WriteLine(vec3);
                vec3 = new Vector3(1, 2);
                Console.WriteLine(vec3);
                vec3 = new Vector3(1, 2, 3);
                Console.WriteLine(vec3);

                Vector2 vec2 = new Vector2(-1, 2);
                Console.WriteLine("vec3:{0} + vec2:{1} = {2}", vec3, vec2, vec3+vec2);
            }
            if(false)
            {
                Vector2 position1 = new Vector2(0, 0);
                VECTOR dir = VECTOR.Up;

                position1 += dir;
                Console.WriteLine($"UP : {position1}");

                //열거형 출력
                Console.WriteLine(VECTOR.Up.ToString());
                VECTOR direction = VECTOR.Down;
                Console.WriteLine("direction의 값 : {0}, 타입 : {1}", direction, direction.GetType());

                //형변환(열거형->문자형)
                string str = direction.ToString();
                Console.WriteLine("str 값 : {0}, 타입 : {1}", str, str.GetType());

                //형변환(문자형->열거형)
                str = "Left";
                //str = "LEFT";
                //direction = (VECTOR)Enum.Parse(typeof(VECTOR), str); // "UP" 파싱 안되고 에러. 대소문자 구분
                bool isSuccess = Enum.TryParse(str, out direction); //틀리면 초기값으로 바꿈

                //파싱한 값 출력
                Console.WriteLine("파싱 성공 여부 : {0}", isSuccess);
                Console.WriteLine("direction의 값 : {0}({1}), 타입 : {2}", direction, (int)direction, direction.GetType());
            }
            if(true)
            {
                //숫자 -> 문자형
                int age = 100;
                string text = age.ToString();

                //문자형 -> 숫자형(정수)
                Console.Write("숫자를 입력하세요 : ");
                string input = Console.ReadLine();
                //age = int.Parse(input); //349.3sjkd 에러
                bool isParse = int.TryParse(input, out age);
                Console.WriteLine("성공여부 : {0}", isParse);
                Console.WriteLine("age의 값은 : {0:#,##0}, 타입은 : {1}", age, age.GetType());
            }
        }
        //out 키워드
        //1. 함수가 종료된 후 out 키워드가 붙은 변수에 어떠한 값이든 넣어주겠음을 보장
        //2. 외부에 여러가지 값을 전달하고 싶을 때 사용
        static bool Squt(out int number)
        {
            number = 10;
            return true;
        }        
    }
}