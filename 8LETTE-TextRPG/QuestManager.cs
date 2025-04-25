using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.MonsterFolder;
using System.Diagnostics.CodeAnalysis;

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

            _quests.Add(new Quest("죽여도 끝이 없는 언데드 몬스터",
                "언데드 몬스터 5 마리를 처치하세요.",
                new List<QuestGoal>
                {
                    new QuestGoal(QuestType.KillMonster, MonsterType.Undead.ToString(), 5)
                },
                new Potion("개초딩 포션", "테스트 퀘스트 1 클리어 보상", 0f, new Dictionary<ItemEffect, float>
                {
                    { ItemEffect.Atk, 999f },
                    { ItemEffect.Def, 999f },
                    { ItemEffect.Hp, 999f },
                    { ItemEffect.Critical, 999f },
                    { ItemEffect.Evasion, 999f }
                }),
                5000f)
            );

            _quests.Add(new Quest("코딩은 장비빨!",
                "좋은 환경은 오랫동안 코딩할 수 있게 만들어줍니다.\n" +
                "인벤토리에서 코딩에 관한 장비를 장착해 보세요!",
                new List<QuestGoal>
                {
                    new QuestGoal(QuestType.EquipItem, "", 1)
                },
                new EquipableItem("개사기 마우스", "테스트 퀘스트 2 클리어 보상", 0f, EquipmentType.Mouse, new Dictionary<ItemEffect, float>
                {
                    { ItemEffect.Atk, 999f },
                    { ItemEffect.Def, 999f },
                    { ItemEffect.Hp, 999f },
                    { ItemEffect.Critical, 999f },
                    { ItemEffect.Evasion, 999f }
                }),
                5000f)
            );

            _quests.Add(new Quest("코딩은 체력싸움!",
                "체력이 있어야 코딩도 수월하게 할 수 있습니다..\n" +
                "인벤토리에서 체력포션 한개를 마시자!",
                new List<QuestGoal>
                {
                    new QuestGoal(QuestType.UseItem, "", 1)
                },
                new EquipableItem("인공눈물", "퀘스트 보상으로 얻은 인공눈물. 넣으면 눈의 피로가 풀린다.", 0f, EquipmentType.Glasses, new Dictionary<ItemEffect, float>
                {
                    { ItemEffect.Atk, 0f },
                    { ItemEffect.Def, 0f },
                    { ItemEffect.Hp, 1f },
                    { ItemEffect.Critical, 0f },
                    { ItemEffect.Evasion, 0f }
                }),
                200f)
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
