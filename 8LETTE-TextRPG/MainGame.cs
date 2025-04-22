namespace _8LETTE_TextRPG
{
    internal class MainGame
    {
        public static Player player;
        public static Monster monster;
        static void Main(string[] args)
        {
            player = new Player("8LETTE", Job.GetJobs().First());
            Monster infLoopMonster = new InfLoop();
            Screen? current = TownScreen.instance;

            while (current != null)
            {
                current.Show();
                current = current.Next();
            }
        }
    }
}
