namespace _8LETTE_TextRPG
{
    internal class MainGame
    {
        static void Main(string[] args)
        {
            Player player = new Player("8LETTE", Job.GetJobs().First());

            Screen? current = TownScreen.instance;

            while (current != null)
            {
                current.player = player;
                current.Show();
                current = current.Next();
            }
        }
    }
}
