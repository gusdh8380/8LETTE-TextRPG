using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    class Quest
    {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int RequiredKillCount { get; private set; }
        public int CurrentKillCount { get; private set; }
        public Item? RewardItem { get; private set; }
        public float RewardGold { get; private set; }

        /// <summary>
        /// 몬스터 처치 퀘스트
        /// </summary>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <param name="killCount"></param>
        /// <param name="rewardItem"></param>
        /// <param name="rewardGold"></param>
        public Quest(string title, string desc, int killCount, Item? rewardItem = null, float rewardGold = 0f)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Description = desc;
            RequiredKillCount = killCount;
            if (rewardItem != null)
            {
                RewardItem = rewardItem;
            }
            RewardGold = rewardGold;
        }
    }
}
