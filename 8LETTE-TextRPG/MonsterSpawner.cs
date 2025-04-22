namespace _8LETTE_TextRPG
{
    internal class MonsterSpawner
    {
        public List<Monster> monsters;

        public static readonly MonsterSpawner instance = new MonsterSpawner() { monsters = new List<Monster>() };
        private MonsterSpawner() { }

        public void InitMonsters()
        {
            Random random = new Random();

            //1 ~ 4 마리의 몬스터 생성
            int monsterNum = random.Next(1, 5);

            //랜덤한 몬스터 생성
            monsters.Clear();
            for (int i = 0; i < monsterNum; i++)
            {
                monsters.Add(new InfLoop());
            }
        }

        //몬스터 객체를 불러와서 정보를 출력
        public void ShowMonsterInfo(bool isNum = false)
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                if(monsters[i].IsDead) Console.ForegroundColor = ConsoleColor.DarkGray;
                if(isNum) Console.Write($"{i + 1} ");
                Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} HP {monsters[i].Hp}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        public bool isAllDead()
        {
            int cnt = monsters.Count;
            for (int i = 0; i < monsters.Count; i++)
            {
                if (monsters[i].IsDead) cnt--;
            }

            if(cnt == 0) return true;
            return false;
        }
    }
}
