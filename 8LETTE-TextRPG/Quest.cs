using _8LETTE_TextRPG.ItemFolder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    [Serializable]
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

        [JsonConstructor]
        public Quest(string id, string title, string description, List<QuestGoal> goals, Item? rewardItem = null, float rewardGold = 0f)
        {
            Id = id;
            Title = title;
            Description = description;

            Goals = goals;

            RewardItem = rewardItem;
            RewardGold = rewardGold;

            State = QuestState.BeforeAccept;
        }
    }
}
