namespace _8LETTE_TextRPG
{
    internal class BattleResultScreen : Screen
    {
        public static readonly BattleResultScreen instance = new BattleResultScreen();
        private BattleResultScreen() { }

        //전투의 결과를 출력
        public void BattleResult()
        {
            if (Player.Instance.Health <= 0) //여기서 플레이어의 체력 받아와야 함
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
                Console.WriteLine($"던전에서 몬스터 {MonsterSpawner.instance.MonsterNum}마리를 잡았습니다.\n");
            }

            //플레이어 레벨 / 이름
            Console.WriteLine($"Lv.{Player.Instance.Level} {Player.Instance.Name}");
            //플레이어 전 체력 -> 현 체력
            Console.WriteLine($"{MonsterSpawner.instance.PreviousHP} -> {Player.Instance.Health}");
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
            if (Player.Instance.Health <= 0) 
            {
                GameOver();
                return null;
            }

            Console.ReadKey();
            return TownScreen.instance;
        }
    }
}
