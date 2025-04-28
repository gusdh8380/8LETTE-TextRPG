using _8LETTE_TextRPG.ScreenFolder;

namespace _8LETTE_TextRPG
{
    internal class MainGame
    {
        static void Main(string[] args)
        {
            IntroScreen.ShowIntro();
            new Player();

            Screen? current = TownScreen.Instance;
            while (current != null)
            {
                current.Show();
                current = current.Next();
            }
        }
    }
}
