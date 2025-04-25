using _8LETTE_TextRPG.ItemFolder;

namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class UseItemScreen : Screen
    {
        public static readonly UseItemScreen Instance = new UseItemScreen();

        private Item? _selectedItem = null;

        public override void Show()
        {
            Console.Clear();

            PrintTitle("인벤토리 - 아이템 사용");

            Console.WriteLine("보유 중인 아이템을 사용할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("[아이템 목록]");
            Player.Instance.Inventory.ShowPlayerItems(Inventory.PrintState.Usable);
            Console.WriteLine();

            if (_selectedItem != null)
            {
                Console.WriteLine($"{_selectedItem.Name}을(를) 사용했습니다.");
                Console.WriteLine();
            }

            PrintNumAndString(0, "나가기");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            if (input == "0")
            {
                return InventoryScreen.Instance;
            }
            else if (int.TryParse(input, out int num))
            {
                Item[] items = Player.Instance.Inventory.GetItemsOfType(ItemType.Usable);
                if (num < 1 || num > items.Length)
                {
                    _isRetry = true;
                    return this;
                }

                _selectedItem = items[num - 1];
                if (_selectedItem is IUsable usableItem)
                {
                    usableItem.Use();
                }
            }
            else
            {
                _isRetry = true;
            }

            return this;
        }
    }
}