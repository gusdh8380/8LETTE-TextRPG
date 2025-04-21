namespace _8LETTE_TextRPG
{
    internal class BattleResultScreen : Screen
    {
        public static readonly BattleResultScreen Instance = new BattleResultScreen();
        private int hp = 1; //플레이어의 체력 받아와야 함

        //전투의 결과를 출력
        public void BattleResult()
        {
            if (hp <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Lose");
                Console.ResetColor();

                Console.WriteLine();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Victory");
                Console.ResetColor();
                Console.WriteLine();

                Console.WriteLine("던전에서 몬스터 3마리를 잡았습니다.\n");  //몬스터 몇 마리 잡았는지 정보
            }
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("Battle!! - Result");

            BattleResult();

            Console.WriteLine("Lv.1 Chad");     //플레이어 레벨 / 이름 정보
            Console.WriteLine("HP 100 -> 74");  //플레이어 전 체력 -> 현 체력

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "0": return TownScreen.instance;
                default:
                    isRetry = true;
                    return this;
            }
        }
    }
}
