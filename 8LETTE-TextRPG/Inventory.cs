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
        public Item[] GetAllItems() => _items.ToArray();
        public void DeleteItem(Item item) => _items.Remove(item);

        public float EquippedAttackBonus() => _items.Where(i => i.IsEquipped).Sum(i => i.Attack);
        public float EquippedDefenseBonus() => _items.Where(i => i.IsEquipped).Sum(i => i.Defense);


        //장착 메소드
        //입력된 수를 파라미터로 받아서 처리하도록 구현함
        //잘못된 입력에 대해서는 구현하지 않았습니다.
        /// <summary>
        /// 장착 메소드
        /// </summary>
        /// <param name="equipableItem"></param>
        public void Equip(IEquipable equipableItem)
        {
            equipableItem.Equip();
        }

        public void Unequip(IEquipable equipableItem)
        {
            equipableItem.Unequip();
        }

        public void ShowPlayerItems()
        {
            Console.WriteLine("[판매 목록]");
            if(_items.Count == 0)
            {
                Console.WriteLine("현재 가지고 있는 아이템이 없습니다.");
                return;
            }

            foreach (Item item in _items)
            {
                string atk = item.Attack > 0 ? $"공격력 +{item.Attack} " : "";
                string def = item.Defense > 0 ? $"방어력 +{item.Defense} " : "";

                Console.Write("- {0}", (_items.IndexOf(item) + 1).ToString() + " ");
                Console.WriteLine($"{item.Name} | {atk}{def}| {item.Description} | {(float)Math.Round(item.Price * 0.85)} G");
            }
        }
    }
}