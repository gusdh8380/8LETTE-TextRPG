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

            //직업 선택
            List<Job> jobs = Job.GetJobs();
            Job selectedJob;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("직업을 선택해주세요.");

                for (int i = 0; i < jobs.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {jobs[i].Name}");
                }

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("\n>> ");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out int num))
                {
                    if (num < 1 || num > jobs.Count)
                    {
                        continue;
                    }

                    selectedJob = jobs[num - 1];
                    break;
                }
            }

            new Player(userName, selectedJob);

            Screen? current = TownScreen.Instance;
            while (current != null)
            {
                current.Show();
                current = current.Next();
            }
        }
    }
}
