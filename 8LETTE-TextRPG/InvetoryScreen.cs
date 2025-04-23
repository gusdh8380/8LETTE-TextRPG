namespace _8LETTE_TextRPG
{
    internal class InventoryScreen : Screen
    {
        public static readonly InventoryScreen instance = new InventoryScreen();
        private InventoryScreen() { }

        public override void Show()
        {
            Console.Clear();
            var items = Player.Instance.Inventory.GetAllItems();

            PrintTitle("인벤토리");
            Console.WriteLine("");
            if (items.Count == 0)
            {
                Console.WriteLine("인벤토리에 아무 것도 없습니다.");
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    string eqipped = item.IsEquipped ? "[E]" : "";
                    Console.WriteLine($"{eqipped} {i + 1}. {item.Name} | 공격력: {item.Attack} |");
                }
            }


        }



        public override Screen? Next()
        {
            Console.WriteLine();
            PrintNumAndString(1, "장착 관리");
            PrintNumAndString(0, "나가기");
            PrintUserInstruction();
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    return EquipmentScreen.instance;
                case "0":
                    return TownScreen.instance;
                default:
                    isRetry = true;
                    return this;
            }

            


        }
    }
}