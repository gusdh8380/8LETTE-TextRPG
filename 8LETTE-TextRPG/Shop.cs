using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8LETTE_TextRPG
{
    class Shop
    {
        private List<Item> items = new List<Item>();
        private Dictionary<string, bool> itemPurchasedDict = new Dictionary<string, bool>(); // 아이템 아이디, 구매 여부

        public Shop()
        {
            items.Add(new Item("테스트", 999f, 1234f, "테스트용 아이템.", 100f, 0));

            foreach (Item item in items)
            {
                itemPurchasedDict.Add(item.Id, false);
            }
        }

        public void BuyItem(Item item)
        {
            itemPurchasedDict[item.Id] = true;

            Player.Instance.Gold -= item.Price;
            // 아이템 추가 로직
            //Player.Instance.Inventory.AddItem(item);

            Console.WriteLine($"{item.Name}을(를) 구매했습니다. 남은 골드: {Player.Instance.Gold}G");
        }

        public void SellItem(Item item)
        {
            itemPurchasedDict[item.Id] = false;

            // Todo: 장착하고 있던 아이템 해제 후 제거
            //Item playerItem = Player.Instance.Inventory.Items.Find((x) => x.Id == item.Id);
            //if (playerItem.IsEquipped)
            //{
            //    Player.UnEquipItem(playerItem);
            //}

            //Player.Instance.Inventory.RemoveItem(item);
            Player.Instance.Gold += (long)Math.Round(item.Price * 0.85);

            Console.WriteLine($"{item.Name}을(를) 판매했습니다. 남은 골드: {Player.Instance.Gold}G");
        }
    }
}
