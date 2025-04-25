using _8LETTE_TextRPG.MonsterFolder;

namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class PlayerAttackScreen : Screen
    {
        public static readonly PlayerAttackScreen Instance = new PlayerAttackScreen();

        private bool isAttacked = false;
        private int userInput = -1;

        private void ShowAttackList()
        {
            //몬스터 객체를 불러와서 입력 번호와 정보를 출력
            MonsterSpawner.Instance.ShowMonsterInfo(true);

            //플레이어의 필요한 정보(레벨, 이름, 직업, 체력)를 출력
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{Player.Instance.Level.CurrentLevel} {Player.Instance.Name} ({Player.Instance.Job.Name})");
            Console.WriteLine($"HP {Player.Instance.Health} / {Player.Instance.MaxHealth}");
            Console.WriteLine($"MP {Player.Instance.Mana} / {Player.Instance.MaxMana}");

            Console.WriteLine();
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("전투!! - 플레이어의 공격");

            if (!isAttacked)
            {
                ShowAttackList();
                PrintNumAndString(0, "취소");

                PrintUserInstruction();
            }
            else
            {
                Player.Instance.AttackTo(MonsterSpawner.Instance.GetAllMonsters()[userInput]);

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
                Monster[] monsters = MonsterSpawner.Instance.GetAllMonsters();
                if (num < 1 || num > monsters.Length || monsters[num - 1].IsDead)
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