using _8LETTE_TextRPG.MonsterFolder;
using _8LETTE_TextRPG.SkillFolder;

namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class PlayerSkillScreen : Screen
    {
        public static readonly PlayerSkillScreen Instance = new PlayerSkillScreen();

        private bool isAttacked = false;
        private bool hasMana;
        private int userInput = -1;

        private void ShowSkillList()
        {
            //몬스터 객체를 불러와서 정보를 출력
            DungeonManager.Instance.ShowMonsterInfo();

            //플레이어의 필요한 정보(레벨, 이름, 직업, 체력)를 출력
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{Player.Instance.Level.CurrentLevel} {Player.Instance.Name} ({Player.Instance.Job.Name})");
            Console.WriteLine($"HP {Player.Instance.Health} / {Player.Instance.Job.BaseHealth}");
            Console.WriteLine();

            //번호와 함께 플레이어의 스킬 정보를 출력
            Skill[] skills = Player.Instance.Job.Skills.ToArray();
            for (int i = 0; i < skills.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {skills[i].Name} - MP {skills[i].ManaCost}");
                Console.WriteLine($"   {skills[i].Description}");
                Console.WriteLine();
            }
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
                Monster monster = DungeonManager.Instance.GetAllMonsters().Where(m => m.IsDead == false).ToArray()[0];
                hasMana = Player.Instance.Job.Skills.ToArray()[userInput].Execute(Player.Instance, monster);

                PrintAnyKeyInstruction();
            }
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            if (isAttacked)
            {
                if (!hasMana)
                {
                    isAttacked = false;
                    return this;
                }
                isAttacked = false;
                //만약 몬스터가 모두 죽었다면, 전투 결과 화면으로 이동
                if (DungeonManager.Instance.IsAllDead())
                {
                    return BattleResultScreen.Instance;
                }

                //아니면, 몬스터 공격 화면으로 이동
                return MonsterAttackScreen.Instance;
            }

            if (input == "0")
            {
                return ActionSelectScreen.Instance;
            }
            else if (int.TryParse(input, out int num))
            {
                Skill[] skills = Player.Instance.Job.Skills.ToArray();
                if (num < 1 || num > skills.Length)
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
