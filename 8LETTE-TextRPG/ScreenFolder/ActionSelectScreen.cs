namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class ActionSelectScreen : Screen
    {
        public static readonly ActionSelectScreen Instance = new ActionSelectScreen();

        //플레이어의 필요한 정보(레벨, 이름, 직업, 체력)를 출력
        private void ShowPlayerInfo()
        {
            Console.WriteLine("[내 정보]");

            Console.WriteLine($"Lv.{Player.Instance.Level.CurrentLevel} {Player.Instance.Name} ({Player.Instance.Job.Name}) ");
            Console.WriteLine($"HP {Player.Instance.Health} / {Player.Instance.MaxHealth} ");

            Console.WriteLine();
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("전투!!");

            MonsterSpawner.Instance.ShowMonsterInfo();
            
            ShowPlayerInfo();

            PrintNumAndString(1, "공격하기");
            PrintNumAndString(2, "스킬 사용");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    return PlayerAttackScreen.Instance;
                case "2":
                    return PlayerSkillScreen.Instance;
                default:
                    _isRetry = true;
                    return this;
            }
        }
    }
}
