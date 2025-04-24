using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG
{
    internal class MainGame
    {
        static void Main(string[] args)
        {
            //이름 입력
            Console.WriteLine("이름을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\n>> ");
            Console.ResetColor();
            string? userName = Console.ReadLine();
            userName = string.IsNullOrEmpty(userName) ? "8LETTE" : userName;

            new Player(userName, new Junior());

            Screen? current = TownScreen.Instance;
            while (current != null)
            {
                current.Show();
                current = current.Next();
            }
        }
    }
}
