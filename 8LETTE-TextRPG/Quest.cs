using _8LETTE_TextRPG.ItemFolder;
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
        
        public List<QuestGoal> Goals { get; private set; }
        public bool IsCompleted => Goals.All(x => x.IsCompleted);

        public Item? RewardItem { get; private set; }
        public float RewardGold { get; private set; }

        public QuestState State { get; set; }

        public Quest(string title, string desc, List<QuestGoal> goals, Item? rewardItem = null, float rewardGold = 0f)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Description = desc;

            Goals = goals;

            RewardItem = rewardItem;
            RewardGold = rewardGold;

            State = QuestState.BeforeAccept;
        }
    }
}
