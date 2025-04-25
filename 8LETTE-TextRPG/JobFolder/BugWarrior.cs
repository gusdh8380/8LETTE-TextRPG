using _8LETTE_TextRPG.SkillFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _8LETTE_TextRPG.JobFolder
{
    class BugWarrior : JobBase
    {
        public BugWarrior(PromotionType type)
        {
            switch (type)
            {
                case PromotionType.Middle:
                    Name = "버그워리어 (미들)";
                    BaseAttack = 15f;
                    BaseDefense = 5f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 15f;
                    EvasionRate = 10f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseAtk()
                    };
                    break;
                case PromotionType.Senior:
                    Name = "버그워리어 (시니어)";
                    BaseAttack = 15f;
                    BaseDefense = 5f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 15f;
                    EvasionRate = 10f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseAtk(),
                        new DebugStrike()
                    };
                    break;
                case PromotionType.Director:
                    Name = "버그워리어 (디렉터)";
                    BaseAttack = 15f;
                    BaseDefense = 5f;
                    BaseHealth = 100f;
                    BaseMP = 50f;
                    CriticalChance = 15f;
                    EvasionRate = 10f;
                    Skills = new List<Skill>
                    {
                        new Yaguen(),
                        new IncreaseAtk(),
                        new DebugStrike()
                    };
                    break;
            }

            JobType = JobType.BugWarrior;
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
                    atkMultiplier = 1f;
                    defMultiplier = 0.5f;
                    break;
                case PromotionType.Director:
                    atkMultiplier = 1.5f;
                    defMultiplier = 1f;
                    break;
            }

            Player.Instance.Stats.LevelBonusAtk += lv * atkMultiplier;
            Player.Instance.Stats.LevelBonusDfs += lv * defMultiplier;

            base.IncreaseStats();
        }
    }
}
