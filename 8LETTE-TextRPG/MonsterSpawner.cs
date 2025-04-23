using _8LETTE_TextRPG.MonsterFolder;
using _8LETTE_TextRPG.MonsterFolder.JuniorDungeonMonster;

namespace _8LETTE_TextRPG
{
    internal class MonsterSpawner
    {
        private List<Monster> _monsters = new List<Monster>();
        public Monster[] GetAllMonsters() => _monsters.ToArray();
        public int MonsterCount {  get; private set; }
        public float PreviousHP { get; private set; }

        public static readonly MonsterSpawner Instance = new MonsterSpawner();

        public void InitMonsters()
        {
            PreviousHP = Player.Instance.Health;

            Random random = new Random();

            //1 ~ 4 마리의 몬스터 생성
            MonsterCount = random.Next(1, 5);

            //랜덤한 몬스터 생성
            _monsters.Clear();
            for (int i = 0; i < MonsterCount; i++)
            {
                _monsters.Add(new InfLoop());
            }
        }

        //몬스터 객체를 불러와서 정보를 출력
        public void ShowMonsterInfo(bool isNum = false)
        {
            for (int i = 0; i < MonsterCount; i++)
            {
                if (_monsters[i].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }

                if (isNum)
                {
                    Console.Write($"{i + 1} ");
                }

                Console.WriteLine($"Lv.{_monsters[i].Level} {_monsters[i].Name} HP {_monsters[i].Hp}/{_monsters[i].MaxHp}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        //살아있는 모든 몬스터가 플레이어를 공격
        public void AttackPlayer()
        {
            for (int i = 0; i < MonsterCount; i++) 
            {
                if (_monsters[i].IsDead)
                {
                    continue;
                }

                _monsters[i].CurState = Monster.State.Attack;
            }
        }

        //모든 몬스터가 죽었는지 검사
        public bool IsAllDead()
        {
            int cnt = MonsterCount;
            for (int i = 0; i < MonsterCount; i++)
            {
                if (_monsters[i].IsDead) cnt--;
            }

            if (cnt == 0) return true;
            return false;
        }
    }
}
