using _8LETTE_TextRPG.ItemFolder;
using static _8LETTE_TextRPG.MonsterFolder.Monster;

namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class ShopScreen : Screen
    {
        private Shop _shop;
        private Item[] _items = [];
        private Item? _selectedItem;
        private EquipmentType? _equipmentType;
        private int flag = -1;

        private enum ShopState
        {
            Choice,
            Buy,
            SpecificBuy,
            Sell
        }

        private ShopState _state;

        public static readonly ShopScreen Instance = new ShopScreen();

        private ShopScreen()
        {
            _shop = new Shop();
            _state = ShopState.Choice;
        }

        public override void Show()
        {
            Console.Clear();

            switch (_state)
            {
                case ShopState.Choice:      PrintTitle("상점"); break;
                case ShopState.Buy:
                case ShopState.SpecificBuy: PrintTitle("상점 - 아이템 구매"); break;
                case ShopState.Sell:        PrintTitle("상점 - 아이템 판매"); break;
            }

            Console.WriteLine("아이템을 사고 팔 수 있습니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Player.Instance.Gold} G\n");

            switch (_state)
            {
                case ShopState.Choice:
                    PrintNumAndString(1, "아이템 구매");
                    PrintNumAndString(2, "아이템 판매");
                    break;

                case ShopState.Buy:
                    PrintNumAndString(1, "마우스");
                    PrintNumAndString(2, "키보드");
                    PrintNumAndString(3, "모니터");
                    PrintNumAndString(4, "의자");
                    PrintNumAndString(5, "책상");
                    PrintNumAndString(6, "안경");
                    PrintNumAndString(7, "포션");
                    break;

                case ShopState.SpecificBuy:
                    _shop.ShowItems(_equipmentType);
                    Console.WriteLine();
                    break;

                case ShopState.Sell:
                    Console.WriteLine("[판매 목록]");
                    Player.Instance.Inventory.ShowPlayerItems(Inventory.PrintState.Shop);
                    Console.WriteLine();
                    break;
            }

            switch (flag)
            {
                case 0: // 돈이 부족하여 살 수 없을 때
                    Console.WriteLine($"{_selectedItem?.Name}은(는) 돈이 부족하여 살 수 없습니다.\n");
                    flag = -1;
                    break;

                case 1: // 아이템을 샀을 때
                    Console.WriteLine($"{_selectedItem?.Name}을(를) 구매했습니다.\n");
                    flag = -1;
                    break;

                case 2: // 구매한 아이템을 사려고 할 때
                    Console.WriteLine($"{_selectedItem?.Name}은(는) 이미 구매한 항목입니다.\n");
                    flag = -1;
                    break;

                case 3: // 구매한 아이템을 사려고 할 때
                    Console.WriteLine($"{_selectedItem?.Name}을(를) 판매했습니다.\n");
                    flag = -1;
                    break;

                default: // 아이템 구매/판매 X
                    break;
            }

            PrintNumAndString(0, "나가기");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            switch (_state)
            {
                case ShopState.Choice:
                    if (input == "0")       return TownScreen.Instance;
                    else if (input == "1")  _state = ShopState.Buy;
                    else if (input == "2")  _state = ShopState.Sell;
                    else                    _isRetry = true;
                    break;

                case ShopState.Buy:
                    if (input == "0")
                    {
                        _state = ShopState.Choice;
                    }
                    else if (int.TryParse(input, out int num) && 1 <= num && num <= 7)
                    {
                        _state = ShopState.SpecificBuy;
                        if (num == 7) _equipmentType = null;
                        else _equipmentType = (EquipmentType)num - 1;
                    }
                    else
                    {
                        _isRetry = true;
                    }
                    break;

                case ShopState.SpecificBuy:
                    Item[] items = _shop.GetItemsOfType(_equipmentType);
                    if (input == "0") _state = ShopState.Buy;
                    else if(int.TryParse(input, out int num) && 1 <= num && num <= items.Length)
                    {
                        _selectedItem = items[num - 1];
                        flag = _shop.BuyItem(_selectedItem);
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
                    else if (int.TryParse(input, out int num) && 1 <= num && num <= _items.Length)
                    {
                        _selectedItem = _items[num - 1];
                        _shop.SellItem(_selectedItem);
                        flag = 3;
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