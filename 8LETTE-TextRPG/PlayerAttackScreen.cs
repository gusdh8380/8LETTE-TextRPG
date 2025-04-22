namespace _8LETTE_TextRPG
{
    internal class PlayerAttackScreen : Screen
    {
        public static readonly PlayerAttackScreen instance = new PlayerAttackScreen();
        private PlayerAttackScreen() { }

        private bool isAttacked = false;
        private int userInput = -1;

        private void ShowAttackList()
        {
            //몬스터 객체를 불러와서 입력 번호와 정보를 출력
            MonsterSpawner.instance.ShowMonsterInfo(true);

            //플레이어의 필요한 정보(레벨, 이름, 직업, 체력)를 출력
            Console.WriteLine("[내 정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job.Name}) ");
            Console.WriteLine($"HP {player.Health}/{player.Job.BaseHealth} ");

            Console.WriteLine();
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("전투!!");

            if (!isAttacked)
            {
                ShowAttackList();
                PrintNumAndString(0, "취소");

                PrintUserInstruction();
            }
            else
            {
                player.Attack(MonsterSpawner.instance.monsters[userInput]);

                PrintAnyKeyInstruction();
            }
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            if (isAttacked)
            {
                isAttacked = false;
                //만약 몬스터가 모두 죽었다면, 전투 결과 화면으로 이동
                if (MonsterSpawner.instance.isAllDead()) return BattleResultScreen.Instance;

                //아니면, 몬스터 공격 화면으로 이동
                return MonsterAttackScreen.Instance;
            }

            if(input == "0")
            {
                return ActionSelectScreen.instance;
            }
            else if (int.TryParse(input, out int num) && 
                1 <= num && num <= MonsterSpawner.instance.monsters.Count &&
                !MonsterSpawner.instance.monsters[num - 1].IsDead)
            {
                userInput = num - 1;
                isAttacked = true;
            }
            else
            {
                isRetry = true;
            }

            return this;
        }
    }
}