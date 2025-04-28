using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using _8LETTE_TextRPG.JobFolder;

namespace _8LETTE_TextRPG.ContextFolder
{
    [Serializable]
    class PlayerContext
    {
        private readonly static JsonSerializerSettings serializerSettings = new JsonSerializerSettings();

        public string? Name;
        public JobBase? Job;
        public Level? Level;
        public PlayerStats? Stats;

        public float? Gold;
        public bool? IsDead;

        public Inventory? Inventory;
        public Dictionary<EquipmentType, string?>? EquippedItems;

        public PlayerContext()
        {
            serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public void Initialize(string name, JobBase job)
        {
            Name = name;
            Job = job;
            Level = new Level();
            Stats = new PlayerStats();

            Inventory = new Inventory();

            Inventory.AddItem(new UsableItem(Guid.NewGuid().ToString(), "회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 100f, UseType.Potion, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Hp, 30f }
            }));
            Inventory.AddItem(new UsableItem(Guid.NewGuid().ToString(), "회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 100f, UseType.Potion, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Hp, 30f }
            }));
            Inventory.AddItem(new UsableItem(Guid.NewGuid().ToString(), "회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 100f, UseType.Potion, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Hp, 30f }
            }));
            Inventory.AddItem(new EquipableItem(Guid.NewGuid().ToString(), "테스트 아이템1", "모든 스탯이 5000 증가합니다. (장비타입: 안경)", 500f, EquipmentType.Glasses, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 5000f },
                { ItemEffect.Def, 5000f },
                { ItemEffect.Hp, 5000f },
                { ItemEffect.Critical, 5000f },
                { ItemEffect.Evasion, 5000f },
                { ItemEffect.MP, 5000f },
            }));
            Inventory.AddItem(new EquipableItem(Guid.NewGuid().ToString(), "테스트 아이템2", "모든 스탯이 5000 깎입니다. (장비타입: 책상)", 500f, EquipmentType.Desk, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, -5000f },
                { ItemEffect.Def, -5000f },
                { ItemEffect.Hp, -5000f },
                { ItemEffect.Critical, -5000f },
                { ItemEffect.Evasion, -5000f },
                { ItemEffect.MP, -5000f },
            }));

            EquippedItems = new Dictionary<EquipmentType, string?>
            {
                { EquipmentType.Mouse, null },
                { EquipmentType.Keyboard, null },
                { EquipmentType.Monitor, null },
                { EquipmentType.Chair, null },
                { EquipmentType.Desk, null },
                { EquipmentType.Glasses, null }
            };

            Stats.BaseAttack = Job.BaseAttack;
            Stats.BaseDefense = Job.BaseDefense;
            Stats.BaseHealth = Job.BaseHealth;
            Stats.CurHealth = Stats.BaseHealth;

            Stats.BaseCriticalChance = Job.CriticalChance;
            Stats.BaseEvasionRate = Job.EvasionRate;

            Stats.BaseMP = Job.BaseMP;
            Stats.CurMP = Stats.BaseMP;

            Gold = 1500f;
            IsDead = false;
        }

        public void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented, serializerSettings);
                File.WriteAllText("player.json", json);
            }
            catch (Exception e)
            {
                Console.WriteLine("플레이어 정보 저장에 실패했습니다.");
                Console.WriteLine(e);
            }
        }

        public static PlayerContext? Load()
        {
            PlayerContext? data = null;
            try
            {
                if (!File.Exists("player.json"))
                {
                    return null;
                }

                string json = File.ReadAllText("player.json");
                data = JsonConvert.DeserializeObject<PlayerContext>(json, serializerSettings);
            }
            catch (Exception e)
            {
                Console.WriteLine("플레이어 정보 로딩에 실패했습니다.");
                Console.WriteLine(e);
            }

            return data;
        }
    }
}