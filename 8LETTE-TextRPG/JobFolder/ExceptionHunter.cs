using _8LETTE_TextRPG.SkillFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.JobFolder
{
    class ExceptionHunter : JobBase
    {
        public ExceptionHunter(PromotionType type)
        {
            switch (type)
            {
                case PromotionType.Middle:
                    Name = "익셉션헌터 (미들)";
                    BaseAttack = 10f;
                    BaseDefense = 5f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 30f;
                    EvasionRate = 10f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseCritical()
                    };
                    break;
                case PromotionType.Senior:
                    Name = "익셉션헌터 (시니어)";
                    BaseAttack = 10f;
                    BaseDefense = 10f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 30f;
                    EvasionRate = 10f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseCritical(),
                        new Rampage()
                    };
                    break;
                case PromotionType.Director:
                    Name = "익셉션헌터 (디렉터)";
                    BaseAttack = 10f;
                    BaseDefense = 10f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 30f;
                    EvasionRate = 10f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseCritical(),
                        new Rampage()
                    };
                    break;
            }

            JobType = JobType.ExceptionHunter;
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
                    atkMultiplier = 0.7f;
                    defMultiplier = 0.7f;
                    break;
                case PromotionType.Director:
                    atkMultiplier = 1.2f;
                    defMultiplier = 1.2f;
                    break;
            }

            Player.Instance.Stats.LevelBonusAtk += lv * atkMultiplier;
            Player.Instance.Stats.LevelBonusDfs += lv * defMultiplier;

            base.IncreaseStats();
        }
    }
}
