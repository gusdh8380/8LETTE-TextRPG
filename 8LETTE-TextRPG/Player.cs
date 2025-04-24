using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.MonsterFolder;
using System.Diagnostics.CodeAnalysis;

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
        //public Job Job { get; }
        public JobBase Job { get; private set; }
        public Level Level { get; set; }

        //공격력 수정 : 전직 시 기존 공격력 유지를 위해
        public float JobBaseAttack => Job.BaseAttack;// 직업 초기 값
        public float BonusAttack {  get; set; } = 0f;//레벨업 추가 능력치
        public float TotalAttack => JobBaseAttack + BonusAttack;


        // 방어력 수정 : 전직 시 기존 방어력 유지를 위해
        public float JobBaseDefense => Job.BaseDefense;// 직업 초기 값
        public float BonusDefense { get; set; } = 0f;//레벨업 추가 능력치
        public float TotalDefense => JobBaseDefense + BonusDefense;

        //스킬 리스트 속성 추가?
        //현재는 player.job.Skill로 사용가능
      
        private float _health;
        public float Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (value > Job.BaseHealth)
                {
                    _health = Job.BaseHealth;
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

        private List<Buff> _buffs = new List<Buff>();

        //방어 계수
        public const float DefenseConstant = 50f;
       

        public Player(string name, JobBase job)
        {
            Instance = this;
            Name = name;
            Job = job;
            Level = new Level();

           // BaseAttack = job.BaseAttack;
            //BaseDefense = job.BaseDefense;
            Health = job.BaseHealth;
            Gold = 1500f;
            //인벤토리, 레벨, 몬스터 생성자 추가
            Inventory = new Inventory();
            Inventory.AddItem(new Item("회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 30f, 100f));
            Inventory.AddItem(new Item("회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 30f, 100f));
            Inventory.AddItem(new Item("회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 30f, 100f));
            //test용: 낡은 키보드 공격템
            Inventory.AddItem(new Item("낡은 키보드", "가끔씩 키보드가 작동하지 않습니다.", 10f, 0f, 200f, 1));

            //치명타, 회피율 생성자 추가, 임시로 15%, 10% 고정
            /*
             * 향후 논의 : 레벨업, 아이템에 따른 치명타 및 회피율 수치 변동
             */
            CriticalChance = job.CriticalChance;
            EvasionRate = job.EvasionRate;
        }

        public void GainExp(int exp)
        {
            bool leveledUp = Level.AddExp(exp);
            if (leveledUp)
            {
                Job.IncreaseStats(this);//기본 능력치 상승
            }

        }
        //전직 메소드, job 클래스를 입력 받음
        public void Promote(JobBase job)
        {
            Job = job;

            CriticalChance = Job.CriticalChance;
            EvasionRate = Job.EvasionRate;

            if (Health > Job.BaseHealth)
                Health = Job.BaseHealth;

            //디렉터에서 스킬 계수 강화
            const int UptpDirector = 3;
            float enforce = (Job.PromotionStage == UptpDirector) ? 1.5f : 1f;
            foreach(var skill in job.Skills)
                skill.PromotionMultiplier = enforce;
        }
        //버프 가져오기
        public void AddBuff(Buff buff)
        {
            _buffs.Add(buff);
        }
        

        //버프로 인한 공격력증가 반환
        public float GetBuffAttack()
        {
            float atk = TotalAttack;

            foreach (var buff in _buffs)
            {
                atk *= buff.AttackMultiplier;
            }

            return atk;
        }
        //버프된 방어력, 몬스터의 데미지 부분에 이 함수 호출
        public float GetBuffedDefense()
        {
            float def = TotalDefense;
            foreach (var buff in _buffs)
            {
                def *= buff.DefenseMultiplier;
            }
            return def;
        }

        //Todo : 치명타, 회피 버브 적용 코드
        public int GetBuffEvasion() 
        {
            int evs = EvasionRate;
            foreach (var buff in _buffs)
            {
                evs += (int)buff.EvasionMultiplier;
            }
            if (evs > 99) { Console.WriteLine("이미 회피율이 99% 이상입니다"); }

            return (int)MathF.Min(evs, 99); 
        }

        public int GetBuffCritical() {  return 1; }


        //턴 종료 시 버프 없애기 : 스크린 클래스에서 플레이어가 공격 시 사용

        public void EndTurn()
        {
            for (int i = _buffs.Count - 1; i >= 0; i--)
            {
                var buff = _buffs[i];
                if (buff.Duration == DurationType.OneTurn)
                {
                    buff.TurnsRemaining--;
                    if (buff.TurnsRemaining <= 0)
                    {
                        //버프 종료
                        _buffs.RemoveAt(i);
                    }
                }
            }
        }
        //전투가 끝나는 타이밍에 버프 제거 메소드
        
        public void ClearBattleBuffs()
        {
            _buffs.RemoveAll(b => b.Duration == DurationType.UntilBattleEnd);
        }

        public float ApplyDefenseReduction(float Damage, float Defense)
        {
            float k = DefenseConstant / (DefenseConstant + Defense);
            float mitigate = Damage * k;
            mitigate = (float)Math.Ceiling(mitigate);
            return Math.Max(1, mitigate);
        }


        /// <summary>
        /// 몬스터 공격 메소드. 공격한 몬스터 객체를 파라미터로 받아와서 해당 몬스터의 체력 감소 로직 작성
        /// </summary>
        /// <param name="target"></param>
        public void AttackTo(Monster target)
        {
            Random r = new Random();

            

            float varirance = (float)Math.Ceiling(TotalAttack * 0.1f);

            //몬스터에게 피해를 입힐 데미지 계산
         

            //공격력 버프 적용, 버프가 없으면 기본 공격력 적용
            float atk = GetBuffAttack();

            float damage = atk + r.Next(-(int)varirance, (int)varirance);

          
            damage = Math.Max(1, damage);//최소 데미지 보장

            //크리티컬 계산
            bool isCritical = TryCritical();
            if (isCritical)
            {
                //방어력에 따른 데미지 감소
                
                damage = (float)Math.Ceiling(damage * 1.6);
                damage = ApplyDefenseReduction(damage, target.Defense);

                //데미지 계산 처리는 몬스터 클래스에서
                target.OnDamaged(damage);

                Console.WriteLine($"{Name}의 공격!");
                Console.WriteLine($"Lv.{target.Level} {target.Name}을 공격.  {damage}의 데미지 - 치명타 공격!!");
            }
            else
            {
                //데미지 계산 처리는 몬스터 클래스에서
                damage = ApplyDefenseReduction(damage, target.Defense);
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
            return r.Next(1, 101) <= GetBuffEvasion();
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

        ////플레이어 레벨업 시 능력치 수치 추가 메소드
        //public void IncreaseStats()
        //{
        //    BaseAttack += 0.5f;
        //    BaseDefense += 1f;
        //}

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