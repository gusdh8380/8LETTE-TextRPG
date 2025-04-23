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
        public Job Job { get; }
        public int Level { get; set; }
        public float BaseAttack { get; set; }
        public float BaseDefense { get; set; }
        public float Health { get; set; }
        public float Gold { get; set; }

        public int Levels { get; set; }


        //인벤토리
        public Inventory Inventory { get; private set; }

        //레벨

        public Player() { }
        public Player(string name, Job job)
        {
            Instance = this;
            Name = name;
            Job = job;
            Level = 1;
            BaseAttack = job.BaseAttack;
            BaseDefense = job.BaseDefense;
            Health = job.BaseHealth;
            Gold = 1500f;
            //인벤토리, 레벨, 몬스터 생성자 추가
            Inventory = new Inventory();
        }

        //몬스터 공격 메소드
        //공격한 몬스터 객체를 파라미터로 받아와서 해당 몬스터의 체력 감소 로직 작성
        public void Attack(Monster target)
        {
            Random r = new Random();
            float varirance = (float)Math.Ceiling(BaseAttack * 0.1f);

            //몬스터에게 피해를 입힐 데미지 계산
            //Todo : 몬스터 방어력에 따른 데미지 감소 로직도 염두
            //현재는 방어력 무시
            float damage = BaseAttack + r.Next(-(int)varirance, (int)varirance);
            damage = Math.Max(1, damage);//최소 데미지 보장

            //데미지 계산 처리는 몬스터 클래스에서
            target.OnDamaged(damage);

            Console.WriteLine($"{Name}의 공격!");
            Console.WriteLine($"Lv.{target.Level} {target.Name}에게 {damage}의 데미지를 입혔습니다.");

            if (target.IsDead)
            {
                Console.WriteLine($"\n{target.Name}을(를) 처치했습니다!");
            }
        }

        //플레이어 레벨업 시 능력치 수치 추가 메소드
        public void IncreaseStats() { BaseAttack += 0.5f; BaseDefense += 1f; }

        public void OnDamaged(float dmg)
        {
            Health -= dmg;
            // 로직 추가
            if(Health < 0) { Health = 0; }
        }
    }

    public class Job
    {
        public string Name { get; }

        public float BaseAttack { get; }
        public float BaseDefense { get; }
        public float BaseHealth { get; }

        //직업 클래스 : 임시 작성 
        public Job(string name, float baseAttack, float baseDefense, float baseHealth)
        {
            Name = name;
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
            BaseHealth = baseHealth;
        }

        public static List<Job> GetJobs()
        {
            return new List<Job>
            {
                new Job("빨간 ", 12f, 5f, 100f),
                new Job("파란 ", 8f, 10f, 120f),
                new Job("초록 ", 10f, 7f, 110f),
                new Job("노란 ", 15f, 3f, 90f),
                new Job("검정 ", 9f, 6f, 130f)
            };
        }
    }

}
