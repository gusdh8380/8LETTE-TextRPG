using System;

namespace _8LETTE_TextRPG
{
    internal class InventoryScreen : Screen
    {
        public static readonly InventoryScreen Instance = new InventoryScreen();

        public override void Show()
        {
            Console.Clear();

            PrintTitle("인벤토리");

            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();

            Console.WriteLine("[아이템 목록]");
            Player.Instance.Inventory.ShowPlayerItems(Inventory.PrintState.Inventory);
            Console.WriteLine();

            PrintNumAndString(1, "장착 관리");
            PrintNumAndString(2, "아이템 사용");
            PrintNumAndString(0, "나가기");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    return EquipmentScreen.Instance;
                case "2":
                    return UseItemScreen.Instance;
                case "0":
                    return TownScreen.Instance;
                default:
                    _isRetry = true;
                    return this;
            }
        }
    }
}