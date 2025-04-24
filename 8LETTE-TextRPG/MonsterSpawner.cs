using _8LETTE_TextRPG.MonsterFolder;
using _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster;
using _8LETTE_TextRPG.MonsterFolder.JuniorDungeonMonster;
using _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster;
using _8LETTE_TextRPG.MonsterFolder.SeniorDungeonMonster;
using System;

namespace _8LETTE_TextRPG
{
    internal class MonsterSpawner
    {
        private List<Monster> _monsters = new List<Monster>();
        public Monster[] GetAllMonsters() => _monsters.ToArray();
        public int MonsterCount { get; private set; }
        public int ClearCount { get; private set; }
        public DungeonType Type { get; private set; }

        public int PreviousLevel { get; private set; }
        public float PreviousHP { get; private set; }
        public int PreviousExp { get; private set; }
        public float PreviousGold { get; private set; }

        public static readonly MonsterSpawner Instance = new MonsterSpawner();
        private MonsterSpawner()
        {
            //나중에 정보 저장 후 불러올 때, 여기서 ClearCount 값 초기화 후 Type 변경
            ClearCount = 0;
            ChangeDungeonType();
        }

        private void ChangeDungeonType()
        {
            switch (ClearCount / 5)
            {
                case 0:
                    Type = DungeonType.Junior; break;
                case 1:
                    Type = DungeonType.Middle; break;
                case 2:
                    Type = DungeonType.Senior; break;
                default:
                    Type = DungeonType.Director; break;
            }
        }

        private Monster? SpawnMonster(int num)
        {
            if(Type == DungeonType.Junior)
                switch (num)
                {
                    case 0: return new SemicolonSlime();
                    case 1: return new TypeMissGoblin();
                    case 2: return new LoopZombie();
                    case 3: return new IndexFairy();
                    case 4: return new NullGhost();
                }
            else if (Type == DungeonType.Middle)
                switch (num)
                {
                    case 0: return new InitGhost();
                    case 1: return new LiteralSkeleton();
                    case 2: return new MemoryMelter();
                    case 3: return new LagSpider();
                    case 4: return new DependencyHydra();
                }
            else if (Type == DungeonType.Senior)
                switch (num)
                {
                    case 0: return new OldCodeBigSlime();
                    case 1: return new NoCommentRich();
                    case 2: return new OverturningGolem();
                    case 3: return new IllusionPixie();
                    case 4: return new ConflictDragon();
                }
            else
                switch (num)
                {
                    case 0: return new VoidDragon();
                    case 1: return new TiredWebSpider();
                    case 2: return new SpineFairy();
                    case 3: return new EyeBlurPhantom();
                    case 4: return new CollaborationDestroyer();
                }

            return null;
        }

        public void InitMonsters()
        {
            PreviousLevel = Player.Instance.Level.CurrentLevel;
            PreviousHP = Player.Instance.Health;
            PreviousExp = Player.Instance.Level.CurrentExp;
            PreviousGold = Player.Instance.Gold;

            Random random = new Random();
            MonsterCount = random.Next(1 + (int)Type, 5 + (int)Type);

            _monsters.Clear();
            for (int i = 0; i < MonsterCount; i++)
            {
                _monsters.Add(SpawnMonster(random.Next(0, 5)) ?? new LoopZombie());
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

            if (cnt == 0)
            {
                ClearCount++;
                ChangeDungeonType();
                return true;
            }
            return false;
        }
    }
}
