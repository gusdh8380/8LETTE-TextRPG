using _8LETTE_TextRPG.ItemFolder;

namespace _8LETTE_TextRPG
{
    //인벤토리 클래스
    // 가장 기본적인 기능만 구현하였습니다.

    //장착 메소드 구현, 장착 시 능력치 갱신 구현
    //+ 이미 장착 중인 경우 기존 아이템과 교체 로직 구현
    //아이템 해제 메소드 구현
    //UI 콘솔 출력은 제외했습니다.(스크린 담당)



    public class Inventory
    {
        /// <summary>
        /// 내부 아이템 List
        /// </summary>
        private readonly List<Item> _items = new List<Item>();


        public void AddItem(Item item) => _items.Add(item);

        /// <summary>
        /// 아이템 리스트 가져오기
        /// </summary>
        /// <returns></returns>
        public Item[] GetAllItems(ItemType type = ItemType.None)
        {
            switch (type)
            {
                case ItemType.Equipment:
                    return _items.FindAll((x) => x.ItemType == ItemType.Equipment).ToArray();
                case ItemType.Usable:
                    return _items.FindAll((x) => x.ItemType == ItemType.Usable).ToArray();
                default:
                    return _items.ToArray();
            }
        }

        public void RemoveItem(Item item) => _items.Remove(item);

        public float EquippedAttackBonus() => _items.Where(i => i.IsEquipped).Sum(i => i.EquipAtkInc);
        public float EquippedDefenseBonus() => _items.Where(i => i.IsEquipped).Sum(i => i.EquipDefInc);
        public float EquippedHpBonus() => _items.Where(i => i.IsEquipped).Sum(i => i.EquipHpInc);


        //장착 메소드
        //입력된 수를 파라미터로 받아서 처리하도록 구현함
        //잘못된 입력에 대해서는 구현하지 않았습니다.
        /// <summary>
        /// 장착 메소드
        /// </summary>
        /// <param name="item"></param>
        public void Equip(IEquipable item)
        {
            item.Equip();
        }

        public void Unequip(IEquipable item)
        {
            item.Unequip();
        }

        public void Use(IUsable item)
        {
            item.Use();
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

            Item[] equipableItems = _items.FindAll((x) => x.ItemType == ItemType.Equipment).ToArray();
            Item[] usableItems = _items.FindAll((x) => x.ItemType == ItemType.Usable).ToArray();

            switch (state)
            {
                case PrintState.Inventory:
                    foreach (Item item in _items)
                    {
                        Console.WriteLine($"- {(item.IsEquipped ? "[E]" : "")}{item.Name} | {item.GetEffectName()}| {item.Description} ");
                    }
                    break;
                case PrintState.Equipment:
                    for (int i = 0; i < equipableItems.Length; i++)
                    {
                        Item item = equipableItems[i];
                        Console.WriteLine($"- {i + 1} {(item.IsEquipped ? "[E]" : "")}{item.Name} | {item.GetEffectName()}| {item.Description} ");
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