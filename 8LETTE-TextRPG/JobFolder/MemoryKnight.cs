using _8LETTE_TextRPG.SkillFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.JobFolder
{
    class MemoryKnight : JobBase
    {
        public MemoryKnight(PromotionType type)
        {
            switch (type)
            {
                case PromotionType.Middle:
                    Name = "메모리나이트 (미들)";
                    BaseAttack = 10f;
                    BaseDefense = 10f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 15f;
                    EvasionRate = 10f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseDfs()
                    };
                    break;
                case PromotionType.Senior:
                    Name = "메모리나이트 (시니어)";
                    BaseAttack = 10f;
                    BaseDefense = 10f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 15f;
                    EvasionRate = 10f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseDfs(),
                        new ShieldStrike()
                    };
                    break;
                case PromotionType.Director:
                    Name = "메모리나이트 (디렉터)";
                    BaseAttack = 10f;
                    BaseDefense = 10f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 15f;
                    EvasionRate = 10f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseDfs(),
                        new ShieldStrike()
                    };
                    break;
            }

            JobType = JobType.MemoryKnight;
            PromotionType = type;
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;
            float atkMultiplier = 0f;
            float defMultiplier = 0f;

            switch (PromotionType)
            {
                case PromotionType.Middle or PromotionType.Senior:
                    atkMultiplier = 0.5f;
                    defMultiplier = 1f;
                    break;
                case PromotionType.Director:
                    atkMultiplier = 1f;
                    defMultiplier = 1.5f;
                    break;
            }

            Player.Instance.Stats.LevelBonusAtk += lv * atkMultiplier;
            Player.Instance.Stats.LevelBonusDfs += lv * defMultiplier;

            base.IncreaseStats();
        }
    }
}
