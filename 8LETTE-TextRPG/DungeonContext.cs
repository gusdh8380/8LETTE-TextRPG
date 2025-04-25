using _8LETTE_TextRPG.ItemFolder;
using _8LETTE_TextRPG.MonsterFolder;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    class DungeonContext
    {
        private readonly static JsonSerializerSettings serializerSettings = new JsonSerializerSettings();

        public int MonsterCount;
        public int ClearCount;
        public DungeonType DungeonType;

        public int PreviousLevel;
        public float PreviousHP;
        public int PreviousExp;
        public float PreviousGold;

        public DungeonContext()
        {
            serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public void Initialize()
        {
            ClearCount = 0;
            ChangeDungeonType();
        }

        public void ChangeDungeonType()
        {
            switch (ClearCount / 5)
            {
                case 0:
                    DungeonType = DungeonType.Junior;
                    break;
                case 1:
                    DungeonType = DungeonType.Middle;
                    break;
                case 2:
                    DungeonType = DungeonType.Senior;
                    break;
                default:
                    DungeonType = DungeonType.Director;
                    break;
            }
        }

        public void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented, serializerSettings);
                File.WriteAllText("dungeon.json", json);
            }
            catch (Exception e)
            {
                Console.WriteLine("던전 정보 저장에 실패했습니다.");
                Console.WriteLine(e);
            }
        }

        public static DungeonContext? Load()
        {
            DungeonContext? data = null;
            try
            {
                if (!File.Exists("dungeon.json"))
                {
                    return null;
                }

                string json = File.ReadAllText("dungeon.json");
                data = JsonConvert.DeserializeObject<DungeonContext>(json, serializerSettings);
            }
            catch (Exception e)
            {
                Console.WriteLine("던전 정보 로딩에 실패했습니다.");
                Console.WriteLine(e);
            }

            return data;
        }
    }
}
