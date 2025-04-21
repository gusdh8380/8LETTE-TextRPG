namespace _8LETTE_TextRPG
{
    internal abstract class Screen
    {
        protected bool isRetry = false;

        public void PrintTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(title);
            Console.ResetColor();

            Console.WriteLine();
        }

        public void PrintNumAndString(int num, string str)
        {
            Console.WriteLine($"{num}. {str}");
        }

        public void PrintUserInstruction()
        {
            if (!isRetry)   Console.WriteLine("\n원하시는 행동을 입력해주세요. ");
            else            Console.WriteLine("\n잘못된 입력입니다! 다시 입력해주세요 : ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            Console.ResetColor();

            isRetry = false;
        }

        public void GameOver()
        {
            Console.WriteLine("\n체력이 0이 되어 사망하였습니다....\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--- 게임 오버 ---");
            Console.ResetColor();
        }

        public abstract void Show();
        public abstract Screen? Next();
    }
}
