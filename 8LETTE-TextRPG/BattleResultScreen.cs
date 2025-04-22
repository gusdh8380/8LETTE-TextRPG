namespace _8LETTE_TextRPG
{
    internal class BattleResultScreen : Screen
    {
        public static readonly BattleResultScreen Instance = new BattleResultScreen();

        //전투의 결과를 출력
        public void BattleResult()
        {
            if (Player.Instance.IsDead)
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

                //몬스터 몇 마리 잡았는지 정보
                Console.WriteLine($"던전에서 몬스터 {MonsterSpawner.Instance.MonsterCount}마리를 잡았습니다.\n");

                //플레이어 레벨 / 이름
                Console.WriteLine($"Lv.{Player.Instance.Level.CurrentLevel} {Player.Instance.Name}");
                //플레이어 전 체력 -> 현 체력
                Console.WriteLine($"HP {MonsterSpawner.Instance.PreviousHP} -> {Player.Instance.Health}");
            }
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("전투 결과");

            BattleResult();

            Console.WriteLine();
            PrintAnyKeyInstruction();
        }

        public override Screen? Next()
        {
            if (Player.Instance.IsDead)
            {
                GameOver();
                return null;
            }

            Console.ReadKey();
            return TownScreen.Instance;
        }
    }
}
