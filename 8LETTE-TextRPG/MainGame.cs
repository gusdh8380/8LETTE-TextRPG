using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG
{
    internal class MainGame
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ShowIntro();

            new Player();

            Screen? current = TownScreen.Instance;
            while (current != null)
            {
                current.Show();
                current = current.Next();
            }
        }

        public static void ShowIntro()
        {
            Console.Clear();

            string[] introLines = new string[]
            {
                @"",
                @" /$$$$$$$$ /$$           /$$         /$$    /$$",
                @"| $$_____/|__/          | $$        | $$   | $$",
                @"| $$       /$$  /$$$$$$ | $$$$$$$  /$$$$$$ | $$",
                @"| $$$$$   | $$ /$$__  $$| $$__  $$|_  $$_/ | $$",
                @"| $$__/   | $$| $$  \ $$| $$  \ $$  | $$   |__/",
                @"| $$      | $$| $$  | $$| $$  | $$  | $$ /$$   ",
                @"| $$$$$$$$| $$|  $$$$$$$| $$  | $$  |  $$$$//$$",
                @"|________/|__/ \____  $$|__/  |__/   \___/ |__/",
                @"               /$$  \ $$                       ",
                @"              |  $$$$$$/                       ",
                @"               \______/                        ",
                @"",
                @" /$$$$$$$                                                   ",
                @"| $$__  $$                                                  ",
                @"| $$  \ $$  /$$$$$$  /$$$$$$$   /$$$$$$   /$$$$$$   /$$$$$$ ",
                @"| $$$$$$$/ |____  $$| $$__  $$ /$$__  $$ /$$__  $$ /$$__  $$",
                @"| $$__  $$  /$$$$$$$| $$  \ $$| $$  \ $$| $$$$$$$$| $$  \__/",
                @"| $$  \ $$ /$$__  $$| $$  | $$| $$  | $$| $$_____/| $$      ",
                @"| $$  | $$|  $$$$$$$| $$  | $$|  $$$$$$$|  $$$$$$$| $$      ",
                @"|__/  |__/ \_______/|__/  |__/ \____  $$ \_______/|__/      ",
                @"                               /$$  \ $$                    ",
                @"                              |  $$$$$$/                    ",
                @"                               \______/                     "
            };

            int consoleWidth = Console.WindowWidth;
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var line in introLines)
            {
                int lineWidth = GetDisplayWidth(line);
                int padding = Math.Max((consoleWidth - lineWidth) / 2, 0);
                Console.WriteLine(new string(' ', padding) + line);
            }
            Console.ResetColor();

            Console.WriteLine("\n");

            string pressKey = "아무 버튼이나 눌러주세요.";
            int pressWidth = GetDisplayWidth(pressKey);
            int pressPadding = Math.Max((consoleWidth - pressWidth) / 2, 0);
            Console.Write(new string(' ', pressPadding) + pressKey);

            Console.ReadKey();

            Console.Clear();
        }

        private static int GetDisplayWidth(string s)
        {
            int width = 0;
            foreach (char c in s)
            {
                if (c >= 0xAC00 && c <= 0xD7A3) // 한글 유니코드 범위
                    width += 2;
                else
                    width += 1;
            }
            return width;
        }
    }
}
