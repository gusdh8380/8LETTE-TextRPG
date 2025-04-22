namespace _8LETTE_TextRPG
{
    internal class ActionSelectScreen : Screen
    {
        public static readonly ActionSelectScreen Instance = new ActionSelectScreen();

        private void ShowPlayerInfo()
        {
            //플레이어의 필요한 정보(레벨, 이름, 직업, 체력)를 출력
            Console.WriteLine("[내 정보]");

            Console.WriteLine($"Lv.{Player.Instance.Level.CurrentLevel} {Player.Instance.Name} ({Player.Instance.Job.Name}) ");
            Console.WriteLine($"HP {Player.Instance.Health} / {Player.Instance.Job.BaseHealth} ");

            Console.WriteLine();
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("전투!!");

            MonsterSpawner.Instance.ShowMonsterInfo();
            
            ShowPlayerInfo();

            PrintNumAndString(1, "공격");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    return PlayerAttackScreen.Instance;
                default:
                    isRetry = true;
                    return this;
            }
        }
    }
}
