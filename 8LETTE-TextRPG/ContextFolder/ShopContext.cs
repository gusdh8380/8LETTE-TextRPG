using _8LETTE_TextRPG.ItemFolder;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace _8LETTE_TextRPG.ContextFolder
{
    class ShopContext
    {
        private readonly static JsonSerializerSettings serializerSettings = new JsonSerializerSettings();

        public List<Item> Items;
        public Dictionary<string, bool> ItemPurchasedDict; // 아이템 아이디, 구매 여부

        public ShopContext()
        {
            serializerSettings.Converters.Add(new StringEnumConverter());

            Items = new List<Item>();
            ItemPurchasedDict = new Dictionary<string, bool>();
        }

        public void Initialize()
        {
            //마우스 아이템
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "잠X리 마우스", "VXN 사의 미친 가성비 마우스. 초경량 마우스로 매우 가볍다.", 950f,
                EquipmentType.Mouse, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 1f },
                { ItemEffect.Def, 1f },
                { ItemEffect.Critical, 1f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "레X저 마우스", "레X저 사의 게이밍 마우스. 초경량이지만 성능 차이가 남다르다.", 2000f,
                EquipmentType.Mouse, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 4f },
                { ItemEffect.Def, 2f },
                { ItemEffect.Evasion, 1f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "로X텍 마우스", "로X텍 사의 마우스. 모두가 즐겨쓰는 마우스이다.", 2350f,
                EquipmentType.Mouse, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 3f },
                { ItemEffect.Def, 3f },
                { ItemEffect.Critical, 1f },
                { ItemEffect.Evasion, 1f }
            }));

            //키보드 아이템
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "다이소 키보드", "기본적인 키보드. 고무 패드가 없어 미끄러진다.", 900f,
                EquipmentType.Keyboard, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 3f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "기계식 키보드", "타건감이 좋은 키보드. 청축을 사용하면 뒤에서 맞을 것 같은 키보드이다.", 1800f,
                EquipmentType.Keyboard, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 6f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "저소음 키보드", "조용한 타건음의 기보드. 타인에 대한 배려가 담겨있다.", 2700f,
                EquipmentType.Keyboard, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 8f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "K8 무선 기계식 키보드", "스파르타가 추천한 무선 기계식 키보드. 품절이 되기 전에 구매해야 한다!", 3600f,
                EquipmentType.Keyboard, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 12f }
            }));

            //모니터 아이템
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "금이 간 모니터", "언제 멈출 지 모르는 금이 간 모니터이다.", 750f,
                EquipmentType.Monitor, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 1f },
                { ItemEffect.Critical, 1f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "풀HD 모니터", "HD 화면으로 눈이 편안한 모니터이다.", 2850f,
                EquipmentType.Monitor, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 2f },
                { ItemEffect.Critical, 5f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "울트라와이드 모니터", "울트라와이드 화면으로 생동감이 흘러넘치는 모니터이다.", 4050f,
                EquipmentType.Monitor, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 3f },
                { ItemEffect.Critical, 7f }
            }));

            //의자 아이템
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "나무 의자", "장시간 앉아 있기는 불편한 삐걱거리는 나무 의자이다.", 200f,
                EquipmentType.Chair, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 1f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "PC방 의자", "PC방에서 본 적 있는 편안한 의자이다.", 1800f,
                EquipmentType.Chair, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 3f },
                { ItemEffect.Evasion, 3f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "인체공학 의자", "인체공학적 설계로 당신의 하루를 받쳐준다.", 3000f,
                EquipmentType.Chair, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 5f },
                { ItemEffect.Evasion, 5f }
            }));

            //책상 아이템
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "나무 책상", "장시간 일하기는 불편한 삐걱거리는 나무 책상이다.", 400f,
                EquipmentType.Desk, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 2f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "길이조절 책상", "길이가 조절되어 편안히 일할 수 있는 책상이다.", 1500f,
                EquipmentType.Desk, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 5f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "커피포트 책상", "언제든지 커피를 먹을 수 있는 최고의 책상이다.", 2350f,
                EquipmentType.Desk, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 5f },
                { ItemEffect.Critical, 3f }
            }));

            //안경 아이템
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "도수 없는 안경", "의미없는 패션 안경이다.", 450f,
                EquipmentType.Glasses, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Critical, 1f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "도수 있는 안경", "시력에 맞춘 안경이다.", 2450f,
                EquipmentType.Glasses, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 1f },
                { ItemEffect.Critical, 5f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "뿔테 안경", "두껍고 튼튼하다. 대신, 조금 무겁다.", 1250f,
                EquipmentType.Glasses, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 5f },
                { ItemEffect.Critical, 5f },
                { ItemEffect.Evasion, -5f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "블루 라이트 안경", "눈 피로를 줄여주는 안경이다.", 5500f,
                EquipmentType.Glasses, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 5f },
                { ItemEffect.Critical, 10f }
            }));
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "스마트 HUD 안경", "AI 기술이 탑재된, 아이언맨이 착용했던 안경이다.", 8750f,
                EquipmentType.Glasses, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 10f },
                { ItemEffect.Critical, 15f }
            }));

            //테스트용 아이템
            Items.Add(new EquipableItem(Guid.NewGuid().ToString(), "테스트", "테스트용 아이템 (장비타입: 모니터).", 999f,
                EquipmentType.Monitor, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 999f },
                { ItemEffect.Def, 999f },
                { ItemEffect.Hp, 999f },
                { ItemEffect.Critical, 999f },
                { ItemEffect.Evasion, 999f },
            }));

            //포션
            Items.Add(new UsableItem(Guid.NewGuid().ToString(), "회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 100f, UseType.Potion,
                new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Hp, 30f }
            }));

            foreach (Item item in Items)
            {
                ItemPurchasedDict.Add(item.Id, false);
            }
        }

        public void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented, serializerSettings);
                File.WriteAllText("shop.json", json);
            }
            catch (Exception e)
            {
                Console.WriteLine("상점 정보 저장에 실패했습니다.");
                Console.WriteLine(e);
            }
        }

        public static ShopContext? Load()
        {
            ShopContext? data = null;
            try
            {
                if (!File.Exists("shop.json"))
                {
                    return null;
                }

                string json = File.ReadAllText("shop.json");
                data = JsonConvert.DeserializeObject<ShopContext>(json, serializerSettings);
            }
            catch (Exception e)
            {
                Console.WriteLine("상점 정보 로딩에 실패했습니다.");
                Console.WriteLine(e);
            }

            return data;
        }
    }
}
