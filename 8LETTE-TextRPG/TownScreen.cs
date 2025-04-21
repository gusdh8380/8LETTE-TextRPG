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

            PrintTitle(" 마을\n");

            Console.WriteLine(" 스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine(" 이제 전투를 시작할 수 있습니다.");

            Console.WriteLine("\n 1. 상태 보기");
            Console.WriteLine(" 2. 전투 시작");
            Console.WriteLine();

            

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            switch (input)
            {

                case "0":
                    GameOver();
                    return null;

                case "1":
                    return StatusScreen.instance; // StatusScreen으로 이동

                case "2":
                    //Console.WriteLine()
                    //isRetry = true;
                    //return this;

                default:
                    isRetry = true;
                    return this;
            }
        }

    }
}
