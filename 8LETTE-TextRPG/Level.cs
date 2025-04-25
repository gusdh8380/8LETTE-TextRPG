using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    public class Level
    {
        public int CurrentLevel { get; set; } = 1;
        public int CurrentExp { get; set; } = 0;
        public int NextLevelExp { get; set; } = 10;

        public bool AddExp(int exp)
        {
            bool IsLevelUp = false;

            CurrentExp += exp;

            //경험치 축적과 레벨 업
            if (CurrentExp >= NextLevelExp)
            {
                CurrentExp -= NextLevelExp;
                CurrentLevel++;
                NextLevelExp += 25 + 5 * (CurrentExp - 1);
                IsLevelUp = true;
                QuestManager.Instance?.SendProgress(QuestType.IncreaseStat, "PlayerLevel", 1);
            }

            return IsLevelUp;

        }

    }
}