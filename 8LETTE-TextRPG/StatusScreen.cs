namespace _8LETTE_TextRPG
{
    internal class StatusScreen : Screen
    {
        public static readonly StatusScreen instance = new StatusScreen();
        private StatusScreen() { }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("상태 보기");

            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            // 플레이어 정보
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ({player.Job.Name})");
            Console.WriteLine($"공격력 : {player.BaseAttack}");
            Console.WriteLine($"방어력 : {player.BaseDefense}");
            Console.WriteLine($"체  력 : {player.Health}");
            Console.WriteLine($"골  드 : {player.Gold}");

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
