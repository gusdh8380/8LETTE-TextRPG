namespace _8LETTE_TextRPG.ScreenFolder
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

                Console.WriteLine($"던전에서 몬스터 {MonsterSpawner.Instance.MonsterCount}마리를 잡았습니다.\n");

                Console.WriteLine("[캐릭터 정보]");
                if(Player.Instance.Level.CurrentLevel == MonsterSpawner.Instance.PreviousLevel)
                {
                    Console.WriteLine($"Lv.{Player.Instance.Level.CurrentLevel} {Player.Instance.Name}");
                }
                else
                {
                    Console.WriteLine($"Lv.{MonsterSpawner.Instance.PreviousLevel} {Player.Instance.Name} " +
                        $"-> {Player.Instance.Level.CurrentLevel} {Player.Instance.Name}");
                }
                Console.WriteLine($"체  력 : {MonsterSpawner.Instance.PreviousHP} -> {Player.Instance.Health}");
                Console.WriteLine($"경험치 : {MonsterSpawner.Instance.PreviousExp} -> {Player.Instance.Level.CurrentExp}\n");

                Console.WriteLine("[클리어 보상]");
                Console.WriteLine($"골  드 : {MonsterSpawner.Instance.PreviousGold} -> {Player.Instance.Gold}");
            }
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("전투 결과");

            BattleResult();

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
