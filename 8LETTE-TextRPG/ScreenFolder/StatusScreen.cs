namespace _8LETTE_TextRPG.ScreenFolder
{
    internal class StatusScreen : Screen
    {
        public static readonly StatusScreen Instance = new StatusScreen();

        private void PrintBonusStatus()
        {
            Console.WriteLine();

            float hpBounus = Player.Instance.Inventory.EquippedHpBonus();
            if (hpBounus != 0f)
            {
                if (hpBounus > 0f)
                {
                    Console.WriteLine($"체  력 : {Player.Instance.Health} / {Player.Instance.MaxHealth} (+{hpBounus})");
                }
                else
                {
                    Console.WriteLine($"체  력 : {Player.Instance.Health} / {Player.Instance.MaxHealth} ({hpBounus})");
                }
            }
            else
            {
                Console.WriteLine($"체  력 : {Player.Instance.Health} / {Player.Instance.MaxHealth}");
            }

            float atkBounus = Player.Instance.Inventory.EquippedAttackBonus();
            if (atkBounus != 0f)
            {
                if (atkBounus > 0f)
                {
                    Console.WriteLine($"공격력 : {Player.Instance.Attack} (+{atkBounus})");
                }
                else
                {
                    Console.WriteLine($"공격력 : {Player.Instance.Attack} ({atkBounus})");
                }
            }
            else
            {
                Console.WriteLine($"공격력 : {Player.Instance.Attack}");
            }

            float defBounus = Player.Instance.Inventory.EquippedDefenseBonus();
            if (defBounus != 0f)
            {
                if (defBounus > 0f)
                {
                    Console.WriteLine($"방어력 : {Player.Instance.Defense} (+{defBounus})");
                }
                else
                {
                    Console.WriteLine($"방어력 : {Player.Instance.Defense} ({defBounus})");
                }
            }
            else
            {
                Console.WriteLine($"방어력 : {Player.Instance.Defense}");
            }

            float criticalBonus = Player.Instance.Inventory.EquippedCriticalBonus();
            if (criticalBonus != 0f)
            {
                if (criticalBonus > 0f)
                {
                    Console.WriteLine($"치명타 : {Player.Instance.CriticalChance} (+{criticalBonus}) %");
                }
                else
                {
                    Console.WriteLine($"치명타 : {Player.Instance.CriticalChance} ({criticalBonus}) %");
                }
            }
            else
            {
                Console.WriteLine($"치명타 : {Player.Instance.CriticalChance} %");
            }

            float evasionBonus = Player.Instance.Inventory.EquippedEvasionBonus();
            if (evasionBonus != 0f)
            {
                if (evasionBonus > 0f)
                {
                    Console.WriteLine($"회  피 : {Player.Instance.EvasionRate} (+{evasionBonus}) %");
                }
                else
                {
                    Console.WriteLine($"회  피 : {Player.Instance.EvasionRate} ({evasionBonus}) %");
                }
            }
            else
            {
                Console.WriteLine($"회  피 : {Player.Instance.EvasionRate} %");
            }

            Console.WriteLine();
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