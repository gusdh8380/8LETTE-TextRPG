using static _8LETTE_TextRPG.Monster;

namespace _8LETTE_TextRPG
{
    internal class ShopScreen : Screen
    {
        private Shop shop;
        private List<Item> items;

        private enum ShopState { choice, buy, sell }
        private ShopState state;

        private int itemNum;
        private bool isShopping = false;

        public static readonly ShopScreen instance = new ShopScreen() { };
        private ShopScreen() 
        {
            shop = new Shop();
            state = ShopState.choice;
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("상점");

            Console.WriteLine("아이템을 사고 팔 수 있습니다.\n");

            switch (state)
            {
                case ShopState.choice:
                    shop.ShowItems();
                    break;

                case ShopState.buy:
                    if (isShopping)
                    {
                        isShopping = false;
                        shop.BuyItem(items[itemNum]);
                        Console.WriteLine();
                    }
                    shop.ShowItems(true);
                    break;

                case ShopState.sell:
                    if (isShopping)
                    {
                        isShopping = false;
                        shop.SellItem(items[itemNum]);
                        Console.WriteLine();
                    }
                    Player.Instance.Inventory.ShowPlayerItems();
                    break;
            }

            Console.WriteLine("\n[보유 골드]");
            Console.WriteLine($"{Player.Instance.Gold} G\n");

            if (state == ShopState.choice) PrintNumAndString(1, "아이템 구매");
            if (state == ShopState.choice) PrintNumAndString(2, "아이템 판매");
            PrintNumAndString(0, "나가기");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            switch (state)
            {
                case ShopState.choice:
                    if (input == "0") return TownScreen.instance;
                    else if (input == "1") state = ShopState.buy;
                    else if (input == "2") state = ShopState.sell;
                    else isRetry = true;
                    break;

                case ShopState.buy:
                    items = shop.GetAllItems();

                    if (input == "0") state = ShopState.choice;
                    else if (int.TryParse(input, out int num) && 0 < num && num <= items.Count)
                    {
                        itemNum = num - 1;
                        isShopping = true;
                    }
                    else isRetry = true;
                    break;

                case ShopState.sell:
                    items = Player.Instance.Inventory.GetAllItems();

                    if (input == "0") state = ShopState.choice;
                    else if (int.TryParse(input, out int num) && 0 < num && num <= items.Count)
                    {
                        itemNum = num - 1;
                        isShopping = true;
                    }
                    else isRetry = true;
                    break;
            }

            return this;
        }
    }
}
