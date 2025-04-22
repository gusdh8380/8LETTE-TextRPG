namespace _8LETTE_TextRPG
{
    internal class MainGame
    {

        static void Main(string[] args)
        {
            List<Job> jobs = Job.GetJobs(); // 직업생성 추가
            Job selectedJob = jobs[0]; // 직업 선택 추가

            Player player = new Player("chad", jobs[0]); // 플레이어 생성추가

            StatusScreen.instance.Player = player; // 상태보기 화면에 플레이어 정보 전달 추가

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
