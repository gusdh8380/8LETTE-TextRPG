namespace _8LETTE_TextRPG
{
    internal class TownScreen : Screen
    {
        public static readonly TownScreen instance = new TownScreen();
        private TownScreen() { }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("마을");

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

            PrintNumAndString(1, "상태 보기");
            PrintNumAndString(2, "전투 시작");
            PrintNumAndString(4, "상점");
            PrintNumAndString(0, "게임 종료");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            switch (input)
            {
                case "0": return null;
                case "1": return StatusScreen.instance;
                case "2":
                    MonsterSpawner.instance.InitMonsters(Player.Instance);
                    return ActionSelectScreen.instance;
                case "4": return ShopScreen.instance;
                default:
                    isRetry = true;
                    return this;
            }
        }
    }
}
