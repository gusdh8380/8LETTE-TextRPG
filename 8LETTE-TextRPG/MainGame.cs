namespace _8LETTE_TextRPG
{
    internal class MainGame
    {
        static void Main(string[] args)
        {
            List<Job> jobs = Job.GetJobs(); //직업생성 추가
            Job selectedJob = jobs[0];      //직업선택 추가

            Player player = new Player("chad", jobs[0]);

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
