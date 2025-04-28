using _8LETTE_TextRPG.ContextFolder;
using _8LETTE_TextRPG.MonsterFolder;
using _8LETTE_TextRPG.MonsterFolder.DirectorDungeonMonster;
using _8LETTE_TextRPG.MonsterFolder.JuniorDungeonMonster;
using _8LETTE_TextRPG.MonsterFolder.MiddleDungeonMonster;
using _8LETTE_TextRPG.MonsterFolder.SeniorDungeonMonster;
using _8LETTE_TextRPG.ScreenFolder;
using System.Diagnostics.CodeAnalysis;

namespace _8LETTE_TextRPG
{
    class DungeonManager
    {
        private static DungeonManager? _instance;

        [NotNull]
        public static DungeonManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DungeonManager();
                }

                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        private DungeonContext _context;
        private List<Monster> _monsters = new List<Monster>();

        public int MonsterCount => _context.MonsterCount;
        public int ClearCount => _context.ClearCount;
        public DungeonType Type => _context.DungeonType;

        public int PreviousLevel => _context.PreviousLevel;
        public float PreviousHP => _context.PreviousHP;
        public int PreviousExp => _context.PreviousExp;
        public float PreviousGold => _context.PreviousGold;

        public DungeonManager()
        {
            DungeonContext? context = DungeonContext.Load();
            if (context == null)
            {
                _context = new DungeonContext();
                _context.Initialize();
            }
            else
            {
                _context = context;
            }
        }

        public Monster[] GetAllMonsters() => _monsters.ToArray();

        private Monster? SpawnMonster(int num)
        {
            if (Type == DungeonType.Junior)
            {
                return num switch
                {
                    0 => new SemicolonSlime(),
                    1 => new TypeMissGoblin(),
                    2 => new LoopZombie(),
                    3 => new IndexFairy(),
                    4 => new NullGhost(),
                    _ => null
                };
            }
            else if (Type == DungeonType.Middle)
            {
                return num switch
                {
                    0 => new InitGhost(),
                    1 => new LiteralSkeleton(),
                    2 => new MemoryMelter(),
                    3 => new LagSpider(),
                    4 => new DependencyHydra(),
                    _ => null
                };
            }
            else if (Type == DungeonType.Senior)
            {
                return num switch
                {
                    0 => new OldCodeBigSlime(),
                    1 => new NoCommentRich(),
                    2 => new OverturningGolem(),
                    3 => new IllusionPixie(),
                    4 => new ConflictDragon(),
                    _ => null
                };
            }
            else
            {
                return num switch
                {
                    0 => new VoidDragon(),
                    1 => new TiredWebSpider(),
                    2 => new SpineFairy(),
                    3 => new EyeBlurPhantom(),
                    4 => new CollaborationDestroyer(),
                    _ => null
                };
            }
        }

        public void OnContextChanged()
        {
            _context.Save();
        }

        public void InitMonsters()
        {
            _context.PreviousLevel = Player.Instance.Level.CurrentLevel;
            _context.PreviousHP = Player.Instance.Health;
            _context.PreviousExp = Player.Instance.Level.CurrentExp;
            _context.PreviousGold = Player.Instance.Gold;

            Random random = new Random();
            _context.MonsterCount = random.Next(1 + (int)Type, 5 + (int)Type);

            _monsters.Clear();
            for (int i = 0; i < MonsterCount; i++)
            {
                _monsters.Add(SpawnMonster(random.Next(0, 5)) ?? new LoopZombie());
            }

            OnContextChanged();
        }

        /// <summary>
        /// 몬스터 객체를 불러와서 정보를 출력
        /// </summary>
        /// <param name="isNum"></param>
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

                Console.WriteLine($"Lv.{_monsters[i].Level} {_monsters[i].Name} HP {_monsters[i].Hp} / {_monsters[i].MaxHp}");
                Console.ResetColor();
            }

            Console.WriteLine();
        }

        /// <summary>
        /// 살아있는 모든 몬스터가 플레이어를 공격
        /// </summary>
        public void AttackPlayer()
        {
            Console.WriteLine("");

            for (int i = 0; i < MonsterCount; i++) 
            {
                if (_monsters[i].IsDead)
                {
                    continue;
                }

                Console.SetCursorPosition(Console.GetCursorPosition().Left, Console.GetCursorPosition().Top - 1);
                Console.Write(new string(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top);

                _monsters[i].CurState = Monster.State.Attack;

                if(i == MonsterCount - 1)
                {
                    continue;
                }
                TownScreen.Instance.PrintAnyKeyInstruction();
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 모든 몬스터가 죽었는지 검사
        /// </summary>
        /// <returns></returns>
        public bool IsAllDead()
        {
            int cnt = MonsterCount;
            for (int i = 0; i < MonsterCount; i++)
            {
                if (_monsters[i].IsDead)
                {
                    cnt--;
                }
            }

            if (cnt == 0)
            {
                _context.ClearCount++;
                _context.ChangeDungeonType();
                OnContextChanged();
                return true;
            }

            return false;
        }
    }
}
