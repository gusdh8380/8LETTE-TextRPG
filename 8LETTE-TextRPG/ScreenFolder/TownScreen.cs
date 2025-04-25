namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class TownScreen : Screen
    {
        public static readonly TownScreen Instance = new TownScreen();

        public override void Show()
        {
            Console.Clear();

            PrintTitle("마을");

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.\n");

            PrintNumAndString(1, "상태 보기");
            PrintNumAndString(2, $"전투 시작 ({MonsterSpawner.Instance.Type} Dungeon - {(MonsterSpawner.Instance.ClearCount % 5 + 1)}층)");
            PrintNumAndString(3, "인벤토리");
            PrintNumAndString(4, "상점");
            PrintNumAndString(5, "퀘스트");
            PrintNumAndString(6, "휴식");
            PrintNumAndString(0, "게임 종료");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    return null;
                case "1":
                    return StatusScreen.Instance;
                case "2":
                    MonsterSpawner.Instance.InitMonsters();
                    return ActionSelectScreen.Instance;
                case "3":
                    return InventoryScreen.Instance;
                case "4":
                    return ShopScreen.Instance;
                case "5":
                    return QuestScreen.Instance;
                case "6":
                    return QuestScreen.Instance;
                default:
                    _isRetry = true;
                    return this;
            }
        }
    }
}