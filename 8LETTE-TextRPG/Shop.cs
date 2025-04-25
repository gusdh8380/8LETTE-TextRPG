using _8LETTE_TextRPG.ItemFolder;

namespace _8LETTE_TextRPG
{
    class Shop
    {
        private ShopContext _context;

        public Item[] GetItemsOfType(EquipmentType? equipmentType)
        {
           if(equipmentType != null)
                return _context.Items?
                    .ToArray()
                    .OfType<EquipableItem>()
                    .Where(item => item.EquipmentType == equipmentType)
                    .ToArray();
           else
                return _context.Items?.ToArray().OfType<Potion>().ToArray();
        }
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

        public void ShowItems(EquipmentType? equipmentType)
        {
            Item[] items = GetItemsOfType(equipmentType);

            Console.WriteLine("[판매 목록]");
            for (int i = 0; i < items.Length; i++)
            {
                string sold = !_itemPurchasedDict[items[i].Id] ? items[i].Price.ToString() + " G" : "구매완료";
                Console.WriteLine($"- {i + 1}. {items[i].Name} | {items[i].GetEffectName()}| {items[i].Description} | {sold}");
            }
        }

        public int BuyItem(Item item)
        {
            if (_context.ItemPurchasedDict[item.Id])
            {
                return 2;
            }

            if (Player.Instance.Gold < item.Price)
            {
                return 0;
            }

            Player.Instance.Gold -= item.Price;
            Player.Instance.Inventory.AddItem(item);
            Player.Instance.OnContextChanged();

            _context.ItemPurchasedDict[item.Id] = true;
            _context.Save();

            return 1;
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
        }
    }
}