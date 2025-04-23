namespace _8LETTE_TextRPG
{
    internal class StatusScreen : Screen
    {
        public static readonly StatusScreen Instance = new StatusScreen();

        private void PrintBonusStatus()
        {
            float atkBounus = Player.Instance.Inventory.EquippedAttackBonus();
            if (atkBounus != 0f)
            {
                Console.WriteLine($"공격력 : {Player.Instance.BaseAttack} (+{atkBounus})");
            }
            else
            {
                Console.WriteLine($"공격력 : {Player.Instance.BaseAttack}");
            }

            float defBounus = Player.Instance.Inventory.EquippedDefenseBonus();
            if (defBounus != 0f)
            {
                Console.WriteLine($"공격력 : {Player.Instance.BaseDefense} (+{defBounus})");
            }
            else
            {
                Console.WriteLine($"방어력 : {Player.Instance.BaseDefense}");
            }

            float hpBounus = Player.Instance.Inventory.EquippedHpBonus();
            if (hpBounus != 0f)
            {
                Console.WriteLine($"체  력 : {Player.Instance.Health} (+{hpBounus}) / {Player.Instance.MaxHealth} (+{hpBounus})");
            }
            else
            {
                Console.WriteLine($"체  력 : {Player.Instance.Health} / {Player.Instance.MaxHealth}");
            }
        }

        public override void Show()
        {
            Console.Clear();

            PrintTitle("상태 보기");

            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            // 플레이어 정보
            Console.WriteLine($"Lv. {Player.Instance.Level.CurrentLevel}");
            Console.WriteLine($"{Player.Instance.Name} ({Player.Instance.Job.Name})");
            PrintBonusStatus();
            Console.WriteLine($"골  드 : {Player.Instance.Gold}G");

            Console.WriteLine();
            PrintNumAndString(0, "나가기");

            PrintUserInstruction();
        }

        public override Screen? Next()
        {
            string? input = Console.ReadLine();
            if (input == "0")
            {
                return TownScreen.Instance;
            }
            else
            {
                _isRetry = true;
            }

            return this;
        }
    }
}