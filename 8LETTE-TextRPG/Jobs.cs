namespace _8LETTE_TextRPG
{
    // 직업 : 주니어
    public class Junior : JobBase 
    {
        public readonly List<Skill> _skills;
        public override string Name => "주니어";
        public override float BaseAttack => 12f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;
        public override int PromotionStage => 0;

        public override List<Skill> Skills => _skills;
        public Junior()
        {
            _skills = new List<Skill>{
                new YaguenSkill()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 0.5f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 0.5f;

            base.IncreaseStats();
        }
    }

    #region 버그 워리어
    public class BugWarrior_Middle : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "버그워리어 (미들)";
        public override float BaseAttack => 15f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;
        public override int PromotionStage => 1;
        
        public override List<Skill> Skills => _skills;
        public BugWarrior_Middle()
       {
            _skills = new List<Skill>{
                new YaguenSkill(), 
                new IncreaseAtk()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 1f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 0.5f;

            base.IncreaseStats();
        }
    }

    public class BugWarrior_Senior : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "버그워리어 (시니어)";
        public override float BaseAttack => 15f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;
        public override int PromotionStage => 2;

        public override List<Skill> Skills => _skills;
        public BugWarrior_Senior()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseAtk(),
                new DebugStrike()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 1f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 0.5f;

            base.IncreaseStats();
        }

    }
    
    public class BugWarrior_Director : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "버그워리어 (디렉터)";
        public override float BaseAttack => 15f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;
        public override int PromotionStage => 3;

        public override List<Skill> Skills => _skills;
        public BugWarrior_Director()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseAtk(),
                new DebugStrike()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 1.5f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 1f;

            base.IncreaseStats();
        }
    }
    #endregion

    #region 메모리 나이트
    public class  MemoryKnight_Middle: JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "메모리나이트";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 10f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;
        public override int PromotionStage => 1;
        
        public override List<Skill> Skills => _skills;
        public MemoryKnight_Middle()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseDfs()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 0.5f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 1f;

            base.IncreaseStats();
        }
    }

    public class MemoryKnight_Senior : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "메모리나이트(시니어)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 10f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;
        public override int PromotionStage => 2;
        
        public override List<Skill> Skills => _skills;
        public MemoryKnight_Senior()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseDfs(),
                new ShieldStrike()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 0.5f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 1f;

            base.IncreaseStats();
        }
    }

    public class MemoryKnight_Director : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "메모리나이트(디렉터)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 10f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;
        public override int PromotionStage => 3;
        
        public override List<Skill> Skills => _skills;
        public MemoryKnight_Director()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseDfs(),
                new ShieldStrike()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 1f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 1.5f;

            base.IncreaseStats();
        }
    }
    #endregion

    #region 스레드 어썌신
    public class ThreadAssassin_Middle: JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "스레드어쌔신(미들)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 30;
        public override int PromotionStage => 1;
        
        public override List<Skill> Skills => _skills;
        public ThreadAssassin_Middle()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseEvasion()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 0.7f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 0.7f;

            base.IncreaseStats();
        }
    }

    public class ThreadAssassin_Senior : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "스레드어쌔신(시니어)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 30;
        public override int PromotionStage => 2;
        
        public override List<Skill> Skills => _skills;
        public ThreadAssassin_Senior()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseEvasion(),
                new Counterattack()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 0.7f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 0.7f;

            base.IncreaseStats();
        }
    }

    public class ThreadAssassin_Director : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "스레드어쌔신(디렉터)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 30;
        public override int PromotionStage => 3;

        public override List<Skill> Skills => _skills;
        public ThreadAssassin_Director()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseEvasion(),
                new Counterattack()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 1.2f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 1.2f;

            base.IncreaseStats();
        }
    }
    #endregion

    #region 익셉션 헌터
    public class ExceptionHunter_Middle : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "익셉션헌터(미들)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 30;
        public override int EvasionRate => 10;
        public override int PromotionStage => 1;

        public override List<Skill> Skills => _skills;
        public ExceptionHunter_Middle()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseCritical()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 0.7f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 0.7f;

            base.IncreaseStats();
        }
    }

    public class ExceptionHunter_Senior : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "익셉션헌터(시니어)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 30;
        public override int EvasionRate => 10;
        public override int PromotionStage => 2;

        public override List<Skill> Skills => _skills;
        public ExceptionHunter_Senior()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseCritical(),
                new Rampage()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 0.7f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 0.7f;

            base.IncreaseStats();
        }
    }

    public class ExceptionHunter_Director : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "익셉션헌터(디렉터)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override float BaseMP => 50f;
        public override int CriticalChance => 30;
        public override int EvasionRate => 10;
        public override int PromotionStage => 3;

        public override List<Skill> Skills => _skills;
        public ExceptionHunter_Director()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseCritical(),
                new Rampage()
            };
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 1.2f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 1.2f;

            base.IncreaseStats();
        }
    }
    #endregion
}
