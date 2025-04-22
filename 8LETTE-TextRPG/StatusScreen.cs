using System.Security.Cryptography.X509Certificates;

namespace _8LETTE_TextRPG
{
    internal class StatusScreen : Screen
    {
        public static readonly StatusScreen instance = new StatusScreen();

        private StatusScreen() { }

        public Player Player { get; set; } // Player.cs 에서 정보를 가져옴

        public override void Show()
        {
            Console.Clear();

            PrintTitle("상태 보기");

            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            // 플레이어 정보
            Console.WriteLine($"Lv. {Player.Level}");
            Console.WriteLine($"{Player.Name} ({Player.Job.Name})");
            Console.WriteLine($"공격력 : {Player.BaseAttack}");
            Console.WriteLine($"방어력 : {Player.BaseDefense}");
            Console.WriteLine($"체력 : {Player.Health}");
            Console.WriteLine($"골드 : {Player.Gold}");

            Console.WriteLine();
            PrintNumAndString(0, "나가기");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            if (input == "0")
            {
                return TownScreen.instance;
            }
            else
            {
                isRetry = true;
            }
            
            return this;
        }
    }
}
