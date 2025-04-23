using static _8LETTE_TextRPG.Monster;

namespace _8LETTE_TextRPG
{
    internal class ShopScreen : Screen
    {
        private Shop shop;
        private Item[] items;

        private enum ShopState
        {
            Choice,
            Buy,
            Sell
        }

        private ShopState state;

        private int itemNum;
        private bool isShopping = false;

        public static readonly ShopScreen Instance = new ShopScreen();

        public ShopScreen()
        {
            shop = new Shop();
            state = ShopState.Choice;
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("상점");

            Console.WriteLine("아이템을 사고 팔 수 있습니다.\n");

            switch (state)
            {
                case ShopState.Choice:
                    shop.ShowItems();
                    break;

                case ShopState.Buy:
                    if (isShopping)
                    {
                        isShopping = false;
                        shop.BuyItem(items[itemNum]);
                        Console.WriteLine();
                    }

                    shop.ShowItems(true);
                    break;

                case ShopState.Sell:
                    if (isShopping)
                    {
                        isShopping = false;
                        shop.SellItem(items[itemNum]);
                        Console.WriteLine();
                    }

                    Console.WriteLine("[판매 목록]");
                    Player.Instance.Inventory.ShowPlayerItems(Inventory.PrintState.Shop);
                    break;
            }

            Console.WriteLine("\n[보유 골드]");
            Console.WriteLine($"{Player.Instance.Gold} G\n");

            if (state == ShopState.Choice) PrintNumAndString(1, "아이템 구매");
            if (state == ShopState.Choice) PrintNumAndString(2, "아이템 판매");
            PrintNumAndString(0, "나가기");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string input = Console.ReadLine();
            switch (state)
            {
                case ShopState.Choice:
                    if (input == "0") return TownScreen.Instance;
                    else if (input == "1") state = ShopState.Buy;
                    else if (input == "2") state = ShopState.Sell;
                    else isRetry = true;
                    break;

                case ShopState.Buy:
                    items = shop.GetAllItems();

                    if (input == "0") state = ShopState.Choice;
                    else if (int.TryParse(input, out int num) && 0 < num && num <= items.Length)
                    {
                        itemNum = num - 1;
                        isShopping = true;
                    }
                    else isRetry = true;
                    break;

                case ShopState.Sell:
                    items = Player.Instance.Inventory.GetAllItems();

                    if (input == "0") state = ShopState.Choice;
                    else if (int.TryParse(input, out int num) && 0 < num && num <= items.Length)
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