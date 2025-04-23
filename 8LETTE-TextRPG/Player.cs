using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;

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
            get => _instance; // 경고가 안 없어져요ㅗㅗㅗㅗㅗㅗㅗㅗ
            private set => _instance = value ?? throw new ArgumentNullException("Player Instance is required.");
        }


        public string Name { get; }
        public Job Job { get; }
        public Level Level { get; set; }
        public float BaseAttack { get; set; }
        public float BaseDefense { get; set; }
        public float MaxHealth { get; set; }
        private float _health;
        public float Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (value > MaxHealth)
                {
                    _health = MaxHealth;
                }
                else if (value <= 0)
                {
                    _health = 0f;
                    Death();
                }
                else
                {
                    _health = value;
                }
            }
        }
        public float Gold { get; set; }
        public bool IsDead { get; private set; }

        public int CriticalChance { get; set; }
        public int EvasionRate { get; set; }

        //인벤토리
        public Inventory Inventory { get; private set; }

        public Dictionary<EquipmentType, string> EquippedItems { get; private set; } // 장착 타입, 아이템 아이디

        //레벨

        public Player(string name, Job job)
        {
            Instance = this;
            Name = name;
            Job = job;
            Level = new Level();

            BaseAttack = job.BaseAttack;
            BaseDefense = job.BaseDefense;
            MaxHealth = job.BaseHealth;
            Health = MaxHealth;
            Gold = 1500f;
            //인벤토리, 레벨, 몬스터 생성자 추가
            Inventory = new Inventory();
            Inventory.AddItem(new Item("회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 100f, hp: 30f));
            Inventory.AddItem(new Item("회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 100f, hp: 30f));
            Inventory.AddItem(new Item("회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 100f, hp: 30f));
            //test용: 낡은 키보드 공격템
            Inventory.AddItem(new Item("낡은 키보드", "가끔씩 키보드가 작동하지 않습니다.", 10f, EquipmentType.Hand, atk: 10));

            EquippedItems = new Dictionary<EquipmentType, string>
            {
                { EquipmentType.Hand, string.Empty },
                { EquipmentType.Head, string.Empty },
                { EquipmentType.Body, string.Empty },
                { EquipmentType.Legs, string.Empty },
                { EquipmentType.Foots, string.Empty },
                { EquipmentType.Item, string.Empty }
            };

            //치명타, 회피율 생성자 추가, 임시로 15%, 10% 고정
            /*
             * 향후 논의 : 레벨업, 아이템에 따른 치명타 및 회피율 수치 변동
             */
            CriticalChance = 15;
            EvasionRate = 10;
        }

        public void GainExp(int exp)
        {
            bool leveledUp = Level.AddExp(exp);
            if (leveledUp)
            {
                IncreaseStats();//기본 능력치 상승
            }

        }

        /// <summary>
        /// 몬스터 공격 메소드. 공격한 몬스터 객체를 파라미터로 받아와서 해당 몬스터의 체력 감소 로직 작성
        /// </summary>
        /// <param name="target"></param>
        public void AttackTo(Monster target)
        {
            Random r = new Random();
            float varirance = (float)Math.Ceiling(BaseAttack * 0.1f);

            //몬스터에게 피해를 입힐 데미지 계산
            //Todo : 몬스터 방어력에 따른 데미지 감소 로직도 염두
            //현재는 방어력 무시


            float damage = BaseAttack + r.Next(-(int)varirance, (int)varirance);
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
            return r.Next(1, 101) <= EvasionRate;
        }

        /// <summary>
        /// 치명타 확률 계산
        /// </summary>
        /// <returns></returns>
        public bool TryCritical()
        {
            Random r = new Random();
            return r.Next(1, 101) <= CriticalChance;
        }

        //플레이어 레벨업 시 능력치 수치 추가 메소드
        public void IncreaseStats()
        {
            BaseAttack += 0.5f;
            BaseDefense += 1f;
        }

        public void OnDamaged(float dmg)
        {
            if (IsDead)
            {
                return;
            }

            Health -= dmg;
        }

        public void Death()
        {
            IsDead = true;
        }
    }
}