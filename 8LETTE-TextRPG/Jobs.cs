using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _8LETTE_TextRPG.IncreaseEvasion;

namespace _8LETTE_TextRPG
{

  //직업 : 주니어
    public class Junior : JobBase 
    {
        public readonly List<Skill> _skills;
        public override string Name => "주니어";
        public override float BaseAttack => 12f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;
       
        
        //승급 스테이지 기록
        //1이면 승급?
        //던전 챕터 클리어시 또는 승진 퀘스트 클리어 시 아래 변수 +1
        public override int PromotionStage => 0;

        public override List<Skill> Skills => _skills;
        public Junior()
        {
            _skills = new List<Skill>{
                new YaguenSkill()
            };
        }

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 0.5f;
            player.BonusDefense += 0.5f;
        }
    }

    #region 버그 워리어

    //직업 : 버그워리어(미들)
    public class BugWarrior_Middle : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "버그워리어 (미들)";
        public override float BaseAttack => 15f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
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

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 1f;
            player.BonusDefense += 0.5f;
        }
    }
    public class BugWarrior_Senior : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "버그워리어 (시니어)";
        public override float BaseAttack => 15f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
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

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 1f;
            player.BonusDefense += 0.5f;
        }

    }
       
    
    public class BugWarrior_Director : JobBase
    {
        public readonly List<Skill> _skills;
        public override string Name => "버그워리어 (디렉터)";
        public override float BaseAttack => 15f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
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

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 1.5f;
            player.BonusDefense += 1f;
        }
    }

    #endregion

    #region [메모리 나이트]

    public class  MemoryKnight_Middle: JobBase
    {
        public override string Name => "메모리나이트";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 10f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;
        public override int PromotionStage => 1;

        public readonly List<Skill> _skills;
        public override List<Skill> Skills => _skills;
        public MemoryKnight_Middle()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseDfs()
            };
        }

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 0.5f;
            player.BonusDefense += 1f;
        }
    }

    public class MemoryKnight_Senior : JobBase
    {
        public override string Name => "메모리나이트(시니어)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 10f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;


        //승급 스테이지 기록
        //1이면 승급?
        public override int PromotionStage => 2;
        public readonly List<Skill> _skills;
        public override List<Skill> Skills => _skills;
        public MemoryKnight_Senior()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseDfs(),
                new ShieldStrike()
            };
        }

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 0.5f;
            player.BonusDefense += 1f;
        }
    }
    public class MemoryKnight_Director : JobBase
    {
        public override string Name => "메모리나이트(디렉터)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 10f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 10;


        //승급 스테이지 기록
        //1이면 승급?
        public override int PromotionStage => 3;
        public readonly List<Skill> _skills;
        public override List<Skill> Skills => _skills;
        public MemoryKnight_Director()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseDfs(),
                new ShieldStrike()
            };
        }

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 1f;
            player.BonusDefense += 1.5f;
        }
    }


    #endregion


    #region 스레드 어썌신
    public class ThreadAssassin_Middle: JobBase
    {
        public override string Name => "스레드어쌔신(미들)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 30;


        //승급 스테이지 기록
        //1이면 승급?
        public override int PromotionStage => 0;
        public readonly List<Skill> _skills;
        public override List<Skill> Skills => _skills;
        public ThreadAssassin_Middle()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseEvasion()
            };
        }

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 0.7f;
            player.BonusDefense += 0.7f;
        }
    }

    public class ThreadAssassin_Senior : JobBase
    {
        public override string Name => "스레드어쌔신(시니어)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 30;


        //승급 스테이지 기록
        //1이면 승급?
        public override int PromotionStage => 0;

        public readonly List<Skill> _skills;
        public override List<Skill> Skills => _skills;
        public ThreadAssassin_Senior()
        {
            _skills = new List<Skill>{
                new YaguenSkill(),
                new IncreaseEvasion(),
                new Counterattack()
            };
        }

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 0.7f;
            player.BonusDefense += 0.7f;
        }
    }

    public class ThreadAssassin_Director : JobBase
    {
        public override string Name => "스레드어쌔신(디렉터)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 15;
        public override int EvasionRate => 30;


        //승급 스테이지 기록
        //1이면 승급?
        public override int PromotionStage => 0;

        public override List<Skill> Skills => new()
        {
            new YaguenSkill()

        };

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 0.7f;
            player.BonusDefense += 0.7f;
        }
    }

    #endregion


    #region 익셉션 헌터
    public class ExceptionHunter_Middle : JobBase
    {
        public override string Name => "익셉션헌터(미들)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 30;
        public override int EvasionRate => 10;


        //승급 스테이지 기록
        //1이면 승급?
        public override int PromotionStage => 0;

        public override List<Skill> Skills => new()
        {
            new YaguenSkill()

        };

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 0.7f;
            player.BonusDefense += 0.7f;
        }
    }
    public class ExceptionHunter_Senior : JobBase
    {
        public override string Name => "익셉션헌터(미들)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 30;
        public override int EvasionRate => 10;


        //승급 스테이지 기록
        //1이면 승급?
        public override int PromotionStage => 0;

        public override List<Skill> Skills => new()
        {
            new YaguenSkill()

        };

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 0.7f;
            player.BonusDefense += 0.7f;
        }
    }
    public class ExceptionHunter_Director : JobBase
    {
        public override string Name => "익셉션헌터(미들)";
        public override float BaseAttack => 10f;
        public override float BaseDefense => 5f;
        public override float BaseHealth => 100f;
        public override int CriticalChance => 30;
        public override int EvasionRate => 10;


        //승급 스테이지 기록
        //1이면 승급?
        public override int PromotionStage => 0;

        public override List<Skill> Skills => new()
        {
            new YaguenSkill()

        };

        public override void IncreaseStats(Player player)
        {
            player.BonusAttack += 0.7f;
            player.BonusDefense += 0.7f;
        }
    }

    #endregion

}
