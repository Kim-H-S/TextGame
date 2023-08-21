namespace TextGame
{
    internal class Program
    {
        static Player player;

        static void Main(string[] args)
        {
            player = new Player(1, "Chad", "전사", 10, 5, 100, 1500);

            DisplayGameIntro();








        }

        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    player.DisplayInfo();
                    break;

                case 2:

                    break;

            }

        }


        static int CheckValidInput(int min, int max)
        {

            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (min <= ret && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }

        }


    }


    public class Player
    {
        public int level { get; }
        public string name { get; }
        public string classType { get; }
        public float attack { get; }
        public float defense { get; }
        public float health { get; }
        public long gold { get; }

        public Player(int level, string name, string classType, float attack, float defense, float health, long gold)
        {
            this.level = level;
            this.name = name;
            this.classType = classType;
            this.attack = attack;
            this.defense = defense;
            this.health = health;
            this.gold = gold;
        }

        public void DisplayInfo()
        {
            Console.Clear();

            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {level}");
            Console.WriteLine($"{name} ( {classType} )");
            Console.WriteLine($"공격력 : {attack}");
            Console.WriteLine($"방어력 : {defense}");
            Console.WriteLine($"체력 : {health}");
            Console.WriteLine($"Gold : {gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            Console.ReadLine();


        }
    }


}

