using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    class Shop
    {
        private List<Item> _items = new List<Item>();
        private Dictionary<string, bool> _itemPurchasedDict = new Dictionary<string, bool>(); // 아이템 아이디, 구매 여부

        public List<Item> GetAllItems() => _items;

        public Shop()
        {
            _items.Add(new Item("테스트", 999f, 1234f, "테스트용 아이템.", 100f, 0));

            foreach (Item item in _items)
            {
                _itemPurchasedDict.Add(item.Id, false);
            }
        }

        public void BuyItem(Item item)
        {
            _itemPurchasedDict[item.Id] = true;

            Player.Instance.Gold -= item.Price;
            Player.Instance.Inventory.AddItem(item);

            Console.WriteLine($"{item.Name}을(를) 구매했습니다. 남은 골드: {Player.Instance.Gold}G");
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

            Console.WriteLine($"{playerItem.Name}을(를) 판매했습니다. 남은 골드: {Player.Instance.Gold}G");
        }
    }
}
