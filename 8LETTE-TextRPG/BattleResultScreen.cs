using static _8LETTE_TextRPG.MainGame;


namespace _8LETTE_TextRPG
{
    internal class BattleResultScreen : Screen
    {

        public static readonly BattleResultScreen Instance = new BattleResultScreen();
        private int testHP = 1; //지금은 테스트용
        Player player = MainGame.player;

        //전투의 결과를 출력
        public void BattleResult()
        {
            //전투 이후 체력 저장
            BattleData.PlayerHpAfterBattle = MainGame.player.Health;
            if (testHP <= 0) //여기서 플레이어의 체력 받아와야 함
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
                Console.WriteLine($"던전에서 몬스터 {BattleData.KillCountInThisBattle}마리를 잡았습니다.\n");
            }
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("전투 결과");

            BattleResult();

            Console.WriteLine($"Lv {player.Level}. {player.Name}");     //플레이어 레벨 / 이름 정보
            Console.WriteLine($"{BattleData.PlayerHpBeforeBattle} -> {BattleData.PlayerHpAfterBattle}");  //플레이어 전 체력 -> 현 체력

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            if (testHP <= 0) 
            {
                GameOver();
                return null;
            }

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
