using static _8LETTE_TextRPG.Monster;

namespace _8LETTE_TextRPG
{
    internal class ShopScreen : Screen
    {
        private Shop _shop;
        private Item[] _items = [];

        private enum ShopState
        {
            Choice,
            Buy,
            Sell
        }

        private ShopState _state;

        private int _itemNum;
        private bool _isShopping = false;

        public static readonly ShopScreen Instance = new ShopScreen();

        public ShopScreen()
        {
            _shop = new Shop();
            _state = ShopState.Choice;
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("상점");

            Console.WriteLine("아이템을 사고 팔 수 있습니다.\n");

            switch (_state)
            {
                case ShopState.Choice:
                    _shop.ShowItems();
                    break;

                case ShopState.Buy:
                    if (_isShopping)
                    {
                        _isShopping = false;
                        _shop.BuyItem(_items[_itemNum]);
                        Console.WriteLine();
                    }

                    _shop.ShowItems(true);
                    break;

                case ShopState.Sell:
                    if (_isShopping)
                    {
                        _isShopping = false;
                        _shop.SellItem(_items[_itemNum]);
                        Console.WriteLine();
                    }

                    Console.WriteLine("[판매 목록]");
                    Player.Instance.Inventory.ShowPlayerItems(Inventory.PrintState.Shop);
                    break;
            }

            Console.WriteLine("\n[보유 골드]");
            Console.WriteLine($"{Player.Instance.Gold} G\n");

            if (_state == ShopState.Choice) PrintNumAndString(1, "아이템 구매");
            if (_state == ShopState.Choice) PrintNumAndString(2, "아이템 판매");
            PrintNumAndString(0, "나가기");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            switch (_state)
            {
                case ShopState.Choice:
                    if (input == "0")
                    {
                        return TownScreen.Instance;
                    }
                    else if (input == "1")
                    {
                        _state = ShopState.Buy;
                    }
                    else if (input == "2")
                    {
                        _state = ShopState.Sell;
                    }
                    else
                    {
                        _isRetry = true;
                    }
                    break;

                case ShopState.Buy:
                    _items = _shop.GetAllItems();

                    if (input == "0")
                    {
                        _state = ShopState.Choice;
                    }
                    else if (int.TryParse(input, out int num) && 0 < num && num <= _items.Length)
                    {
                        _itemNum = num - 1;
                        _isShopping = true;
                    }
                    else
                    {
                        _isRetry = true;
                    }
                    break;

                case ShopState.Sell:
                    _items = Player.Instance.Inventory.GetAllItems();

                    if (input == "0")
                    {
                        _state = ShopState.Choice;
                    }
                    else if (int.TryParse(input, out int num) && 0 < num && num <= _items.Length)
                    {
                        _itemNum = num - 1;
                        _isShopping = true;
                    }
                    else
                    {
                        _isRetry = true;
                    }
                    break;
            }

            return this;
        }
    }
}