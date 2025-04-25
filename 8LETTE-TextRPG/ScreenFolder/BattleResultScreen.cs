namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class BattleResultScreen : Screen
    {
        public static readonly BattleResultScreen Instance = new BattleResultScreen();

        public Action? PrintDropItem;

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
                //전투 후 마나 회복 로직
                Player.Instance.Mana += 10f;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Victory");
                Console.ResetColor();
                Console.WriteLine();

                Console.WriteLine($"던전에서 몬스터 {DungeonManager.Instance.MonsterCount}마리를 잡았습니다.\n");

                Console.WriteLine("[캐릭터 정보]");
                if(Player.Instance.Level.CurrentLevel == DungeonManager.Instance.PreviousLevel)
                {
                    Console.WriteLine($"Lv.{Player.Instance.Level.CurrentLevel} {Player.Instance.Name}");
                }
                else
                {
                    Console.WriteLine($"Lv.{DungeonManager.Instance.PreviousLevel} {Player.Instance.Name} " +
                        $"-> {Player.Instance.Level.CurrentLevel} {Player.Instance.Name}");
                }
                Console.WriteLine($"체  력 : {DungeonManager.Instance.PreviousHP} -> {Player.Instance.Health}");
                Console.WriteLine($"경험치 : {DungeonManager.Instance.PreviousExp} -> {Player.Instance.Level.CurrentExp}\n");

                Console.WriteLine("[클리어 보상]");
                Console.WriteLine($"골  드 : {DungeonManager.Instance.PreviousGold}G -> {Player.Instance.Gold}G");
                PrintDropItem?.Invoke();

                PrintDropItem = null;
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
            //if (Player.Instance.IsDead)
            //{
            //    GameOver();
            //    return null;
            //}

            Console.ReadKey();
            return TownScreen.Instance;
        }
    }
}
