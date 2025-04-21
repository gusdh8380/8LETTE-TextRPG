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
            Console.WriteLine();

            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();

            // 플레이어 정보
            Console.WriteLine("Lv. 1");
            Console.WriteLine("직업  : 전사");
            Console.WriteLine("공격력 : 10");
            Console.WriteLine("방어력 : 5");
            Console.WriteLine("체력  : 100");
            Console.WriteLine("골드  : 1500 G");

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

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
