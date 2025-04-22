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
            PrintNumAndString(3, "퀘스트"); //퀘스트 선택지 추가
            PrintNumAndString(4, "진행중인 퀘스트"); //퀘스트 진행상황 추가(구현X)
            PrintNumAndString(5, "완료된 퀘스트"); //완료된 퀘스트 추가(구현X)
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
                    MonsterSpawner.instance.InitMonsters(player);
                    return ActionSelectScreen.instance;
                case "3": return QuestScreen. instance; // case 3번 추가
                default:
                    isRetry = true;
                    return this;
            }
        }
    }
}
