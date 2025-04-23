using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    class QuestManager
    {
        private static QuestManager? _instance;

        [NotNull]
        public static QuestManager Instance
        {
            get => _instance;
            private set => _instance = value ?? throw new ArgumentNullException("QuestManager Instance is required.");
        }

        private List<Quest> _quests = new List<Quest>();

        public List<Quest> GetAllQuests() => _quests;

        public QuestManager()
        {
            Instance = this;

            _quests.Add(new Quest("테스트 퀘스트 1", "무한루프 몬스터 1마리 처치.",
                new List<QuestGoal>
                {
                    new QuestGoal(QuestType.KillMonster, MonsterType.InfLoop.ToString(), 1)
                },
                new Item("퀘스트 클리어 아이템 1", "테스트 퀘스트 1 클리어 보상", 0f, new Dictionary<ItemEffect, float>
                {
                    { ItemEffect.Atk, 999f },
                    { ItemEffect.Def, 999f },
                    { ItemEffect.Hp, 999f },
                    { ItemEffect.Critical, 999f },
                    { ItemEffect.Evasion, 999f }
                }),
                5000f)
            );

            _quests.Add(new Quest("테스트 퀘스트 2", "아무 장비 장착.",
                new List<QuestGoal>
                {
                    new QuestGoal(QuestType.EquipItem, "", 1)
                },
                new Item("퀘스트 클리어 아이템 2", "테스트 퀘스트 2 클리어 보상 (장비타입: 마우스)", 0f, EquipmentType.Mouse, new Dictionary<ItemEffect, float>
                {
                    { ItemEffect.Atk, 999f },
                    { ItemEffect.Def, 999f },
                    { ItemEffect.Hp, 999f },
                    { ItemEffect.Critical, 999f },
                    { ItemEffect.Evasion, 999f }
                }),
                5000f)
            );
        }

        public void Accept(Quest quest)
        {
            quest.State = QuestState.InProgress;
        }

        public void SendProgress(QuestType type, string target, int amount = 1)
        {
            foreach (Quest quest in _quests)
            {
                foreach (QuestGoal goal in quest.Goals)
                {
                    if (goal.IsCompleted)
                    {
                        continue;
                    }

                    if (goal.Type == type && goal.Target == target)
                    {
                        goal.CurrentAmount += amount;
                    }
                }

                if (quest.IsCompleted)
                {
                    quest.State = QuestState.Completed;
                }
            }
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
        }
    }
}
