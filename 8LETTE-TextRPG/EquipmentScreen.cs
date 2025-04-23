namespace _8LETTE_TextRPG
{
    internal class EquipmentScreen : Screen
    {
        public static readonly EquipmentScreen Instance = new EquipmentScreen();

        private Item? _selectedItem = null;

        public override void Show()
        {
            Console.Clear();

            PrintTitle("인벤토리 - 장착 관리");

            Console.WriteLine("보유 중인 아이템을 장착할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("[아이템 목록]");
            Player.Instance.Inventory.ShowPlayerItems(Inventory.PrintState.Equipment);
            Console.WriteLine();

            if (_selectedItem != null)
            {
                Console.WriteLine($"{_selectedItem.Name}을(를) {(_selectedItem.IsEquipped ? "장착" : "해제")}했습니다.");
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
                _selectedItem = null;
                return InventoryScreen.Instance;
            }
            else if (int.TryParse(input, out int num))
            {
                Item[] items = Player.Instance.Inventory.GetAllItems(ItemType.Equipment);
                if (num < 1 || num > items.Length)
                {
                    _selectedItem = null;
                    isRetry = true;
                    return this;
                }
                _selectedItem = items[num - 1];
                _selectedItem.Equip();
            }
            else
            {
                _selectedItem = null;
                isRetry = true;
            }

            return this;
        }
    }
}