namespace _8LETTE_TextRPG
{
    class Shop
    {
        private List<Item> _items = new List<Item>();
        private Dictionary<string, bool> _itemPurchasedDict = new Dictionary<string, bool>(); // 아이템 아이디, 구매 여부

        public Item[] GetAllItems() => _items.ToArray();

        public Shop()
        {
            _items.Add(new Item("테스트", "테스트용 아이템.", 999f, 1234f, 100f, 0));
            _items.Add(new Item("회복 물약 (30)", "사용 시 HP를 30 회복합니다.", 30f, 100f));

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
                string atk = item.Attack > 0 ? $"공격력 +{item.Attack} " : "";
                string def = item.Defense > 0 ? $"방어력 +{item.Defense} " : "";
                string sold = !_itemPurchasedDict[item.Id] ? item.Price.ToString() + " G" : "구매완료";

                Console.Write("- {0}", isNum ? (_items.IndexOf(item) + 1).ToString() + " " : "");
                Console.WriteLine($"{item.Name} | {atk}{def}| {item.Description} | {sold}");
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

            if (playerItem.IsEquipped)
            {
                Player.Instance.Inventory.Unequip(playerItem);
            }

            Player.Instance.Inventory.DeleteItem(playerItem);
            Player.Instance.Gold += (float)Math.Round(playerItem.Price * 0.85);

            Console.WriteLine($"{playerItem.Name}을(를) 판매했습니다.");
        }
    }
}
