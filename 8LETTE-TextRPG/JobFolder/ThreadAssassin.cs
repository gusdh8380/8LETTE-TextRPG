using _8LETTE_TextRPG.SkillFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.JobFolder
{
    class ThreadAssassin : JobBase
    {
        public ThreadAssassin(PromotionType type)
        {
            switch (type)
            {
                case PromotionType.Middle:
                    Name = "스레드어쌔신 (미들)";
                    BaseAttack = 10f;
                    BaseDefense = 5f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 15f;
                    EvasionRate = 30f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseEvasion()
                    };
                    break;
                case PromotionType.Senior:
                    Name = "스레드어쌔신 (시니어)";
                    BaseAttack = 10f;
                    BaseDefense = 5f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 15f;
                    EvasionRate = 30f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseEvasion(),
                        new Counterattack()
                    };
                    break;
                case PromotionType.Director:
                    Name = "스레드어쌔신 (디렉터)";
                    BaseAttack = 10f;
                    BaseDefense = 5f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 15f;
                    EvasionRate = 30f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseEvasion(),
                        new Counterattack()
                    };
                    break;
            }

            JobType = JobType.ThreadAssassin;
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

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * atkMultiplier;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * defMultiplier;

            base.IncreaseStats();
        }
    }
}
