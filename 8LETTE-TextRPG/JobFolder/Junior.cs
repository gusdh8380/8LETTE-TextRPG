using _8LETTE_TextRPG.SkillFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.JobFolder
{
    class Junior : JobBase
    {
        public Junior()
        {
            Name = "주니어";
            BaseAttack = 12f;
            BaseDefense = 5f;
            BaseHealth = 100f;
            BaseMP = 50f;
            CriticalChance = 15f;
            EvasionRate = 10f;
            Skills = new List<Skill>
            {
                new Yaguen()
            };

            JobType = JobType.Junior;
            PromotionType = PromotionType.Junior;
        }

        public override void IncreaseStats()
        {
            int lv = Player.Instance.Level.CurrentLevel;

            Player.Instance.Stats.BaseAttack = BaseAttack + lv * 0.5f;
            Player.Instance.Stats.BaseDefense = BaseDefense + lv * 0.5f;

            base.IncreaseStats();
        }
    }
}
