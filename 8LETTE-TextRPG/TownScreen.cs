using System.Numerics;

namespace _8LETTE_TextRPG
{
    internal class TownScreen : Screen
    {
        public static readonly TownScreen instance = new TownScreen();
        private TownScreen() { }

        public override void Show()
        {
            Console.Clear();

            Console.WriteLine(" 스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine(" 이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            Console.WriteLine("\n 1. 상태 보기");
            Console.WriteLine(" 2. 인벤토리");
            Console.WriteLine(" 3. 상점");
            Console.WriteLine(" 4. 던전입장");
            Console.WriteLine(" 5. 휴식하기");
            Console.WriteLine(" 0. 게임 종료\n");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "1": 
                default:
                    isRetry = true;
                    return this;
            }
        }

    }
}
