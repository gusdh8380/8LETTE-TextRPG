using _8LETTE_TextRPG.ItemFolder;
using Newtonsoft.Json;

namespace _8LETTE_TextRPG
{
    //인벤토리 클래스
    // 가장 기본적인 기능만 구현하였습니다.

    //장착 메소드 구현, 장착 시 능력치 갱신 구현
    //+ 이미 장착 중인 경우 기존 아이템과 교체 로직 구현
    //아이템 해제 메소드 구현
    //UI 콘솔 출력은 제외했습니다.(스크린 담당)


    [Serializable]
    public class Inventory
    {
        /// <summary>
        /// 내부 아이템 List
        /// </summary>
        [JsonProperty("Items")]
        private readonly List<Item> _items = new List<Item>();


        public void AddItem(Item item) => _items.Add(item);

        /// <summary>
        /// 아이템 리스트 가져오기
        /// </summary>
        /// <returns></returns>
        public Item[] GetAllItems() => _items.ToArray();

        public Item[] GetItemsOfType(ItemType type)
        {
            return type switch
            {
                ItemType.Equipment => _items.FindAll((x) => x.ItemType == ItemType.Equipment).ToArray(),
                ItemType.Usable => _items.FindAll((x) => x.ItemType == ItemType.Usable).ToArray(),
                _ => [],
            };
        }

        public void RemoveItem(Item item) => _items.Remove(item);

        public float EquippedAttackBonus()
        {
            float atk = 0f;
            EquipableItem[] items = _items.OfType<EquipableItem>().Where(x => x.IsEquipped).ToArray();
            foreach (EquipableItem item in items)
            {
                if (item.Effects.TryGetValue(ItemEffect.Atk, out float value))
                {
                    atk += value;
                }
            }

            return atk;
        }

        public float EquippedDefenseBonus()
        {
            float def = 0f;
            EquipableItem[] items = _items.OfType<EquipableItem>().Where(x => x.IsEquipped).ToArray();
            foreach (EquipableItem item in items)
            {
                if (item.Effects.TryGetValue(ItemEffect.Def, out float value))
                {
                    def += value;
                }
            }

            return def;
        }

        public float EquippedHpBonus()
        {
            float hp = 0f;
            EquipableItem[] items = _items.OfType<EquipableItem>().Where(x => x.IsEquipped).ToArray();
            foreach (EquipableItem item in items)
            {
                if (item.Effects.TryGetValue(ItemEffect.Hp, out float value))
                {
                    hp += value;
                }
            }

            return hp;
        }

        public float EquippedMpBonus()
        {
            float mp = 0f;
            EquipableItem[] items = _items.OfType<EquipableItem>().Where(x => x.IsEquipped).ToArray();
            foreach (EquipableItem item in items)
            {
                if (item.Effects.TryGetValue(ItemEffect.MP, out float value))
                {
                    mp += value;
                }
            }

            return mp;
        }

        public float EquippedCriticalBonus()
        {
            float critical = 0f;
            EquipableItem[] items = _items.OfType<EquipableItem>().Where(x => x.IsEquipped).ToArray();
            foreach (EquipableItem item in items)
            {
                if (item.Effects.TryGetValue(ItemEffect.Critical, out float value))
                {
                    critical += value;
                }
            }

            return critical;
        }

        public float EquippedEvasionBonus()
        {
            float evasion = 0f;
            EquipableItem[] items = _items.OfType<EquipableItem>().Where(x => x.IsEquipped).ToArray();
            foreach (EquipableItem item in items)
            {
                if (item.Effects.TryGetValue(ItemEffect.Evasion, out float value))
                {
                    evasion += value;
                }
            }

            return evasion;
        }


        //장착 메소드
        //입력된 수를 파라미터로 받아서 처리하도록 구현함
        //잘못된 입력에 대해서는 구현하지 않았습니다.
        /// <summary>
        /// 장착 메소드
        /// </summary>
        /// <param name="item"></param>
        public void Equip(IEquipable item)
        {
            if (!string.IsNullOrEmpty(Player.Instance.EquippedItems[item.EquipmentType]))
            {
                IEquipable? equippedItem = _items.Find(x => x.Id == Player.Instance.EquippedItems[item.EquipmentType]) as IEquipable;
                if (equippedItem != null)
                {
                    Unequip(equippedItem);
                }
            }

            item.Equip();

            QuestManager.Instance?.SendProgress(QuestType.EquipItem, ((EquipableItem)item).EquipmentType.ToString(), 1);
            Player.Instance.OnContextChanged();
        }

        public void Unequip(IEquipable item)
        {
            item.Unequip();

            Player.Instance.OnContextChanged();
        }

        public void Use(IUsable item)
        {
            item.Use();

            QuestManager.Instance?.SendProgress(QuestType.UseItem, ((UsableItem)item).UseType.ToString(), 1);
            Player.Instance.OnContextChanged();
        }

        public enum PrintState
        {
            Inventory,
            Equipment,
            Usable,
            Shop
        }

        public void ShowPlayerItems(PrintState state)
        {
            if (_items.Count == 0)
            {
                Console.WriteLine("현재 가지고 있는 아이템이 없습니다.");
                return;
            }

            Item[] equipableItems = GetItemsOfType(ItemType.Equipment);
            Item[] usableItems = GetItemsOfType(ItemType.Usable);

            switch (state)
            {
                case PrintState.Inventory:
                    foreach (Item item in _items)
                    {
                        if (item is IEquipable equipableItem)
                        {
                            Console.WriteLine($"- {(equipableItem.IsEquipped ? "[E]" : "")}{item.Name} | {item.GetEffectName()}| {item.Description} ");
                        }
                        else
                        {
                            Console.WriteLine($"- {item.Name} | {item.GetEffectName()}| {item.Description} ");
                        }
                    }
                    break;
                case PrintState.Equipment:
                    for (int i = 0; i < equipableItems.Length; i++)
                    {
                        Item item = equipableItems[i];
                        if (item is IEquipable equipableItem)
                        {
                            Console.WriteLine($"- {i + 1} {(equipableItem.IsEquipped ? "[E]" : "")}{item.Name} | {item.GetEffectName()}| {item.Description} ");
                        }
                    }
                    break;
                case PrintState.Usable:
                    for (int i = 0; i < usableItems.Length; i++)
                    {
                        Item item = usableItems[i];
                        Console.WriteLine($"- {i + 1} {item.Name} | {item.GetEffectName()}| {item.Description} ");
                    }
                    break;
                case PrintState.Shop:
                    for (int i = 0; i < _items.Count; i++)
                    {
                        Item item = _items[i];
                        Console.WriteLine($"{i + 1}. {item.Name} | {item.GetEffectName()}| {item.Description} | {Math.Round(item.Price * 0.85)} G");
                    }
                    break;
            }
        }
    }
}