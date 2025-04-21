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
        }

        public void PrintUserInstruction()
        {
            if (!isRetry)   Console.WriteLine("원하시는 행동을 입력해주세요. ");
            else            Console.WriteLine("잘못된 입력입니다! 다시 입력해주세요 : ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            Console.ResetColor();

            isRetry = false;
        }

        public void GameOver()
        {
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top);
            Console.Write(new String(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 2);
            Console.WriteLine("체력이 0이 되어 사망하였습니다....\n");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("게임 오버");
            Console.ResetColor();
        }

        public abstract void Show();
        public abstract Screen? Next();
    }
}
