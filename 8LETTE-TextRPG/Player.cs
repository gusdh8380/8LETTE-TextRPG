using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.MonsterFolder;
using System.Diagnostics.CodeAnalysis;
using TextRPG;

namespace _8LETTE_TextRPG
{

    /// <summary>
    /// 직업 클래스 관련해서
    /// 게임 스크린 또는 게임 시작 루프에서
    /// 선택한 직업을 적용하기 위한 로직은 다음과 같습니다.
    ///  List<Job> jobs = Job.GetAllJobs(); -> 직업 리스트를 가져와서서
    /// Job selectedJob = [선택한 직업] (ex: jobs[입력받은 수 -1])
    /// Player player = new Player(name, selectedJob);플레이어 객체 생성
    /// </summary>
    public class Player
    {
        /// <summary>
        /// 초기화용. 외부 클래스에서 인스턴스 사용 시 CS8602 경고 뜨는 것 방지
        /// </summary>
        private static Player? _instance;

        /// <summary>
        /// 플레이어 인스턴스
        /// </summary>
        [NotNull]
        public static Player Instance
        {
            get => _instance ?? throw new NullReferenceException();
            private set => _instance = value;
        }

        private PlayerContext _context;

        public string Name => _context.Name ?? throw new NullReferenceException();
        public Job Job => _context.Job ?? throw new NullReferenceException();
        public Level Level => _context.Level ?? throw new NullReferenceException();
        public PlayerStats Stats => _context.Stats ?? throw new NullReferenceException();

        public float Attack
        {
            get
            {
                if (Stats.BaseAttack + Inventory.EquippedAttackBonus() < 0f)
                {
                    return 0f;
                }
                else
                {
                    return Stats.BaseAttack + Inventory.EquippedAttackBonus();
                }
            }
        }
        public float Defense
        {
            get
            {
                if (Stats.BaseDefense + Inventory.EquippedDefenseBonus() < 0f)
                {
                    return 0f;
                }
                else
                {
                    return Stats.BaseDefense + Inventory.EquippedDefenseBonus();
                }
            }
        }
        public float MaxHealth
        {
            get
            {
                if (Stats.BaseHealth + Inventory.EquippedHpBonus() < 1f)
                {
                    return 1f;
                }
                else
                {
                    return Stats.BaseHealth + Inventory.EquippedHpBonus();
                }
            }
        }
        public float Health
        {
            get
            {
                return Stats.CurHealth;
            }
            set
            {
                if (value > MaxHealth)
                {
                    Stats.CurHealth = MaxHealth;
                }
                else if (value <= 0)
                {
                    Stats.CurHealth = 0f;
                    Death();
                }
                else
                {
                    Stats.CurHealth = value;
                }
            }
        }

        public float Gold
        {
            get => _context.Gold ?? throw new NullReferenceException();
            set => _context.Gold = value;
        }
        public bool IsDead => _context.IsDead ?? throw new NullReferenceException();

        public float CriticalChance
        {
            get
            {
                if (Stats.BaseCriticalChance + Inventory.EquippedCriticalBonus() < 0f)
                {
                    return 0f;
                }
                else if (Stats.BaseCriticalChance + Inventory.EquippedCriticalBonus() > 100f)
                {
                    return 100f;
                }
                else
                {
                    return Stats.BaseCriticalChance + Inventory.EquippedCriticalBonus();
                }
            }
        }
        public float EvasionRate
        {
            get
            {
                if (Stats.BaseEvasionRate + Inventory.EquippedEvasionBonus() < 0f)
                {
                    return 0f;
                }
                else if (Stats.BaseEvasionRate + Inventory.EquippedEvasionBonus() > 100f)
                {
                    return 100f;
                }
                else
                {
                    return Stats.BaseEvasionRate + Inventory.EquippedEvasionBonus();
                }
            }
        }

        //인벤토리
        public Inventory Inventory => _context.Inventory ?? throw new ArgumentNullException("Inventory is not defined.");

        public Dictionary<EquipmentType, string?> EquippedItems => _context.EquippedItems ?? throw new ArgumentNullException("EquippedItems is not defined.");

        //레벨

        public Player()
        {
            Instance = this;

            PlayerContext? context = PlayerContext.Load();
            if (context == null)
            {
                //이름 입력
                Console.WriteLine("이름을 입력해주세요.");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("\n>> ");
                Console.ResetColor();
                string? userName = Console.ReadLine();
                userName = string.IsNullOrEmpty(userName) ? "8LETTE" : userName;

                //직업 선택
                List<Job> jobs = Job.GetJobs();
                Job selectedJob;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("직업을 선택해주세요.");

                    for (int i = 0; i < jobs.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {jobs[i].Name}");
                    }

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\n>> ");
                    Console.ResetColor();

                    if (int.TryParse(Console.ReadLine(), out int num))
                    {
                        if (num < 1 || num > jobs.Count)
                        {
                            continue;
                        }

                        selectedJob = jobs[num - 1];
                        break;
                    }
                }

                _context = new PlayerContext();
                _context.Initialize(userName, selectedJob);
            }
            else
            {
                _context = context;
            }
        }

        public void OnContextChanged()
        {
            _context.Save();
        }

        public void GainExp(int exp)
        {
            bool leveledUp = Level.AddExp(exp);
            if (leveledUp)
            {
                IncreaseStats();//기본 능력치 상승
            }

            OnContextChanged();
        }

        /// <summary>
        /// 몬스터 공격 메소드. 공격한 몬스터 객체를 파라미터로 받아와서 해당 몬스터의 체력 감소 로직 작성
        /// </summary>
        /// <param name="target"></param>
        public void AttackTo(Monster target)
        {
            Random r = new Random();
            float varirance = (float)Math.Ceiling(Attack * 0.1f);

            //몬스터에게 피해를 입힐 데미지 계산
            //Todo : 몬스터 방어력에 따른 데미지 감소 로직도 염두
            //현재는 방어력 무시


            float damage = Attack + r.Next(-(int)varirance, (int)varirance);
            damage = Math.Max(1, damage);//최소 데미지 보장

            //크리티컬 계산
            bool isCritical = TryCritical();
            if (isCritical)
            {
                damage = (float)Math.Ceiling(damage * 1.6);

                //데미지 계산 처리는 몬스터 클래스에서
                target.OnDamaged(damage);

                Console.WriteLine($"{Name}의 공격!");
                Console.WriteLine($"Lv.{target.Level} {target.Name}을 공격.  {damage}의 데미지 - 치명타 공격!!");
            }
            else
            {
                //데미지 계산 처리는 몬스터 클래스에서
                target.OnDamaged(damage);

                Console.WriteLine($"{Name}의 공격!");
                Console.WriteLine($"Lv.{target.Level} {target.Name}에게 {damage}의 데미지를 입혔습니다.");
            }

            if (target.IsDead)
            {
                Console.WriteLine($"\n{target.Name}을(를) 처치했습니다!");
                GainExp(target.Level);
                //만일 몬스터 별로 경험치가 다르게 구현해서
                //속성을 추가해서 파라미터로 받아오게 하면
                //Gain(target.Exp);
            }
        }

        /// <summary>
        /// 회피 확률 계산
        /// </summary>
        /// <returns></returns>
        public bool TryEvade()
        {
            Random r = new Random();
            return r.NextDouble() <= EvasionRate * 0.01f;
        }

        /// <summary>
        /// 치명타 확률 계산
        /// </summary>
        /// <returns></returns>
        public bool TryCritical()
        {
            Random r = new Random();
            return r.NextDouble() <= CriticalChance * 0.01f;
        }

        //플레이어 레벨업 시 능력치 수치 추가 메소드
        public void IncreaseStats()
        {
            Stats.BaseAttack += 0.5f;
            Stats.BaseDefense += 1f;

            QuestManager.Instance.SendProgress(QuestType.IncreaseStat);

            OnContextChanged();
        }

        public void OnDamaged(float dmg)
        {
            if (IsDead)
            {
                return;
            }

            Health -= dmg;

            OnContextChanged();
        }

        public void Death()
        {
            _context.IsDead = true;

            OnContextChanged();
        }
    }
}