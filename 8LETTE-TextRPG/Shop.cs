using _8LETTE_TextRPG.ItemFolder;

namespace _8LETTE_TextRPG
{
    class Shop
    {
        private ShopContext _context;

        public Item[] GetAllItems() => _context.Items?.ToArray() ?? throw new NullReferenceException();

        public Shop()
        {
            ShopContext? context = ShopContext.Load();
            if (context == null)
            {
                _context = new ShopContext();
                _context.Initialize();
            }
            else
            {
                _context = context;
            }
        }

        public void ShowItems(bool isNum = false)
        {
            Console.WriteLine("[상점 목록]");
            foreach (Item item in _context.Items)
            {
                string sold = !_context.ItemPurchasedDict[item.Id] ? item.Price.ToString() + " G" : "구매완료";

                Console.Write("- {0}", isNum ? (_context.Items.IndexOf(item) + 1).ToString() + " " : "");
                Console.WriteLine($"{item.Name} | {item.GetEffectName()}| {item.Description} | {sold}");
            }
        }

        public void BuyItem(Item item)
        {
            if (_context.ItemPurchasedDict[item.Id])
            {
                Console.WriteLine($"{item.Name}은(는) 이미 구매한 항목입니다.");
                return;
            }

            if (Player.Instance.Gold < item.Price)
            {
                Console.WriteLine($"{item.Name}은(는) 돈이 부족하여 살 수 없습니다.");
                return;
            }

            Player.Instance.Gold -= item.Price;
            Player.Instance.Inventory.AddItem(item);
            Player.Instance.OnContextChanged();

            _context.ItemPurchasedDict[item.Id] = true;
            _context.Save();

            Console.WriteLine($"{item.Name}을(를) 구매했습니다.");
        }

        /// <summary>
        /// 아이템 판매 메소드
        /// </summary>
        /// <param name="playerItem">플레이어 인벤토리의 아이템</param>
        public void SellItem(Item playerItem)
        {
            if (playerItem is IEquipable equipableItem)
            {
                if (equipableItem.IsEquipped)
                {
                    Player.Instance.Inventory.Unequip(equipableItem);
                }
            }

            Player.Instance.Inventory.RemoveItem(playerItem);
            Player.Instance.Gold += (float)Math.Round(playerItem.Price * 0.85);
            Player.Instance.OnContextChanged();

            _context.ItemPurchasedDict[playerItem.Id] = false;
            _context.Save();

            Console.WriteLine($"{playerItem.Name}을(를) 판매했습니다.");
        }
    }
}