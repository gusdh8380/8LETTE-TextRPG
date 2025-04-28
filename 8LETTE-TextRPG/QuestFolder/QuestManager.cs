using _8LETTE_TextRPG.ContextFolder;
using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.MonsterFolder;
using System.Diagnostics.CodeAnalysis;

namespace _8LETTE_TextRPG.QuestFolder
{
    class QuestManager
    {
        private static QuestManager? _instance;

        [NotNull]
        public static QuestManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QuestManager();
                }

                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }

        private QuestContext _context;

        public Quest[] GetAllQuests() => _context.Quests.ToArray();

        public QuestManager()
        {
            QuestContext? context = QuestContext.Load();
            if (context == null)
            {
                _context = new QuestContext();
                _context.Initialize();
            }
            else
            {
                _context = context;
            }
        }

        public void Accept(Quest quest)
        {
            quest.State = QuestState.InProgress;
            _context.Save();
        }

        /// <summary>
        /// 퀘스트 진행 업데이트
        /// </summary>
        /// <param name="type"></param>
        /// <param name="target"></param>
        /// <param name="amount"></param>
        public void SendProgress(QuestType type, string? target = null, int amount = 1)
        {
            foreach (Quest quest in _context.Quests)
            {
                foreach (QuestGoal goal in quest.Goals)
                {
                    if (goal.IsCompleted)
                    {
                        continue;
                    }

                    if (goal.Type == type)
                    {
                        if (string.IsNullOrEmpty(goal.Target) || goal.Target == target)
                        {
                            goal.CurrentAmount += amount;
                        }
                    }
                }

                if (quest.IsCompleted)
                {
                    quest.State = QuestState.Completed;
                }
            }

            _context.Save();
        }

        public void ClaimReward(Quest quest)
        {
            if (!quest.IsCompleted)
            {
                return;
            }

            if (quest.RewardItem != null)
            {
                Player.Instance.Inventory.AddItem(quest.RewardItem);
            }

            if (quest.RewardGold != 0f)
            {
                Player.Instance.Gold += quest.RewardGold;
            }

            quest.State = QuestState.Rewarded;
            _context.Save();
        }
    }
}