namespace _8LETTE_TextRPG
{

    internal class EquipmentScreen : Screen
    {

        public static readonly EquipmentScreen instance = new EquipmentScreen();
        private EquipmentScreen() { }

        public void IncreaseAttack(float amount)
        {
            Player.Instance.BaseAttack += amount;
        }
        public void IncreaseDefense(float amount)
        {
            Player.Instance.BaseDefense += amount;
        }
        public void ShowEquipMenu()
        {
            while (true)
            {
                Console.Clear();

            var inventory = Player.Instance.Inventory;
            var items = inventory.GetAllItems();

            PrintTitle("인벤토리 - 장착 관리");

            if (items.Count == 0)
            {
                Console.WriteLine("인벤토리에 아무 것도 없습니다.");
                PrintAnyKeyInstruction();
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                string equipped = item.IsEquipped ? "[E]" : "";
                Console.WriteLine($"{equipped}{i + 1}. {item.Name} | 공격력: {item.Attack} |");
            }
                Console.WriteLine();
                PrintNumAndString(0, "나가기");
                PrintUserInstruction();
            string input = Console.ReadLine();
                if (input == "0") break;

            if (int.TryParse(input, out int selected) && selected > 0 && selected <= items.Count)
            {
                var selectedItem = items[selected - 1];

                if(selectedItem.IsEquipped)
                    {
                        IncreaseAttack(-selectedItem.Attack);
                        IncreaseDefense(-selectedItem.Defense);
                        IEquipable.IEquipable();
                    }
                    else
                    {
                        IncreaseAttack(selectedItem.Attack);
                        IncreaseDefense(selectedItem.Defense);
                        IEquipable.Equipe(selectedItem);
                    }

                        Console.WriteLine($"{selectedItem.Name}을(를) {(selectedItem.IsEquipped ? "장착" : "해제")}했습니다.");
            }
        }

        }
        public override void Show()
        {
            ShowEquipMenu();
            PrintAnyKeyInstruction();
        }



        public override Screen? Next()
        {
            
            Console.ReadKey();
            return InventoryScreen.instance;
           

        }



    }
}
