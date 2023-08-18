namespace TextGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");

            int input = int.Parse(Console.ReadLine());
            
            if (input == 1)
            {
                Console.WriteLine(input);
            }
            else if (input == 2)
            {
                Console.WriteLine(input);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

        }
    }
}