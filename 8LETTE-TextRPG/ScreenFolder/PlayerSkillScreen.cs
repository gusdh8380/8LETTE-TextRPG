namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class PlayerSkillScreen : Screen
    {
        public static readonly PlayerSkillScreen Instance = new PlayerSkillScreen();

        private bool isAttacked = false;
        private int userInput = -1;

        private void ShowSkillList()
        {
            //몬스터 객체를 불러와서 정보를 출력
            MonsterSpawner.Instance.ShowMonsterInfo();

            //플레이어의 필요한 정보(레벨, 이름, 직업, 체력)를 출력
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{Player.Instance.Level.CurrentLevel} {Player.Instance.Name} ({Player.Instance.Job.Name})");
            Console.WriteLine($"HP {Player.Instance.Health} / {Player.Instance.MaxHealth}");

            //번호와 함께 플레이어의 스킬 정보를 출력
            //Player.Instance.ShowSkill();

            Console.WriteLine();
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("전투!! - 플레이어의 스킬 사용");

            if (!isAttacked)
            {
                ShowSkillList();
                PrintNumAndString(0, "취소");

                PrintUserInstruction();
            }
            else
            {
                //선택한 플레이어의 스킬 메서드 사용
                //Player.Instance.Fire(MonsterSpawner.Instance.GetAllMonsters());

                PrintAnyKeyInstruction();
            }
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            if (isAttacked)
            {
                isAttacked = false;
                //만약 몬스터가 모두 죽었다면, 전투 결과 화면으로 이동
                if (MonsterSpawner.Instance.IsAllDead()) return BattleResultScreen.Instance;

                //아니면, 몬스터 공격 화면으로 이동
                return MonsterAttackScreen.Instance;
            }

            if (input == "0")
            {
                return ActionSelectScreen.Instance;
            }
            else if (int.TryParse(input, out int num))
            {
                //플레이어의 스킬 번호를 벗어나거나 마력이 부족하면 아래 실행
                //Skill[] skills = Player.Instane.GetAllSkills();
                if (num < 1 || num > 4) //|| Player.Instane.MP < skills[num - 1].MP
                {
                    _isRetry = true;
                    return this;
                }

                userInput = num - 1;
                isAttacked = true;
            }
            else
            {
                _isRetry = true;
            }

            return this;
        }
    }
}
