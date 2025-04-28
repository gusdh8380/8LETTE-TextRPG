using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.MonsterFolder;
using _8LETTE_TextRPG.QuestFolder;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG.ContextFolder
{
    class QuestContext
    {
        private readonly static JsonSerializerSettings serializerSettings = new JsonSerializerSettings();

        public List<Quest> Quests;

        public QuestContext()
        {
            serializerSettings.Converters.Add(new StringEnumConverter());

            Quests = new List<Quest>();
        }

        public void Initialize()
        {
            Quests.Add(new Quest(Guid.NewGuid().ToString(), "죽여도 끝이 없는 언데드 몬스터",
                "끝없이 살아나는 언데드 버그 몬스터!\n" +
                "언데드 몬스터 5 마리를 처치해 주세요!",
                new List<QuestGoal>
                {
                    new QuestGoal(QuestType.KillMonster, MonsterType.Undead.ToString(), 5)
                },
                new UsableItem(Guid.NewGuid().ToString(), "개초딩 포션", "테스트 퀘스트 1 클리어 보상", 0f, UseType.Potion, new Dictionary<ItemEffect, float>
                {
                    { ItemEffect.Atk, 999f },
                    { ItemEffect.Def, 999f },
                    { ItemEffect.Hp, 999f },
                    { ItemEffect.Critical, 999f },
                    { ItemEffect.Evasion, 999f }
                }),
                5000f)
            );

            Quests.Add(new Quest(Guid.NewGuid().ToString(), "코딩은 장비빨!",
                "좋은 환경은 오랫동안 코딩할 수 있게 만들어줍니다.\n" +
                "인벤토리에서 코딩에 관한 장비를 장착해 보세요!",
                new List<QuestGoal>
                {
                    new QuestGoal(QuestType.EquipItem)
                },
                new EquipableItem(Guid.NewGuid().ToString(), "개사기 마우스", "테스트 퀘스트 2 클리어 보상", 0f, EquipmentType.Mouse, new Dictionary<ItemEffect, float>
                {
                    { ItemEffect.Atk, 999f },
                    { ItemEffect.Def, 999f },
                    { ItemEffect.Hp, 999f },
                    { ItemEffect.Critical, 999f },
                    { ItemEffect.Evasion, 999f }
                }),
                5000f)
            );

            Quests.Add(new Quest(Guid.NewGuid().ToString(), "코딩은 체력 싸움!",
                "체력이 있어야 코딩도 수월하게 할 수 있습니다..\n" +
                "인벤토리에서 포션 한 개를 마셔 보세요!",
                new List<QuestGoal>
                {
                    new QuestGoal(QuestType.UseItem, target: UseType.Potion.ToString())
                },
                rewardGold: 200f)
            );

            Quests.Add(new Quest(Guid.NewGuid().ToString(), "버전 업그레이드",
               "장비도 장비지만 장인은 도구 탓을 하지 않는다.\n" +
               "레벨 업을 하여 능력치를 올려보세요!",
               new List<QuestGoal>
               {
                    new QuestGoal(QuestType.IncreaseStat)
               },
               rewardGold: 500f)
            );
        }

        public void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented, serializerSettings);
                File.WriteAllText("quest.json", json);
            }
            catch (Exception e)
            {
                Console.WriteLine("퀘스트 정보 저장에 실패했습니다.");
                Console.WriteLine(e);
            }
        }

        public static QuestContext? Load()
        {
            QuestContext? data = null;
            try
            {
                if (!File.Exists("quest.json"))
                {
                    return null;
                }

                string json = File.ReadAllText("quest.json");
                data = JsonConvert.DeserializeObject<QuestContext>(json, serializerSettings);
            }
            catch (Exception e)
            {
                Console.WriteLine("퀘스트 정보 로딩에 실패했습니다.");
                Console.WriteLine(e);
            }

            return data;
        }
    }
}
