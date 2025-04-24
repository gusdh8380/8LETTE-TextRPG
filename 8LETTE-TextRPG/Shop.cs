using _8LETTE_TextRPG.ItemFolder;

namespace _8LETTE_TextRPG
{
    class Shop
    {
        private List<Item> _items = new List<Item>();
        private Dictionary<string, bool> _itemPurchasedDict = new Dictionary<string, bool>(); // 아이템 아이디, 구매 여부

        public Item[] GetAllItems() => _items.ToArray();

        public Shop()
        {
            //마우스 아이템
            _items.Add(new Item("잠X리 마우스", "VXN 사의 미친 가성비 마우스. 초경량 마우스로 매우 가볍다.",
                950f, EquipmentType.Mouse, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 1f },
                { ItemEffect.Def, 1f },
                { ItemEffect.Critical, 1f }
            }));
            _items.Add(new Item("레X저 마우스", "레X저 사의 게이밍 마우스. 초경량이지만 성능 차이가 남다르다.",
                2000f, EquipmentType.Mouse, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 4f },
                { ItemEffect.Def, 2f },
                { ItemEffect.Evasion, 1f }
            }));
            _items.Add(new Item("로X텍 마우스", "로X텍 사의 마우스. 모두가 즐겨쓰는 마우스이다.",
                2350f, EquipmentType.Mouse, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 3f },
                { ItemEffect.Def, 3f },
                { ItemEffect.Critical, 1f },
                { ItemEffect.Evasion, 1f }
            }));

            //키보드 아이템
            _items.Add(new Item("다이소 키보드", "기본적인 키보드. 고무 패드가 없어 미끄러진다.",
                900f, EquipmentType.Keyboard, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 3f }
            }));
            _items.Add(new Item("기계식 키보드", "타건감이 좋은 키보드. 청축을 사용하면 뒤에서 맞을 것 같은 키보드이다.",
                1800f, EquipmentType.Keyboard, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 6f }
            }));
            _items.Add(new Item("저소음 키보드", "조용한 타건음의 기보드. 타인에 대한 배려가 담겨있다.",
                2700f, EquipmentType.Keyboard, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 8f }
            }));
            _items.Add(new Item("K8 무선 기계식 키보드", "스파르타가 추천한 무선 기계식 키보드. 품절이 되기 전에 구매해야 한다!",
                3600f, EquipmentType.Keyboard, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 12f }
            }));

            //모니터 아이템
            _items.Add(new Item("금이 간 모니터", "언제 멈출 지 모르는 금이 간 모니터이다.",
                750f, EquipmentType.Monitor, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 1f },
                { ItemEffect.Critical, 1f }
            }));
            _items.Add(new Item("풀HD 모니터", "HD 화면으로 눈이 편안한 모니터이다.",
                2850f, EquipmentType.Monitor, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 2f },
                { ItemEffect.Critical, 5f }
            }));
            _items.Add(new Item("울트라와이드 모니터", "울트라와이드 화면으로 생동감이 흘러넘치는 모니터이다.",
                4050f, EquipmentType.Monitor, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 3f },
                { ItemEffect.Critical, 7f }
            }));

            //의자 아이템
            _items.Add(new Item("나무 의자", "장시간 앉아 있기는 불편한 삐걱거리는 나무 의자이다.",
                200f, EquipmentType.Chair, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 1f }
            }));
            _items.Add(new Item("PC방 의자", "PC방에서 본 적 있는 편안한 의자이다.",
                1800f, EquipmentType.Chair, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 3f },
                { ItemEffect.Evasion, 3f }
            }));
            _items.Add(new Item("인체공학 의자", "인체공학적 설계로 당신의 하루를 받쳐준다.",
                3000f, EquipmentType.Chair, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 5f },
                { ItemEffect.Evasion, 5f }
            }));

            //책상 아이템
            _items.Add(new Item("나무 책상", "장시간 일하기는 불편한 삐걱거리는 나무 책상이다.",
                400f, EquipmentType.Desk, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 2f }
            }));
            _items.Add(new Item("길이조절 책상", "길이가 조절되어 편안히 일할 수 있는 책상이다.",
                1500f, EquipmentType.Desk, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 5f }
            }));
            _items.Add(new Item("커피포트 책상", "언제든지 커피를 먹을 수 있는 최고의 책상이다.",
                2350f, EquipmentType.Desk, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 5f },
                { ItemEffect.Critical, 3f }
            }));

            //안경 아이템
            _items.Add(new Item("도수 없는 안경", "의미없는 패션 안경이다.",
                450f, EquipmentType.Glasses, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Critical, 1f }
            }));
            _items.Add(new Item("도수 있는 안경", "시력에 맞춘 안경이다.",
                2450f, EquipmentType.Glasses, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 1f },
                { ItemEffect.Critical, 5f }
            }));
            _items.Add(new Item("뿔테 안경", "두껍고 튼튼하다. 대신, 조금 무겁다.",
                1250f, EquipmentType.Glasses, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 5f },
                { ItemEffect.Critical, 5f },
                { ItemEffect.Evasion, -5f }
            }));
            _items.Add(new Item("블루 라이트 안경", "눈 피로를 줄여주는 안경이다.",
                5500f, EquipmentType.Glasses, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 5f },
                { ItemEffect.Critical, 10f }
            }));
            _items.Add(new Item("스마트 HUD 안경", "AI 기술이 탑재된, 아이언맨이 착용했던 안경이다.",
                8750f, EquipmentType.Glasses, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Def, 10f },
                { ItemEffect.Critical, 15f }
            }));

            _items.Add(new Item("테스트", "테스트용 아이템 (장비타입: 모니터).", 999f, EquipmentType.Monitor, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Atk, 999f },
                { ItemEffect.Def, 999f },
                { ItemEffect.Hp, 999f },
                { ItemEffect.Critical, 999f },
                { ItemEffect.Evasion, 999f },
            }));
            _items.Add(new Potion("회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 100f, new Dictionary<ItemEffect, float>
            {
                { ItemEffect.Hp, 30f }
            }));

            foreach (Item item in _items)
            {
                _itemPurchasedDict.Add(item.Id, false);
            }
        }

        public void ShowItems(bool isNum = false)
        {
            Console.WriteLine("[상점 목록]");
            foreach (Item item in _items)
            {
                string sold = !_itemPurchasedDict[item.Id] ? item.Price.ToString() + " G" : "구매완료";

                Console.Write("- {0}", isNum ? (_items.IndexOf(item) + 1).ToString() + " " : "");
                Console.WriteLine($"{item.Name} | {item.GetEffectName()}| {item.Description} | {sold}");
            }
        }

        public void BuyItem(Item item)
        {
            if (_itemPurchasedDict[item.Id])
            {
                Console.WriteLine($"{item.Name}은(는) 이미 구매한 항목입니다.");
                return;
            }
            if (Player.Instance.Gold < item.Price)
            {
                Console.WriteLine($"{item.Name}은(는) 돈이 부족하여 살 수 없습니다.");
                return;
            }
            _itemPurchasedDict[item.Id] = true;
            
            Player.Instance.Gold -= item.Price;
            Player.Instance.Inventory.AddItem(item);

            Console.WriteLine($"{item.Name}을(를) 구매했습니다.");
        }

        /// <summary>
        /// 아이템 판매 메소드
        /// </summary>
        /// <param name="playerItem">플레이어 인벤토리의 아이템</param>
        public void SellItem(Item playerItem)
        {
            _itemPurchasedDict[playerItem.Id] = false;

            if (playerItem is IEquipable equipableItem)
            {
                if (equipableItem.IsEquipped)
                {
                    Player.Instance.Inventory.Unequip(equipableItem);
                }
            }

            Player.Instance.Inventory.RemoveItem(playerItem);
            Player.Instance.Gold += (float)Math.Round(playerItem.Price * 0.85);

            Console.WriteLine($"{playerItem.Name}을(를) 판매했습니다.");
        }
    }
}