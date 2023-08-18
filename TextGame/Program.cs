namespace TextGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = 0;

            while (true)
            {
                Player player = new Player(1, "Chad", "전사", 10, 5, 100, 1500);

                StartMenu();
            }

            


            void StartMenu()
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

                input = int.Parse(Console.ReadLine());
            }

            
            
        }

        


    }
}

