using System.Numerics;

namespace _8LETTE_TextRPG
{
    internal class MainGame
    {
        static void Main(string[] args)
        {
            Screen? current = TownScreen.instance;
            while (current != null)
            {
                current.Show();
                current = current.Next();
            }
        }
    }
}
