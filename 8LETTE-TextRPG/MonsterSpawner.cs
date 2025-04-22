namespace _8LETTE_TextRPG
{
    internal class MonsterSpawner
    {
        public List<Monster> monsters;
        public int MonsterNum {  get; private set; }
        public float PreviousHP { get; private set; }

        public static readonly MonsterSpawner instance = new MonsterSpawner() { monsters = new List<Monster>() };
        private MonsterSpawner() { }

        public void InitMonsters(Player player)
        {
            PreviousHP = player.Health;

            Random random = new Random();

            //1 ~ 4 마리의 몬스터 생성
            MonsterNum = random.Next(1, 5);

            //랜덤한 몬스터 생성
            monsters.Clear();
            for (int i = 0; i < MonsterNum; i++)
            {
                monsters.Add(new InfLoop());
            }
        }

        //몬스터 객체를 불러와서 정보를 출력
        public void ShowMonsterInfo(bool isNum = false)
        {
            for (int i = 0; i < MonsterNum; i++)
            {
                if(monsters[i].IsDead) Console.ForegroundColor = ConsoleColor.DarkGray;
                if(isNum) Console.Write($"{i + 1} ");
                Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} HP {monsters[i].Hp}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        //살아있는 모든 몬스터가 플레이어를 공격
        public void AttackPlayer(Player player)
        {
            for (int i = 0; i < MonsterNum; i++) 
            {
                if (monsters[i].IsDead) continue;

                monsters[i].AttackTo(player);
            }
        }

        //모든 몬스터가 죽었는지 검사
        public bool isAllDead()
        {
            int cnt = MonsterNum;
            for (int i = 0; i < MonsterNum; i++)
            {
                if (monsters[i].IsDead) cnt--;
            }

            if(cnt == 0) return true;
            return false;
        }
    }
}
