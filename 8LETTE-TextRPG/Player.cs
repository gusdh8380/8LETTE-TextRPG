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
        // 초기화용. 외부 클래스에서 인스턴스 사용 시 CS8602 경고 뜨는 것 방지
        private static Player? _instance;

        // 플레이어 인스턴스
        [NotNull]
        public static Player Instance
        {
            get => _instance; // 경고가 안 없어져요ㅗㅗㅗㅗㅗㅗㅗㅗ
            private set => _instance = value ?? throw new ArgumentNullException("Player Instance is required.");
        }

        public string Name { get; }
        public JobBase Job { get; private set; }
        public Level Level { get; set; }

        //공격력 수정 : 전직 시 기존 공격력 유지를 위해
        public float PotionBonusAttack { get; set; } = 0f;
        public float LevelBonusAttack { get; set; } = 0f; // 레벨업 추가 능력치
        public float JobBaseAttack => Job.BaseAttack + PotionBonusAttack;// 직업 초기 값
        public float TotalAttack => JobBaseAttack + LevelBonusAttack + Inventory.EquippedAttackBonus();

        // 방어력 수정 : 전직 시 기존 방어력 유지를 위해
        public float PotionBonusDefense { get; set; } = 0f;
        public float LevelBonusDefense { get; set; } = 0f; // 레벨업 추가 능력치
        public float JobBaseDefense => Job.BaseDefense + PotionBonusDefense; // 직업 초기 값
        public float TotalDefense => JobBaseDefense + LevelBonusDefense + Inventory.EquippedDefenseBonus();

        //치명 & 회피
        public float PotionBonusCritical { get; set; } = 0f;
        public float TotalCriticalChance => Job.CriticalChance + PotionBonusCritical + Inventory.EquippedCriticalBonus();
        public float PotionBonusEvasion { get; set; } = 0f;
        public float TotalEvasionRate => Job.EvasionRate + PotionBonusEvasion + Inventory.EquippedEvasionBonus();

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

        //인벤토리
        public Inventory Inventory { get; private set; }
        public Dictionary<EquipmentType, string?> EquippedItems { get; private set; } // 장착 타입, 아이템 아이디

        //스킬
        private List<Buff> _buffs = new List<Buff>();
        public IEnumerable<Skill> PassiveReflectSkill => Job.Skills.Where(s => s.Type == SkillType.Passive);

        //방어 계수
        public const float DefenseConstant = 50f;

        public Player(string name, JobBase job)
        {
            Instance = this;
            Name = name;
            Job = job;
            Level = new Level();

            Inventory = new Inventory();
            Inventory.AddItem(new Potion("회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 100f, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Hp, 30f }
            }));
            Inventory.AddItem(new Potion("회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 100f, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Hp, 30f }
            }));
            Inventory.AddItem(new Potion("회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 100f, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Hp, 30f }
            }));
            Inventory.AddItem(new EquipableItem("테스트 아이템2", "모든 스탯이 5000 깎입니다. (장비타입: 책상)", 500f, EquipmentType.Desk, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, -5000f },
                { ItemEffect.Def, -5000f },
                { ItemEffect.Hp, -5000f },
                { ItemEffect.Critical, -5000f },
                { ItemEffect.Evasion, -5000f },
            }));
            Inventory.AddItem(new EquipableItem("테스트 아이템2", "모든 스탯이 5000 깎입니다. (장비타입: 책상)", 500f, EquipmentType.Desk, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 5000f },
                { ItemEffect.Def, 5000f },
                { ItemEffect.Hp, 5000f },
                { ItemEffect.Critical, 5000f },
                { ItemEffect.Evasion, 5000f },
            }));

            Health = job.BaseHealth;

            Gold = 1500f;
        }

        public void GainExp(int exp)
        {
            bool leveledUp = Level.AddExp(exp);
            if (leveledUp)
            {
                Job.IncreaseStats(this);

            }

        }

        //전직 메소드, job 클래스를 입력 받음
        public void Promote(JobBase job)
        {
            Job = job;

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
        public float GetBuffEvasion() 
        {
            float evs = TotalEvasionRate;
            foreach (var buff in _buffs)
            {
                evs += buff.EvasionMultiplier;
            }
            if (evs >= 100)
            {
                evs = 100;
                Console.WriteLine("이미 회피율이 100% 입니다");
            }

            return MathF.Min(evs, 100); 
        }
        public float GetBuffCritical() 
        {  
            float critical = TotalCriticalChance;
            foreach (var buff in _buffs)
            {
                critical += buff.CriticalMultiplier;  
            }
            if(critical >= 100)
            {
                critical = 100;
                Console.WriteLine("이미 치명타가 100% 입니다");
            }

            return MathF.Min(critical, 100);
        }

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

        //방어력에 따른 데미지 감면 로직 구현
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
                Console.WriteLine($"{Name}의 공격!");
                Console.WriteLine($"Lv.{target.Level} {target.Name}을 공격.  {damage}의 데미지 - 치명타 공격!!");

                //데미지 계산 처리는 몬스터 클래스에서
                target.OnDamaged(damage);

               
            }
            else
            {
                //데미지 계산 처리는 몬스터 클래스에서
                Console.WriteLine($"{Name}의 공격!");
                Console.WriteLine($"Lv.{target.Level} {target.Name}에게 {damage}의 데미지를 입혔습니다.");
                damage = ApplyDefenseReduction(damage, target.Defense);
                target.OnDamaged(damage);

               
            }

            if (target.IsDead)
            {   
                //Console.WriteLine($"\n{target.Name}을(를) 처치했습니다!");
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
            return r.Next(1, 101) <= GetBuffCritical();
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